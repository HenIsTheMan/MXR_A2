using MXR.General;
using UnityEngine;

namespace MXR {
    internal sealed class BulletProjectileBehavior: MonoBehaviour {
        #region Fields

        [SerializeField]
        private ProjectileData bulletProjectileData;

        [SerializeField]
        private Transform myTransform;

        [SerializeField]
        private LayerMask ignoreMe;

        #endregion

        #region Properties

        internal ObjPool BulletPool {
            private get;
            set;
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
            _ = StartCoroutine(nameof(ProjectileLifetime));
        }

        private void FixedUpdate() {
            Vector3 oldPos = transform.position;
            transform.position += transform.forward * bulletProjectileData.Spd * Time.fixedDeltaTime;

            if(Physics.Linecast(oldPos, transform.position, out RaycastHit hitInfo, ~ignoreMe, QueryTriggerInteraction.Ignore)) {
                BulletPool.DeactivateObj(gameObject);

                if(hitInfo.transform.CompareTag("Boss")) {
                    Console.Log("Hit!");
                }
            }
        }

        #endregion

        private System.Collections.IEnumerator ProjectileLifetime() {
            yield return new WaitForSeconds(bulletProjectileData.Lifetime);

            BulletPool.DeactivateObj(gameObject);
        }
    }
}