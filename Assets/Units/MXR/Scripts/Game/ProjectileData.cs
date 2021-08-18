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
            dmgOG = 0.0f;
            minSpdOG = 0.0f;
            maxSpdOG = 0.0f;
            accelFactorOG = 0.0f;
            lifetimeOG = 0.0f;

            Dmg = 0.0f;
            Spd = 0.0f;
            MinSpd = 0.0f;
            MaxSpd = 0.0f;
            AccelFactor = 0.0f;
            Lifetime = 0.0f;
        }

        static ProjectileData() {
        }

        #endregion

        private void Awake() {
            dmgOG = Dmg;
            minSpdOG = MinSpd;
            maxSpdOG = MaxSpd;
            accelFactorOG = AccelFactor;
            lifetimeOG = Lifetime;
        }

        private void OnDisable() {
            Dmg = dmgOG;
            MinSpd = minSpdOG;
            MaxSpd = maxSpdOG;
            AccelFactor = accelFactorOG;
            Lifetime = lifetimeOG;
        }
    }
}