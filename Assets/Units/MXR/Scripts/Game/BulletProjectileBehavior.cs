using MXR.General;
using UnityEngine;

namespace MXR {
    internal sealed class BulletProjectileBehavior: MonoBehaviour {
        #region Fields

        [SerializeField]
        private ProjectileData bulletProjectileData;

        [SerializeField]
        internal Transform myTransform;

        [SerializeField]
        private LayerMask ignoreMe;

        #endregion

        #region Properties

        internal ObjPool BulletPool {
            private get;
            set;
        }

        internal Vector3 Dir {
            private get;
            set;
        }

        internal float PlayerSpd {
            private get;
            set;
        }

        internal ProjectileData BulletProjectileData {
            get => bulletProjectileData;
        }

        #endregion

        #region Ctors and Dtor

        internal BulletProjectileBehavior(): base() {
            bulletProjectileData = null;
            myTransform = null;

            BulletPool = null;
        }

        static BulletProjectileBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnEnable() {
            bulletProjectileData.Init();

            _ = StartCoroutine(nameof(ProjectileLifetime));
        }

        private void FixedUpdate() {
			Vector3 oldPos = myTransform.position;

            bulletProjectileData.Spd += bulletProjectileData.AccelFactor * Time.fixedDeltaTime;
            bulletProjectileData.Spd = Mathf.Clamp(bulletProjectileData.Spd, bulletProjectileData.MinSpd, bulletProjectileData.MaxSpd);

            myTransform.position += Dir * (bulletProjectileData.Spd + PlayerSpd) * Time.fixedDeltaTime;
		}

        private void OnCollisionEnter(Collision collision) {
            BulletPool.DeactivateObj(gameObject);

            if(collision.gameObject.CompareTag("Boss")) {
                collision.gameObject.GetComponent<EnemyAttribsLink>().MyEnemyAttribs.CurrHealth -= bulletProjectileData.Dmg;
            }
        }

        private void OnDisable() {
            bulletProjectileData.Reset();
        }

        #endregion

        private System.Collections.IEnumerator ProjectileLifetime() {
            yield return new WaitForSeconds(bulletProjectileData.Lifetime);

            BulletPool.DeactivateObj(gameObject);
        }
    }
}