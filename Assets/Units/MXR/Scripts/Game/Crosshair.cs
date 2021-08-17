using MXR.Anim;
using UnityEngine;

namespace MXR {
    internal sealed class Crosshair: MonoBehaviour {
        #region Fields

        private Material mtl;

        [SerializeField]
        private Renderer myRenderer;

        private bool flag;
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
            mtl = null;
            myRenderer = null;

            flag = true;
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

        private void Awake() {
            mtl = myRenderer.material;
        }

        private void Update() {
            if(Physics.Raycast(transform.parent.position, transform.parent.rotation * transform.parent.forward, out RaycastHit hit, maxDist)) {
                if(hit.transform.tag == targetTag) {
                    canAnimate = true;
                    shldAnimate = false;

                    if(flag) {
                        mtl.color = Color.red; //Lame

                        foreach(AbstractAnim anim in onNotHitAnims) {
                            anim.IsUpdating = false;
                        }

                        foreach(AbstractAnim anim in onHitAnims) {
                            anim.IsUpdating = true;
                        }

                        flag = false;
                    }
                } else {
                    shldAnimate = true;
                }
            } else {
                shldAnimate = true;
            }

            if(canAnimate && shldAnimate) {
                if(!flag) {
                    mtl.color = Color.white; //Lame

                    foreach(AbstractAnim anim in onHitAnims) {
                        anim.IsUpdating = false;
                    }

                    foreach(AbstractAnim anim in onNotHitAnims) {
                        anim.IsUpdating = true;
                    }

                    canAnimate = false;
                    flag = true;
                }
            }
        }

        #endregion
    }
}