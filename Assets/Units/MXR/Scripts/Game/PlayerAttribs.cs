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

        internal float Spd {
            get;
            set;
        }

        [field: SerializeField]
        internal float MinSpd {
            get;
            private set;
        }

        [field: SerializeField]
        internal float MaxSpd {
            get;
            private set;
        }

        [field: SerializeField]
        internal float AccelFactor {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal PlayerAttribs(): base() {
            Dir = Vector3.forward;
            MyTransform = null;

            Spd = 0.0f;
            MinSpd = 0.0f;
            MaxSpd = 0.0f;
            AccelFactor = 0.0f;
        }

        static PlayerAttribs() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnValidate() {
            MinSpd = Mathf.Max(0.0f, MinSpd);
            MaxSpd = Mathf.Max(0.0f, MaxSpd);
        }

        private void Awake() {
            Spd = MinSpd;
        }

        #endregion
    }
}