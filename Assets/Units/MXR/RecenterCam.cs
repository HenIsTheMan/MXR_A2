using TMPro;
using UnityEngine;

namespace MXR {
    internal sealed class RecenterCam: MonoBehaviour {
        #region Fields

        public Transform camTransform;

        public TMP_Text myTmp;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal RecenterCam(): base() {
        }

        static RecenterCam() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Start() {
        }

        void Update() {
            myTmp.text = $"{camTransform.localPosition.x}, {camTransform.localPosition.y}, {camTransform.localPosition.z}\n{camTransform.localRotation.x}, {camTransform.localRotation.y}, {camTransform.localRotation.z}";
        }

        #endregion
    }
}