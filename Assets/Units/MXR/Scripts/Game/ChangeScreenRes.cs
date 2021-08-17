using UnityEngine;

namespace MXR {
    internal sealed class ChangeScreenRes: MonoBehaviour {
        #region Fields

        [SerializeField]
        private float widthMultiplier;

        [SerializeField]
        private float heightMultiplier;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ChangeScreenRes(): base() {
            widthMultiplier = 0.0f;
            heightMultiplier = 0.0f;
        }

        static ChangeScreenRes() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            Screen.SetResolution((int)(Screen.width * widthMultiplier), (int)(Screen.height * heightMultiplier), FullScreenMode.Windowed);
        }

        #endregion
    }
}