using UnityEngine;

namespace MXR {
    public class SimulateHeadTransformVR: MonoBehaviour {
        [SerializeField]
        private Transform targetTransform;

        private void Awake() {
            if(targetTransform == null)
                targetTransform = GetComponent<Transform>();

            if(!Application.isEditor)
                enabled = false;
        }

        private void Update() {
            if(GetKeyRotate()) {
                Vector3 currentEulerAngle = targetTransform.localEulerAngles;
                float targetRotX = currentEulerAngle.x - GetMouseY();
                if(targetRotX < 90 || targetRotX > -90) {
                    currentEulerAngle.x = targetRotX;
                }
                float targetRotY = currentEulerAngle.y + GetMouseX();
                if(targetRotY > 360)
                    targetRotY -= 360;
                else if(targetRotY < -360)
                    targetRotY += 360;
                currentEulerAngle.y = targetRotY;

                targetTransform.localEulerAngles = currentEulerAngle;
            } else if(GetKeyTilt()) {
                Vector3 currentEulerAngle = targetTransform.localEulerAngles;
                float targetRotZ = currentEulerAngle.z - GetMouseY();

                currentEulerAngle.z = targetRotZ;
                targetTransform.localEulerAngles = currentEulerAngle;
            }

        }

        private bool GetKeyRotate() {
            return Input.GetKey(KeyCode.LeftAlt);
        }

        private bool GetKeyTilt() {
            return Input.GetKey(KeyCode.LeftControl);
        }

        private float GetMouseX() {
            return Input.GetAxis("Mouse X");
        }

        private float GetMouseY() {
            return Input.GetAxis("Mouse Y");
        }
    }
}