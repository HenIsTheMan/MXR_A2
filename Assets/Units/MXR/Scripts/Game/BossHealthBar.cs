using MXR.Anim;
using MXR.Math;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MXR {
    internal sealed class BossHealthBar: MonoBehaviour {
        #region Fields

        [SerializeField]
        private EnemyAttribs bossEnemyAttribs;

        [SerializeField]
        private ImgFillAmtAnim myAnim;

        [SerializeField]
        private Image myImg;

        [ColorUsage(false, false), SerializeField]
        private Color healthBarGoingBadColor;

        private Queue<Vector2> myAnimQueue;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal BossHealthBar(): base() {
            bossEnemyAttribs = null;

			myAnim = null;
            myImg = null;

            healthBarGoingBadColor = Color.white;

            myAnimQueue = null;
        }

		static BossHealthBar() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            myAnimQueue = new Queue<Vector2>();

            myAnim.img.color = healthBarGoingBadColor;
        }

        private void Update() {
            if(!myAnim.IsUpdating && myAnimQueue.Any()) {
                Vector2 vec = myAnimQueue.Dequeue();
                myAnim.startFillAmt = vec.x;
                myAnim.endFillAmt = vec.y;

                if(!Mathf.Approximately(myAnim.startFillAmt, myAnim.endFillAmt)) {
                    myAnim.IsUpdating = true;
                }
            }

            if(!myAnim.IsUpdating && myAnimQueue.Count == 0) {
                myAnim.startFillAmt = myImg.fillAmount;
                myImg.fillAmount = bossEnemyAttribs.CurrHealth / bossEnemyAttribs.MaxHealth;
                myAnim.endFillAmt = myImg.fillAmount;
            } else {
                float temp = myImg.fillAmount;
                myImg.fillAmount = bossEnemyAttribs.CurrHealth / bossEnemyAttribs.MaxHealth;
                myAnimQueue.Enqueue(new Vector2(temp, myImg.fillAmount));
            }

            myImg.color = Color.HSVToRGB(Val.Lerp(0.0f, 0.34f, myImg.fillAmount), 1.0f, 1.0f);

            if(!Mathf.Approximately(myAnim.startFillAmt, myAnim.endFillAmt)) {
                myAnim.IsUpdating = true;
            }
        }

        #endregion
    }
}