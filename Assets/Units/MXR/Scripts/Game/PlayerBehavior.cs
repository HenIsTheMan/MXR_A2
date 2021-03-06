using MobfishCardboard;
using UnityEngine;

namespace MXR {
    internal sealed class PlayerBehavior: MonoBehaviour {
        #region Fields

        private Vector3 eulerAngles;

        [SerializeField]
        private PlayerAttribs playerAttribs;

        [SerializeField]
        private bool shldLimitX;

        [SerializeField]
        private bool shldLimitY;

        [SerializeField]
        private bool shldLimitZ;

        [SerializeField]
        private Vector2 xLocalEulerAngleMinMax;

        [SerializeField]
        private Vector2 yLocalEulerAngleMinMax;

        [SerializeField]
        private Vector2 zLocalEulerAngleMinMax;

        [SerializeField]
        private float targetRotationXMultiplier;

        [SerializeField]
        private float targetRotationYMultiplier;

        [SerializeField]
        private float targetRotationZMultiplier;

        private float mouseDownTime;

        [SerializeField]
        private float mouseDownTimeThreshold;

        private BulletProjectileBehavior megaBulletProjectileBehavior;

        [SerializeField]
        private float distOfBulletFromPlayer;

        [SerializeField]
        private float megaBulletScaleMultiplier;

        [SerializeField]
        private float megaBulletMaxScaleFactor;

        [SerializeField]
        private float dmgPerUnitScale;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal PlayerBehavior(): base() {
            eulerAngles = Vector3.zero;
            playerAttribs = null;

            shldLimitX = true;
            shldLimitY = true;
            shldLimitZ = true;

            xLocalEulerAngleMinMax = Vector2.zero;
            yLocalEulerAngleMinMax = Vector2.zero;
            zLocalEulerAngleMinMax = Vector2.zero;

            targetRotationXMultiplier = 0.0f;
            targetRotationYMultiplier = 0.0f;
            targetRotationZMultiplier = 0.0f;

            mouseDownTime = 0.0f;
            mouseDownTimeThreshold = 0.0f;

            megaBulletProjectileBehavior = null;
            distOfBulletFromPlayer = 0.0f;
            megaBulletScaleMultiplier = 0.0f;
            megaBulletMaxScaleFactor = 0.0f;
            dmgPerUnitScale = 0.0f;
        }

        static PlayerBehavior() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            eulerAngles = transform.localEulerAngles;
        }

        private void FixedUpdate() {
            if(!GameControl.globalObj.hasGameStarted) {
                return;
            }

            playerAttribs.Spd += playerAttribs.AccelFactor * Time.fixedDeltaTime;
            playerAttribs.Spd = Mathf.Clamp(playerAttribs.Spd, playerAttribs.MinSpd, playerAttribs.MaxSpd);

            transform.localPosition
                += playerAttribs.Dir
                * playerAttribs.Spd
                * Time.fixedDeltaTime;

            if(Application.isEditor) {
                if(Input.GetMouseButton(1)) {
                    PlayerRotation();
                }
            } else {
                CardboardHeadTracker.UpdatePose();
                PlayerRotation();
            }
        }

