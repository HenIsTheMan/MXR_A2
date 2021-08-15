using UnityEngine;

namespace MXR {
    internal sealed class PlayerAttribs: MonoBehaviour { //POD
        #region Fields
        #endregion

        #region Properties

        internal Vector3 Dir {
            get;
            set;
        }

        [field: SerializeField]
        internal Transform MyTransform {
            get;
            private set;
        }

        [field: SerializeField]
        internal float Spd {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal PlayerAttribs(): base() {
            Dir = Vector3.forward;
            MyTransform = null;
            Spd = 0.0f;
        }

        static PlayerAttribs() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}