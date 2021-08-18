using UnityEngine;

namespace MXR.General {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void BootToSplashDone() {
			PlayerPrefs.SetInt("menuIndex", 0);
		}
	}
}