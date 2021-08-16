using UnityEngine;

namespace MXR.Anim {
    internal sealed class SkyCycleAnim: AbstractAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Material startSkyMtl;

		[HideInInspector, SerializeField]
		internal Material endSkyMtl;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal SkyCycleAnim(): base() {
			startSkyMtl = null;
			endSkyMtl = null;
        }

        static SkyCycleAnim() {
        }

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		protected override void UpdateAnim() {
			RenderSettings.skybox.Lerp(startSkyMtl, endSkyMtl, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
		}
	}
}