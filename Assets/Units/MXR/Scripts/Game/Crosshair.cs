using MXR.Anim;
using UnityEngine;

namespace MXR {
    internal sealed class Crosshair: MonoBehaviour {
        #region Fields

        private bool canAnimate;
        private bool shldAnimate;

        [SerializeField]
        private float maxDist;

        [SerializeField]
        private string targetTag;

        [SerializeField]
        private AbstractAnim[] onHitAnims;

        [SerializeField]
        private AbstractAnim[] onNotHitAnims;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal Crosshair(): base() {
            canAnimate = false;
            shldAnimate = false;

            maxDist = 0.0f;
            targetTag = string.Empty;
            onHitAnims = System.Array.Empty<AbstractAnim>();
            onNotHitAnims = System.Array.Empty<AbstractAnim>();
        }

        static Crosshair() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(Physics.Raycast(transform.position, transform.rotation * -transform.forward, out RaycastHit hit, maxDist)) {
                if(hit.transform.tag == targetTag) {
                    canAnimate = true;
                    shldAnimate = false;

                    foreach(AbstractAnim anim in onNotHitAnims) {
                        anim.IsUpdating = false;
                    }

                    foreach(AbstractAnim anim in onHitAnims) {
                        anim.IsUpdating = true;
                    }
                } else {
                    shldAnimate = true;
                }
            } else {
                shldAnimate = true;
            }

            if(canAnimate && shldAnimate) {
                foreach(AbstractAnim anim in onHitAnims) {
                    anim.IsUpdating = false;
                }

                foreach(AbstractAnim anim in onNotHitAnims) {
                    anim.IsUpdating = true;
                }

                canAnimate = false;
            }
        }

        #endregion
    }
}