namespace MXR.Math {
	internal static partial class Easing: object {
		#region Fields
		#endregion

		#region Properties
		#endregion

		public static float EaseInOutCubic(float x) {
			return x < 0.5f ? 4.0f * x * x * x : 1.0f - UnityEngine.Mathf.Pow(-2.0f * x + 2.0f, 3.0f) * 0.5f;
		}
	}
}