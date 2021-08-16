using UnityEngine;

namespace MXR.General {
    internal sealed class SkyMtlControl: MonoBehaviour {
		#region Fields

		[SerializeField]
		private float rotationVel;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal SkyMtlControl(): base() {
			rotationVel = 0.0f;
		}

        static SkyMtlControl() {
        }

		#endregion

		#region Unity User Callback Event Funcs

		private void FixedUpdate() {
			RenderSettings.skybox.SetFloat("_Rotation", Time.fixedTime * rotationVel);
		}

		private void OnDisable() {
			RenderSettings.skybox.SetFloat("_Rotation", Time.fixedTime * rotationVel);
		}

		#endregion
	}
}