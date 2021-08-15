using MobfishCardboard;
using UnityEngine;

namespace MXR {
    public class GoogleCardboardHeadTransformVR: MonoBehaviour {
        [SerializeField]
        private Transform targetTransform;

        private void Awake() {
            if(targetTransform == null)
                targetTransform = GetComponent<Transform>();

            if(Application.isEditor)
                enabled = false;
        }

        private void Update() {
            CardboardHeadTracker.UpdatePose();
            targetTransform.localPosition = CardboardHeadTracker.trackerUnityPosition;
            targetTransform.localRotation = CardboardHeadTracker.trackerUnityRotation;
        }
    }
}