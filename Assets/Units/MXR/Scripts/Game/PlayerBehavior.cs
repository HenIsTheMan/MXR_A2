using MobfishCardboard;
using UnityEngine;

namespace MXR {
    internal sealed class PlayerBehavior: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlayerAttribs playerAttribs;

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private float camDist;

        [SerializeField]
        private float camYOffset;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PlayerBehavior(): base() {
            playerAttribs = null;

            camTransform = null;

            camDist = 0.0f;
            camYOffset = 0.0f;
        }

        static PlayerBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            Vector3 playerDir = Vector3.Normalize(transform.rotation * Vector3.forward);

            transform.localPosition
                += playerDir
                * playerAttribs.Spd
                * Time.deltaTime;

            camTransform.localPosition = transform.localPosition - playerDir * camDist + new Vector3(0.0f, camYOffset, 0.0f);

            camTransform.localRotation = Quaternion.FromToRotation(
                Vector3.forward,
                Vector3.Normalize(transform.localPosition - camTransform.localPosition)
            );

            //Vector3 eulerAngles = CardboardHeadTracker.trackerRawRotation.eulerAngles;
            //transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
            //= Quaternion.Euler(
            //    0.0f,
            //    Mathf.Atan2((CardboardHeadTracker.trackerUnityRotation.eulerAngles * Vector3.forward).y, 1.0f),
            //    0.0f
            //);
        }

        #endregion
    }
}