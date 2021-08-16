#if UNITY_EDITOR

using UnityEditor;

namespace MXR.Anim {
	[CustomEditor(typeof(SkyCycleAnim)), CanEditMultipleObjects]
	internal sealed class SkyCycleAnimEditor: AbstractAnimEditor {
		#region Fields
		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal SkyCycleAnimEditor(): base() {
		}

		static SkyCycleAnimEditor() {
		}

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		protected override void InitNames() {
			names = new string[]{
				"initControl",
				"isUpdating",
				"shldInitVals",
				"periodicDelay",
				"startTimeOffset",
				"animDuration",
				"countThreshold",
				"easingType",
				"startSkyMtl",
				"endSkyMtl"
			};
		}
	}
}

#endif