using MXR.Math;
using UnityEngine;

namespace MXR {
    internal sealed class CamBehavior: MonoBehaviour {
        #region Fields

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private float camDist;

        [SerializeField]
        private float camYOffset;

        [SerializeField]
        private float camPosSmoothingFactor;

        [SerializeField]
        private float camRotationSmoothingFactor;

        [SerializeField]
        private PlayerAttribs playerAttribs;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal CamBehavior(): base() {
            camTransform = null;

            camDist = 0.0f;
            camYOffset = 0.0f;

            camPosSmoothingFactor = 0.0f;
            camRotationSmoothingFactor = 0.0f;

            playerAttribs = null;
        }

        static CamBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void LateUpdate() {
            camTransform.localPosition = Val.Lerp(
                camTransform.localPosition,
                transform.localPosition - playerAttribs.Dir * camDist + new Vector3(0.0f, camYOffset, 0.0f),
                Time.deltaTime * camPosSmoothingFactor
            );

            camTransform.localRotation = Val.Slerp(
                camTransform.localRotation,
                Quaternion.FromToRotation(
                    Vector3.forward,
                    Vector3.Normalize(transform.localPosition - camTransform.localPosition)
                ),
                Time.deltaTime * camRotationSmoothingFactor
            );
        }

        #endregion
    }
}