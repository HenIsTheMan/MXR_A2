using UnityEngine;

namespace MXR {
    internal sealed class EnemyAttribs: MonoBehaviour { //POD
        #region Fields

        private float currHealth;

        #endregion

        #region Properties

        internal float CurrHealth {
            get => currHealth;
            set {
                currHealth = Mathf.Clamp(value, 0.0f, MaxHealth);

                if(Mathf.Approximately(currHealth, 0.0f)) {
                    LoadGameEndScene.globalObj.OnGameEnd(true);
                }
            }
        }

        [field: SerializeField]
        internal float MaxHealth {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal EnemyAttribs(): base() {
            currHealth = 0.0f;
            MaxHealth = 0.0f;
        }

        static EnemyAttribs() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            CurrHealth = MaxHealth;
        }

        #endregion
    }
}