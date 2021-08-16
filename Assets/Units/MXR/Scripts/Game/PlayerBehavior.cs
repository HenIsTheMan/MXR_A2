using MobfishCardboard;
using UnityEngine;

namespace MXR {
    internal sealed class PlayerBehavior: MonoBehaviour {
        #region Fields

        private Vector3 simulatedEulerAngles;
        private Quaternion actualRotation;

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
            simulatedEulerAngles = Vector3.zero;
            actualRotation = Quaternion.identity;
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
            simulatedEulerAngles = transform.localEulerAngles;
        }

        private void Update() {
            playerAttribs.Dir = Vector3.Normalize(transform.rotation * Vector3.forward);

            if(Application.isEditor) {
                CalcSimulatedPlayerRotation();
            } else {
                CalcActualPlayerRotation();
            }

            playerAttribs.Spd += playerAttribs.AccelFactor * Time.deltaTime;
            playerAttribs.Spd = Mathf.Clamp(playerAttribs.Spd, playerAttribs.MinSpd, playerAttribs.MaxSpd);

            transform.localPosition
                += playerAttribs.Dir
                * playerAttribs.Spd
                * Time.deltaTime;

            if(Application.isEditor) {
                if(Input.GetMouseButton(1)) {
                    ApplySimulatedPlayerRotation();
                }
            } else{
                ApplyActualPlayerRotation();
            }
        }

        private void CalcSimulatedPlayerRotation() {
            if(shldLimitX) {
                simulatedEulerAngles.x = Mathf.Clamp(
                    simulatedEulerAngles.x - GetMouseY() * targetRotationXMultiplier * Time.deltaTime,
                    xLocalEulerAngleMinMax.x,
                    xLocalEulerAngleMinMax.y
                );
            } else {
                simulatedEulerAngles.x -= GetMouseY() * targetRotationXMultiplier * Time.deltaTime;
            }

            if(shldLimitY) {
                simulatedEulerAngles.y = Mathf.Clamp(
                    simulatedEulerAngles.y + GetMouseX() * targetRotationYMultiplier * Time.deltaTime,
                    yLocalEulerAngleMinMax.x,
                    yLocalEulerAngleMinMax.y
                );
            } else {
                simulatedEulerAngles.y += GetMouseX() * targetRotationYMultiplier * Time.deltaTime;
            }

            if(shldLimitZ) {
                simulatedEulerAngles.z = Mathf.Clamp(
                    simulatedEulerAngles.z - GetMouseX() * targetRotationZMultiplier * Time.deltaTime,
                    zLocalEulerAngleMinMax.x,
                    zLocalEulerAngleMinMax.y
                );
            } else {
                simulatedEulerAngles.z -= GetMouseX() * targetRotationZMultiplier * Time.deltaTime;
            }
        }

        private void CalcActualPlayerRotation() {
            actualRotation = CardboardHeadTracker.trackerUnityRotation;
        }

        private float GetMouseX() {
            return Input.GetAxis("Mouse X");
        }

        private float GetMouseY() {
            return Input.GetAxis("Mouse Y");
        }

        private void ApplySimulatedPlayerRotation() {
            transform.localEulerAngles = simulatedEulerAngles;
        }

        private void ApplyActualPlayerRotation() {
            CardboardHeadTracker.UpdatePose();
            transform.localRotation = actualRotation;
        }

        #endregion
    }
}