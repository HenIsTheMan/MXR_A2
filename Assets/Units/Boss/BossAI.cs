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
        private AnimationCurve[] xAnimCurves;

        [SerializeField]
        private AnimationCurve[] yAnimCurves;

        [SerializeField]
        private AnimationCurve[] zAnimCurves;

        [SerializeField]
        private float xMultiplier;

        [SerializeField]
        private float yMultiplier;

        [SerializeField]
        private float zMultiplier;

        [SerializeField]
        private float smallDelay;

        private WaitForSeconds smallWaitForSeconds;

        [SerializeField]
        private float minPeriodicDelay;

        [SerializeField]
        private float maxPeriodicDelay;

        [SerializeField]
        private Vector3 minPos;

        [SerializeField]
        private Vector3 maxPos;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal BossAI(): base() {
            myTransform = null;

            animTime = 0.0f;
            animDuration = 0.0f;

            xAnimCurves = System.Array.Empty<AnimationCurve>();
            yAnimCurves = System.Array.Empty<AnimationCurve>();
            zAnimCurves = System.Array.Empty<AnimationCurve>();

            xMultiplier = 0.0f;
            yMultiplier = 0.0f;
            zMultiplier = 0.0f;

            smallDelay = 0.0f;
            smallWaitForSeconds = null;
            minPeriodicDelay = 0.0f;
            maxPeriodicDelay = 0.0f;
            minPos = Vector3.zero;
            maxPos = Vector3.zero;
        }

        static BossAI() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            smallWaitForSeconds = new WaitForSeconds(smallDelay);

            myTransform.localPosition = new Vector3(
                Random.Range(minPos.x, maxPos.x),
                Random.Range(minPos.y, maxPos.y),
                Random.Range(minPos.z, maxPos.z)
            );

            _ = StartCoroutine(nameof(MobilizeBoss));
        }

        #endregion

        private System.Collections.IEnumerator MobilizeBoss() {
            while(true) {
                yield return StartCoroutine(nameof(MoveBoss));

                yield return smallWaitForSeconds;

                myTransform.localPosition = new Vector3(
                    Random.Range(minPos.x, maxPos.x),
                    Random.Range(minPos.y, maxPos.y),
                    Random.Range(minPos.z, maxPos.z)
                );

                myTransform.localRotation = Quaternion.identity;

                yield return new WaitForSeconds(Random.Range(minPeriodicDelay, maxPeriodicDelay));
            }
        }

        private System.Collections.IEnumerator MoveBoss() {
            animTime = 0.0f;
            float time;

            Vector3 localPosOG = myTransform.localPosition;
            Quaternion localRotationOG = myTransform.localRotation;
            Vector3 lastLocalPos;

            AnimationCurve xAnimCurve = xAnimCurves[Random.Range(0, xAnimCurves.Length)];
            AnimationCurve yAnimCurve = yAnimCurves[Random.Range(0, yAnimCurves.Length)];
            AnimationCurve zAnimCurve = zAnimCurves[Random.Range(0, zAnimCurves.Length)];

            Vector3 localScale = myTransform.localScale;
            Vector3 transformForwardOG = myTransform.forward;

            float val = Mathf.PingPong(animTime, 2.0f) - 1.0f;

            while(animTime <= animDuration) {
                animTime += Time.deltaTime;
                time = Mathf.Min(1.0f, animTime / animDuration);

                lastLocalPos = myTransform.localPosition;
                myTransform.localPosition = localPosOG + localRotationOG * new Vector3(
                    xAnimCurve.Evaluate(time) * xMultiplier * localScale.x * val,
                    yAnimCurve.Evaluate(time) * yMultiplier * localScale.y,
                    zAnimCurve.Evaluate(time) * zMultiplier * localScale.z
                );

                myTransform.localRotation = Quaternion.FromToRotation(
                    transformForwardOG,
                    myTransform.localPosition - lastLocalPos
                ) * localRotationOG;

                yield return null;
            }
        }
    }
}