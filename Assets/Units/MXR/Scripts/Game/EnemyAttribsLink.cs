using UnityEngine;

namespace MXR {
    internal sealed class EnemyAttribsLink: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties

        [field: SerializeField]
        internal EnemyAttribs MyEnemyAttribs {
            get;
            private set;
        }

        #endregion

        #region Ctors and Dtor

        internal EnemyAttribsLink(): base() {
            MyEnemyAttribs = null;
        }

        static EnemyAttribsLink() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}