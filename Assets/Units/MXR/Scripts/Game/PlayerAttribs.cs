using MXR.General;
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

        internal float AccelFactor {
            get;
            set;
        }

        [field: SerializeField]
        internal float RegularAccelFactor {
            get;
            private set;
        }

        [field: SerializeField]
        internal float GoingUpAccelFactor {
            get;
            private set;
        }

        [field: SerializeField]
        internal float GoingDownAccelFactor {
            get;
            private set;
        }

        [field: SerializeField]
        internal float TurningAccelFactor {
            get;
            private set;
        }

        [field: SerializeField]
        internal float UpDownErrorMargin {
            get;
            private set;
        }

        [field: SerializeField]
        internal float TurningErrorMargin {
            get;
            private set;
        }

        [field: SerializeField]
        internal ObjPool BulletPool {
            get;
            private set;
        }

        [field: SerializeField]
        internal int BulletPoolSize {
            get;
            private set;
        }

        [field: SerializeField]
        internal GameObject BulletPrefab {
            get;
            private set;
        }

        [field: SerializeField]
        internal Transform BulletParentTransform {
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
            RegularAccelFactor = 0.0f;
            GoingUpAccelFactor = 0.0f;
            GoingDownAccelFactor = 0.0f;
            TurningAccelFactor = 0.0f;

            UpDownErrorMargin = 0.0f;
            TurningErrorMargin = 0.0f;

            BulletPool = null;
            BulletPoolSize = 0;
            BulletPrefab = null;
            BulletParentTransform = null;
        }

        static PlayerAttribs() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnValidate() {
            MinSpd = Mathf.Max(0.0f, MinSpd);
            MaxSpd = Mathf.Max(0.0f, MaxSpd);

            UpDownErrorMargin = Mathf.Abs(UpDownErrorMargin);
            TurningErrorMargin = Mathf.Abs(TurningErrorMargin);
        }

        private void Awake() {
            Spd = MinSpd;
            AccelFactor = RegularAccelFactor;

            BulletPool.InitMe(BulletPoolSize, BulletPrefab, BulletParentTransform);
        }

        #endregion
    }
}