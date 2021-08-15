using MobfishCardboard;
using UnityEngine;

namespace MXR {
    internal sealed class RecenterCam: MonoBehaviour {
        #region Fields

        [SerializeField]
        private bool shldCallInStart;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal RecenterCam(): base() {
            shldCallInStart = false;
        }

        static RecenterCam() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Start() {
            if(shldCallInStart) {
                RecenterCamFunc();
            }
        }

        #endregion

        internal void RecenterCamFunc() {
            CardboardManager.RecenterCamera();
        }
    }
}