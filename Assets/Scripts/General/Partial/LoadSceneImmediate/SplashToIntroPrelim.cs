using MXR.Anim;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MXR.General {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void SplashToIntroPrelim() {
			GameObject.Find("GenesisAlpha0").GetComponent<RectTransformPathAnim>().animEndDelegate += SubSplashToIntroPrelim;
		}

		private static void SubSplashToIntroPrelim() {
			if(!globalObj.canClickOnSplash) {
				return;
			}

			BloomAnim[] bloomAnims = GameObject.Find("BloomVol").GetComponents<BloomAnim>();
			bloomAnims[0].IsUpdating = true;
			bloomAnims[1].IsUpdating = true;

			bloomAnims[1].animPrePeriodicDelegate += () => {
				if(UnityEngine.SceneManagement.SceneManager.GetSceneByName("IntroScene").isLoaded) {
					bloomAnims[0].IsUpdating = false;
					bloomAnims[1].IsUpdating = false;
				}
			};

			bloomAnims[1].animEndDelegate = () => {
				if(!globalObj.canClickOnSplash) {
					return;
				}

				GameObject proxyCamGO = GameObject.Find("ProxyCam");

				RectTransformRotateAnim rectTransformRotateAnim = proxyCamGO.GetComponent<RectTransformRotateAnim>();

				rectTransformRotateAnim.IsUpdating = true;
				proxyCamGO.GetComponents<RectTransformScaleAnim>()[1].IsUpdating = true;

				rectTransformRotateAnim.animEndDelegate += SubSplashToIntroFadeTransitionPrelim;
			};
		}

		private static void SubSplashToIntroFadeTransitionPrelim() {
			globalObj.canClickOnSplash = false;

			//AudioManager.globalObj.PlayMusicFadeIn("Theme" + Random.Range(0, 3), 2.0f);

			GameObject bloomVolGO = GameObject.Find("BloomVol");

			BloomAnim[] bloomAnims = bloomVolGO.GetComponents<BloomAnim>();
			bloomAnims[0].IsUpdating = false;
			bloomAnims[1].IsUpdating = false;

			_ = bloomVolGO.GetComponent<Volume>().sharedProfile.TryGet(out Bloom bloom);
			bloom.intensity.value = 4.0f;
			bloom.scatter.value = 0.7f;

			GameObject camGO = GameObject.Find("IntroCam");
			Camera camComponent = camGO.GetComponent<Camera>();
			camComponent.enabled = true;
			camGO.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;

			CanvasGrpFadeAnim canvasGrpFadeAnim = GameObject.Find("IntroCanvas").GetComponent<CanvasGrpFadeAnim>();
			GameObject.Find("SplashCanvas").GetComponent<CanvasGrpFadeAnim>().IsUpdating = true;
			canvasGrpFadeAnim.IsUpdating = true;

			canvasGrpFadeAnim.animPreMidDelegate += () => {
				GameObject.Find("GameTitleText").GetComponent<TextMtlFlashAcrossAnim>().IsUpdating = true;

				GameObject instructionTextGO = GameObject.Find("InstructionText");
				RectTransformScaleAnim[] scaleAnims = instructionTextGO.GetComponents<RectTransformScaleAnim>();
				foreach(RectTransformScaleAnim scaleAnim in scaleAnims) {
					scaleAnim.IsUpdating = true;
				}
			};

			canvasGrpFadeAnim.animEndDelegate += () => {
				camComponent.clearFlags = CameraClearFlags.SolidColor;

				globalObj.sceneManager.UnloadScene(
					UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
					UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
					null
				);
			};
		}
	}
}