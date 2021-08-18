using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MXR.General {
	internal sealed partial class CommonSceneTransitionButton: MonoBehaviour {
		public static void MenuScenePrep() {
			SceneManager.globalObj.UnloadScene(
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
				UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
				null
			);

			//NetworkManager.globalObj.Disconnect();

			Time.timeScale = 1.0f;

			Camera camComponent = GameObject.Find("MenuCam").GetComponent<Camera>();
			camComponent.enabled = true;
			camComponent.clearFlags = CameraClearFlags.SolidColor;
			camComponent.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;

			//PtrManager.globalObj.camComponent = camComponent;

			GameObject toneMappingVolGO = GameObject.Find("ToneMappingVol");
			if(toneMappingVolGO != null) {
				_ = toneMappingVolGO.GetComponent<Volume>().sharedProfile.TryGet(out Tonemapping toneMapping);
				toneMapping.active = true;
			}

			GameObject starsGO = GameObject.Find("Stars");
			if(starsGO != null) {
				starsGO.GetComponent<Instancer>().shldDraw = false;
			}

			//BlurRendererFeatureControl.globalObj.BlurRendererFeatureSetActive(false);
		}
	}
}