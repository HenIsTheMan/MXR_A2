using MobfishCardboard;
using UnityEngine;

namespace MXR {
    internal sealed class PlayerBehavior: MonoBehaviour {
        #region Fields

        private Vector3 eulerAngles;

        [SerializeField]
        private PlayerAttribs playerAttribs;

        [SerializeField]
        private bool shldLimitX;

        [SerializeField]
        private bool shldLimitY;

        [SerializeField]
        private bool shldLimitZ;

        [SerializeField]
        private Vector2 xLocalEulerAngleMinMax;

        [SerializeField]
        private Vector2 yLocalEulerAngleMinMax;

        [SerializeField]
        private Vector2 zLocalEulerAngleMinMax;

        [SerializeField]
        private float targetRotationXMultiplier;

        [SerializeField]
        private float targetRotationYMultiplier;

        [SerializeField]
        private float targetRotationZMultiplier;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PlayerBehavior(): base() {
            eulerAngles = Vector3.zero;
            playerAttribs = null;

            shldLimitX = true;
            shldLimitY = true;
            shldLimitZ = true;

            xLocalEulerAngleMinMax = Vector2.zero;
            yLocalEulerAngleMinMax = Vector2.zero;
            zLocalEulerAngleMinMax = Vector2.zero;

            targetRotationXMultiplier = 0.0f;
            targetRotationYMultiplier = 0.0f;
            targetRotationZMultiplier = 0.0f;
        }

        static PlayerBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            eulerAngles = transform.localEulerAngles;
        }

        private void Update() {
            playerAttribs.Dir = Vector3.Normalize(transform.rotation * Vector3.forward);

            playerAttribs.Spd += playerAttribs.AccelFactor * Time.deltaTime;
            playerAttribs.Spd = Mathf.Clamp(playerAttribs.Spd, playerAttribs.MinSpd, playerAttribs.MaxSpd);

            transform.localPosition
                += playerAttribs.Dir
                * playerAttribs.Spd
                * Time.deltaTime;

            if(Application.isEditor) {
                if(Input.GetMouseButton(1)) {
                    SimulatePlayerRotation();
                }
            } else{
                PlayerRotation();
            }
        }

        private void SimulatePlayerRotation() {
            if(shldLimitX) {
                eulerAngles.x = Mathf.Clamp(
                    eulerAngles.x - GetMouseY() * targetRotationXMultiplier * Time.deltaTime,
                    xLocalEulerAngleMinMax.x,
                    xLocalEulerAngleMinMax.y
                );
            } else {
                eulerAngles.x -= GetMouseY() * targetRotationXMultiplier * Time.deltaTime;
            }

            if(shldLimitY) {
                eulerAngles.y = Mathf.Clamp(
                    eulerAngles.y + GetMouseX() * targetRotationYMultiplier * Time.deltaTime,
                    yLocalEulerAngleMinMax.x,
                    yLocalEulerAngleMinMax.y
                );
            } else {
                eulerAngles.y += GetMouseX() * targetRotationYMultiplier * Time.deltaTime;
            }

            if(shldLimitZ) {
                eulerAngles.z = Mathf.Clamp(
                    eulerAngles.z - GetMouseX() * targetRotationZMultiplier * Time.deltaTime,
                    zLocalEulerAngleMinMax.x,
                    zLocalEulerAngleMinMax.y
                );
            } else {
                eulerAngles.z -= GetMouseX() * targetRotationZMultiplier * Time.deltaTime;
            }

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
            transform.localRotation = CardboardHeadTracker.trackerUnityRotation;
        }

        #endregion
    }
}