using UnityEngine;

namespace MXR.General {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void OnSplashClick() {
			if(globalObj.canClickOnSplash
				&& UnityEngine.SceneManagement.SceneManager.GetSceneByName("IntroScene").isLoaded
			) {
				GameObject.Find("ProxyCam").SetActive(false);

				SubSplashToIntroFadeTransitionPrelim();
			}
		}
	}
}