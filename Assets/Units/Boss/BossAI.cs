using UnityEngine;

namespace MXR {
    internal sealed class BossAI: MonoBehaviour {
        #region Fields

        [SerializeField]
        private Transform myTransform;

        private float animTime;

        [SerializeField]
        private float animDuration;

        [SerializeField]
        private AnimationCurve xAnimCurve;

        [SerializeField]
        private AnimationCurve yAnimCurve;

        [SerializeField]
        private AnimationCurve zAnimCurve;

        [SerializeField]
        private float xMultiplier;

        [SerializeField]
        private float yMultiplier;

        [SerializeField]
        private float zMultiplier;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal BossAI(): base() {
            myTransform = null;

            animTime = 0.0f;
            animDuration = 0.0f;

            xAnimCurve = null;
            yAnimCurve = null;
            zAnimCurve = null;

            xMultiplier = 0.0f;
            yMultiplier = 0.0f;
            zMultiplier = 0.0f;
        }

        static BossAI() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            _ = StartCoroutine(nameof(MoveBoss));
        }

        #endregion

        private System.Collections.IEnumerator MoveBoss() {
            animTime = 0.0f;
            float time;
            Vector3 localPosOG = myTransform.localPosition;

            while(animTime <= animDuration) {
                animTime += Time.deltaTime;
                time = Mathf.Min(1.0f, animTime / animDuration);

                myTransform.localPosition = localPosOG + new Vector3(
                    xAnimCurve.Evaluate(time) * xMultiplier,
                    yAnimCurve.Evaluate(time) * yMultiplier,
                    zAnimCurve.Evaluate(time) * zMultiplier
                );

                yield return null;
            }
        }
    }
}