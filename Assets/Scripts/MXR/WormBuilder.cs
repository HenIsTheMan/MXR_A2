using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace MXR {
    [ExecuteAlways]
    internal sealed class WormBuilder: MonoBehaviour {
        #region Fields

        [SerializeField]
        private bool shldBuildInEditMode;

        [Tooltip("Not Really Useful"), SerializeField]
        private bool shldBuildInPlayMode;

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

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal WormBuilder(): base() {
            shldBuildInEditMode = false;
            shldBuildInPlayMode = false;

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
        }

        static WormBuilder() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            if(Application.isPlaying && shldBuildInPlayMode) {
                BuildWorm();
                shldBuildInPlayMode = false;
            }

            if(!Application.isPlaying && shldBuildInEditMode) {
                BuildWorm();
                shldBuildInEditMode = false;
            }
        }

        private void Update() {
            if(!Application.isPlaying && shldBuildInEditMode) {
                BuildWorm();
            }
            shldBuildInEditMode = false;
        }

        #endregion

        private void BuildWorm() {
            boneRenderer.transforms = new Transform[amtOfBodySegments + 1];
            boneRenderer.transforms[0] = headTransform;

            Transform wormPartTransform = headTransform;
            DampedTransform dampedTransformComponent;

            for(int i = 0; i < amtOfBodySegments; ++i) {
                dampedTransformComponent = Instantiate(dampedPrefab, rigTransform).GetComponent<DampedTransform>();
                dampedTransformComponent.data.sourceObject = wormPartTransform;

                dampedTransformComponent.weight = weight;
                dampedTransformComponent.data.dampPosition = dampPosition;
                dampedTransformComponent.data.dampRotation = dampRotation;

                wormPartTransform = Instantiate(bodySegmentPrefab, wormPartTransform).transform;
                wormPartTransform.localPosition = displacementBetweenBodySegments;

                boneRenderer.transforms[i + 1] = wormPartTransform;

                dampedTransformComponent.data.constrainedObject = wormPartTransform;
            }
        }
    }
}