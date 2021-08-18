using UnityEngine;

namespace MXR {
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjs/ProjectileData", order = 0)]
    internal sealed class ProjectileData: ScriptableObject { //POD
        #region Fields

        private float dmgOG;
        private float minSpdOG;
        private float maxSpdOG;
        private float accelFactorOG;
        private float lifetimeOG;

        #endregion

        #region Properties

        [field: SerializeField]
        internal float Dmg {
            get;
            set;
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

        [field: SerializeField]
        internal float Lifetime {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal ProjectileData(): base() {
        }

        static ProjectileData() {
        }

        #endregion

        internal void Init() {
            dmgOG = Dmg;
            minSpdOG = MinSpd;
            maxSpdOG = MaxSpd;
            accelFactorOG = AccelFactor;
            lifetimeOG = Lifetime;
        }

        internal void Reset() {
            Dmg = dmgOG;
            MinSpd = minSpdOG;
            MaxSpd = maxSpdOG;
            AccelFactor = accelFactorOG;
            Lifetime = lifetimeOG;
        }
    }
}