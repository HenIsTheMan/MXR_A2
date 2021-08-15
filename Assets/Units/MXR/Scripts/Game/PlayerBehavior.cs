using MobfishCardboard;
using UnityEngine;

namespace MXR {
    internal sealed class PlayerBehavior: MonoBehaviour {
        #region Fields

        [SerializeField]
        private PlayerAttribs playerAttribs;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PlayerBehavior(): base() {
            playerAttribs = null;
        }

        static PlayerBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            playerAttribs.Dir = Vector3.Normalize(transform.rotation * Vector3.forward);

            transform.localPosition
                += playerAttribs.Dir
                * playerAttribs.Spd
                * Time.deltaTime;

            if(Application.isEditor) {
                if(Input.GetMouseButton(0)) {
                    SimulatePlayerRotation();
                }
            } else{
                PlayerRotation();
            }
        }

        private void SimulatePlayerRotation() {
            Vector3 eulerAngles = transform.localEulerAngles;

            float targetRotX = eulerAngles.x - GetMouseY();
            if(targetRotX < 90.0f || targetRotX > -90.0f) {
                eulerAngles.x = targetRotX;
            }

            float targetRotY = eulerAngles.y + GetMouseX();
            if(targetRotY > 360.0f) {
                targetRotY -= 360.0f;
            } else if(targetRotY < -360.0f) {
                targetRotY += 360.0f;
            }
            eulerAngles.y = targetRotY;

            eulerAngles.z = Mathf.Min(24.0f, eulerAngles.z - GetMouseY());

            transform.localEulerAngles = eulerAngles;
        }

        private float GetMouseX() {
            return Input.GetAxis("Mouse X");
        }

        private float GetMouseY() {
            return Input.GetAxis("Mouse Y");
        }

        private void PlayerRotation() {
            CardboardHeadTracker.UpdatePose();
            transform.localPosition = CardboardHeadTracker.trackerUnityPosition;
            transform.localRotation = CardboardHeadTracker.trackerUnityRotation;
        }

        #endregion
    }
}