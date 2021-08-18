using MXR.Anim;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MXR.General {
	internal sealed partial class LoadSceneImmediate: MonoBehaviour {
		public static void OnIntroClick() {
			if(globalObj.canClickOnIntro && UnityEngine.SceneManagement.SceneManager.GetSceneByName("MenuScene").isLoaded) {
				GameObject camGO = GameObject.Find("MenuCam");
				Camera camComponent = camGO.GetComponent<Camera>();
				camComponent.enabled = true;

				Camera mainCam = Camera.main;
				camComponent.depth = mainCam.depth - 0.1f;
				camComponent.clearFlags = CameraClearFlags.SolidColor;
				camComponent.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;

				//PtrManager.globalObj.camComponent = camComponent;

				mainCam.clearFlags = CameraClearFlags.Nothing;

				RectTransformScaleAnim[] scaleAnims = GameObject.Find("IntroBg").GetComponents<RectTransformScaleAnim>();
				foreach(RectTransformScaleAnim scaleAnim in scaleAnims) {
					scaleAnim.IsUpdating = true;
				}

				GameObject instructionTextGO = GameObject.Find("InstructionText");
				instructionTextGO.GetComponent<TextPtrOverLineOfWords>().enabled = false;

				scaleAnims = instructionTextGO.GetComponents<RectTransformScaleAnim>();
				foreach(RectTransformScaleAnim scaleAnim in scaleAnims) {
					scaleAnim.IsUpdating = false;
				}

				GameObject gameTitleTextGO = GameObject.Find("GameTitleText");
				gameTitleTextGO.GetComponent<TextMtlFlashAcrossAnim>().IsUpdating = false;
				gameTitleTextGO.GetComponent<TMP_Text>().fontMaterial = (Material)Resources.Load("RaiderCrusaderSDF", typeof(Material));

				TextMtlBarsFadeAnim[] fadeAnims = GameObject.Find("NonRaycastGrp").GetComponentsInChildren<TextMtlBarsFadeAnim>();
				foreach(TextMtlBarsFadeAnim fadeAnim in fadeAnims) {
					fadeAnim.IsUpdating = true;
				}

				fadeAnims[0].animEndDelegate += () => {
					globalObj.sceneManager.UnloadScene(
						UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
						UnloadSceneTypes.UnloadSceneType.UnloadAllEmbeddedSceneObjs,
						null
					);
				};

				globalObj.canClickOnIntro = false;
			}
		}
	}
}