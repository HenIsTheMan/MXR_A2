using UnityEngine;

namespace MXR {
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjs/ProjectileData", order = 0)]
    internal sealed class ProjectileData: ScriptableObject {
        #region Fields

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

        [field: SerializeField]
        internal float Lifetime {
            get;
            private set;
        }

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ProjectileData(): base() {
            Spd = 0.0f;
            MinSpd = 0.0f;
            MaxSpd = 0.0f;
            AccelFactor = 0.0f;
            Lifetime = 0.0f;
        }

        static ProjectileData() {
        }

        #endregion
    }
}