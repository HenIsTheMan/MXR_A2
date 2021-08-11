using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace MXR {
    [ExecuteInEditMode]
    internal sealed class WormBuilder: MonoBehaviour {
        #region Fields

        [SerializeField]
        private bool shldBuild;

        [SerializeField]
        private Transform rigTransform;

        [SerializeField]
        private Transform headTransform;

        [SerializeField]
        private GameObject bodySegmentPrefab;

        [SerializeField]
        private GameObject dampedPrefab;

        [SerializeField]
        private int amtOfBodySegments;

        [SerializeField]
        private Vector3 displacementBetweenBodySegments;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal WormBuilder(): base() {
            shldBuild = false;

            rigTransform = null;
            headTransform = null;

            bodySegmentPrefab = null;
            dampedPrefab = null;

            amtOfBodySegments = 0;
            displacementBetweenBodySegments = Vector3.zero;
        }

        static WormBuilder() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(shldBuild) {
                Transform wormPartTransform = headTransform;
                DampedTransform dampedTransform;

                for(int i = 0; i < amtOfBodySegments; ++i) {
                    dampedTransform = Instantiate(dampedPrefab, rigTransform).GetComponent<DampedTransform>();

                    wormPartTransform = Instantiate(bodySegmentPrefab, wormPartTransform).transform;
                    wormPartTransform.localPosition = displacementBetweenBodySegments;
                }
            }
            shldBuild = false;
        }

        #endregion
    }
}