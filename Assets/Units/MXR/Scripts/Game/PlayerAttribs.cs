using UnityEngine;

namespace MXR {
    internal sealed class PlayerAttribs: MonoBehaviour { //POD
        #region Fields

        [SerializeField]
        private float spd;

        #endregion

        #region Properties

        internal float Spd {
            get => spd;
            private set => spd = value;
        }

        #endregion

        #region Ctors and Dtor

        internal PlayerAttribs(): base() {
            spd = 0.0f;
        }

        static PlayerAttribs() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}