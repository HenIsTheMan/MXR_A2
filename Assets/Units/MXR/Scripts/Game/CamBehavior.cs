using MXR.Math;
using UnityEngine;

namespace MXR {
    internal sealed class CamBehavior: MonoBehaviour {
        #region Fields

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private float camHorizontalDist;

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

            camHorizontalDist = 0.0f;
            camYOffset = 0.0f;

            camPosSmoothingFactor = 0.0f;
            camRotationSmoothingFactor = 0.0f;

            playerAttribs = null;
        }

        static CamBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void FixedUpdate() {
            camTransform.localPosition = Val.Lerp(
                camTransform.localPosition,
                playerAttribs.MyTransform.localPosition - playerAttribs.Dir * camHorizontalDist + new Vector3(0.0f, camYOffset, 0.0f),
                Time.fixedDeltaTime * (camPosSmoothingFactor + playerAttribs.AccelFactor)
            );

            Quaternion rotation = Quaternion.Lerp(
                camTransform.localRotation,
                Quaternion.FromToRotation(
                    Vector3.forward,
                    Vector3.Normalize(playerAttribs.MyTransform.localPosition - camTransform.localPosition)
                ),
                Time.fixedDeltaTime * camRotationSmoothingFactor
            );

            Vector3 eulerAngles = rotation.eulerAngles;
            rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0.0f);

			camTransform.localRotation = rotation;
        }

        #endregion
    }
}