        private void Update() {
            if(!GameControl.globalObj.hasGameStarted) {
                return;
            }

            playerAttribs.Dir = transform.rotation * Vector3.forward;

            if(Input.GetMouseButton(0)) {
                mouseDownTime += Time.deltaTime;

                if(megaBulletProjectileBehavior == null) {
                    if(mouseDownTime > mouseDownTimeThreshold) {
                        megaBulletProjectileBehavior
                            = playerAttribs.MegaBulletPool.ActivateObj().GetComponent<BulletProjectileBehavior>();

                        megaBulletProjectileBehavior.myTransform.position
                            = transform.position + playerAttribs.Dir * distOfBulletFromPlayer;

                        megaBulletProjectileBehavior.BulletPool = playerAttribs.MegaBulletPool;
                        megaBulletProjectileBehavior.Dir = Vector3.zero;
                    }
                } else {
                    megaBulletProjectileBehavior.myTransform.position
                        = transform.position + playerAttribs.Dir * distOfBulletFromPlayer;

                    megaBulletProjectileBehavior.myTransform.localScale += Vector3.one * Time.deltaTime * megaBulletScaleMultiplier;
                    megaBulletProjectileBehavior.myTransform.localScale = Vector3.Min(
                        Vector3.one * megaBulletMaxScaleFactor,
                        megaBulletProjectileBehavior.myTransform.localScale
                    );

                    megaBulletProjectileBehavior.BulletProjectileData.Dmg = dmgPerUnitScale * megaBulletProjectileBehavior.myTransform.localScale.x;
                }
            }

            if(Input.GetMouseButtonUp(0)) {
                if(mouseDownTime <= mouseDownTimeThreshold) { //Shoot regular bullet
					BulletProjectileBehavior regularBulletProjectileBehavior
						= playerAttribs.RegularBulletPool.ActivateObj().GetComponent<BulletProjectileBehavior>();

					regularBulletProjectileBehavior.myTransform.position = transform.position + playerAttribs.Dir * distOfBulletFromPlayer;
					regularBulletProjectileBehavior.BulletPool = playerAttribs.RegularBulletPool;
					regularBulletProjectileBehavior.Dir = playerAttribs.Dir;
					regularBulletProjectileBehavior.PlayerSpd = playerAttribs.Spd;
				} else {
                    megaBulletProjectileBehavior.Dir = playerAttribs.Dir;
                    megaBulletProjectileBehavior.PlayerSpd = playerAttribs.Spd;

                    megaBulletProjectileBehavior = null;
                }

                mouseDownTime = 0.0f;
            }
        }

        private void OnCollisionEnter() {
            General.Console.Log("OnCollisionEnter()");

            LoadGameEndScene.globalObj.OnGameEnd(false);
        }

        private void OnTriggerExit() {
            General.Console.Log("OnTriggerExit()");
        }

        #endregion

        private void PlayerRotation() {
            if(shldLimitX) {
                eulerAngles.x = Mathf.Clamp(
                    eulerAngles.x - CalcUpDownFactor() * targetRotationXMultiplier * Time.fixedDeltaTime,
                    xLocalEulerAngleMinMax.x,
                    xLocalEulerAngleMinMax.y
                );
            } else {
                eulerAngles.x -= CalcUpDownFactor() * targetRotationXMultiplier * Time.fixedDeltaTime;
            }

            if(shldLimitY) {
                eulerAngles.y = Mathf.Clamp(
                    eulerAngles.y + CalcTurnFactor() * targetRotationYMultiplier * Time.fixedDeltaTime,
                    yLocalEulerAngleMinMax.x,
                    yLocalEulerAngleMinMax.y
                );
            } else {
                eulerAngles.y += CalcTurnFactor() * targetRotationYMultiplier * Time.fixedDeltaTime;
            }

            if(shldLimitZ) {
                eulerAngles.z = Mathf.Clamp(
                    eulerAngles.z - CalcTurnFactor() * targetRotationZMultiplier * Time.fixedDeltaTime,
                    zLocalEulerAngleMinMax.x,
                    zLocalEulerAngleMinMax.y
                );
            } else {
                eulerAngles.z -= CalcTurnFactor() * targetRotationZMultiplier * Time.fixedDeltaTime;
            }

            transform.localEulerAngles = eulerAngles;
        }

        private float CalcUpDownFactor() {
            if(Application.isEditor) {
                return Input.GetAxis("Mouse Y");
            }

            CardboardHeadTracker.trackerUnityRotation.ToAngleAxis(out float angle, out Vector3 axis);
            return -axis.x * Mathf.Abs(angle) * 0.027f;
        }

        private float CalcTurnFactor() {
            if(Application.isEditor) {
                return Input.GetAxis("Mouse X");
            }

            CardboardHeadTracker.trackerUnityRotation.ToAngleAxis(out float angle, out Vector3 axis);
            return -axis.z * Mathf.Abs(angle) * 0.027f;
        }

        private float Angle0To360(float angle) {
            float result = angle - Mathf.CeilToInt(angle / 360.0f) * 360.0f;

            if(result < 0.0f) {
                result += 360.0f;
            }

            return result;
        }
    }
}