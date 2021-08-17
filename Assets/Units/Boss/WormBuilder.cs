using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace MXR {
    [ExecuteAlways]
    internal sealed class WormBuilder: MonoBehaviour {
        [System.Serializable]
        private struct ExtraSegment {
            [SerializeField]
            internal Vector3 displacement;

            [SerializeField]
            internal GameObject prefab;
        };

        #region Fields

        [SerializeField]
        private bool shldClearInEditMode;

        [SerializeField]
        private bool shldBuildInEditMode;

        [Tooltip("Not Really Useful"), SerializeField]
        private bool shldBuildInPlayMode;

        [SerializeField]
        private string myTag;

        [SerializeField]
        private Transform rigTransform;

        [SerializeField]
        private Transform headTransform;

        [SerializeField]
        private GameObject bodySegmentPrefab;

        [SerializeField]
        private GameObject dampedPrefab;

        [SerializeField]
        private BoneRenderer boneRenderer;

        [SerializeField]
        private float weight;

        [SerializeField]
        private float dampPosition;

        [SerializeField]
        private float dampRotation;

        [SerializeField]
        private int amtOfBodySegments;

        [SerializeField]
        private Vector3 displacementBetweenBodySegments;

        [SerializeField]
        private ExtraSegment[] extraSegments;

        [SerializeField]
        private Component[] scriptsToAdd;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal WormBuilder(): base() {
            shldClearInEditMode = false;
            shldBuildInEditMode = false;
            shldBuildInPlayMode = false;

            myTag = string.Empty;

            rigTransform = null;
            headTransform = null;

            bodySegmentPrefab = null;
            dampedPrefab = null;

            boneRenderer = null;
            weight = 0.0f;

            dampPosition = 0.0f;
            dampRotation = 0.0f;

            amtOfBodySegments = 0;
            displacementBetweenBodySegments = Vector3.zero;

            extraSegments = System.Array.Empty<ExtraSegment>();
            scriptsToAdd = System.Array.Empty<Component>();
        }

        static WormBuilder() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnValidate() {
            if(string.IsNullOrEmpty(myTag)) {
                myTag = "Untagged";
            }
        }

        private void Awake() {
            #if UNITY_EDITOR

            if(Application.isPlaying && shldBuildInPlayMode) {
                BuildWorm();
                shldBuildInPlayMode = false;
            }

            if(!Application.isPlaying && shldBuildInEditMode) {
                BuildWorm();
                shldBuildInEditMode = false;
            }

            #endif
        }

        private void Update() {
            #if UNITY_EDITOR

            if(!Application.isPlaying && shldBuildInEditMode) {
                BuildWorm();
            }
            shldBuildInEditMode = false;

            if(!Application.isPlaying && shldClearInEditMode) {
                List<GameObject> toBeDestroyed = new List<GameObject>();

                foreach(Transform childTransform in rigTransform) {
                    toBeDestroyed.Add(childTransform.gameObject);
                }

                foreach(Transform childTransform in headTransform) {
                    toBeDestroyed.Add(childTransform.gameObject);
                }

                toBeDestroyed.ForEach(GO => DestroyImmediate(GO));

                boneRenderer.transforms = new Transform[1];
                boneRenderer.transforms[0] = headTransform;
            }
            shldClearInEditMode = false;

            #endif
        }

        #endregion

        private void BuildWorm() {
            #if UNITY_EDITOR

            boneRenderer.transforms = new Transform[amtOfBodySegments + 1];
            boneRenderer.transforms[0] = headTransform;

            Transform wormPartTransform = headTransform;
            wormPartTransform.tag = myTag;
            DampedTransform dampedTransformComponent;

            for(int i = 0; i < amtOfBodySegments; ++i) {
                dampedTransformComponent = Instantiate(dampedPrefab, rigTransform).GetComponent<DampedTransform>();
                dampedTransformComponent.data.sourceObject = wormPartTransform;

                dampedTransformComponent.weight = weight;
                dampedTransformComponent.data.dampPosition = dampPosition;
                dampedTransformComponent.data.dampRotation = dampRotation;

                wormPartTransform = Instantiate(bodySegmentPrefab, wormPartTransform).transform;
                wormPartTransform.tag = myTag;
                wormPartTransform.localPosition = displacementBetweenBodySegments;

				foreach(Component script in scriptsToAdd) {
                    _ = UnityEditorInternal.ComponentUtility.CopyComponent(script);
                    _ = UnityEditorInternal.ComponentUtility.PasteComponentAsNew(wormPartTransform.gameObject);
				}

				boneRenderer.transforms[i + 1] = wormPartTransform;

                dampedTransformComponent.data.constrainedObject = wormPartTransform;
            }

            int len = extraSegments.Length;
            for(int i = 0; i < len; ++i) {
                wormPartTransform = Instantiate(extraSegments[i].prefab, wormPartTransform).transform;
                wormPartTransform.tag = myTag;
                wormPartTransform.localPosition = extraSegments[i].displacement;

                foreach(Component script in scriptsToAdd) {
                    _ = UnityEditorInternal.ComponentUtility.CopyComponent(script);
                    _ = UnityEditorInternal.ComponentUtility.PasteComponentAsNew(wormPartTransform.gameObject);
                }
            }

            #endif
        }

        private void CopyAndAddComponent<T, Type>(T original, Type destination)
            where T: Component
            where Type: Object
        {
            System.Type type = original.GetType();
            var dst = (destination as GameObject).GetComponent(type) as T;
            if(!dst)
                dst = (destination as GameObject).AddComponent(type) as T;
            var fields = type.GetFields();
            foreach(var field in fields) {
                if(field.IsStatic)
                    continue;
                field.SetValue(dst, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach(var prop in props) {
                if(!prop.CanWrite || !prop.CanWrite || prop.Name == "name")
                    continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }
        }
    }
}