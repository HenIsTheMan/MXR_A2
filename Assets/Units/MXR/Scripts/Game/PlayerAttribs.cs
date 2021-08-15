using UnityEngine;

namespace MXR {
    internal sealed class PlayerAttribs: MonoBehaviour { //POD
        #region Fields
        #endregion

        #region Properties

        [field: SerializeField]
        internal float Spd {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal PlayerAttribs(): base() {
            Spd = 0.0f;
        }

        static PlayerAttribs() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}