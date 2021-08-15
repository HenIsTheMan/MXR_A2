using MXR.Anim;
using TMPro;
using UnityEngine;

namespace MXR.General {
    internal sealed partial class TextPtrOverLineOfWords: MonoBehaviour {
		public static void PtrNotOverInstructionText() {
			GameObject instructionText = globalObj.tmpTextComponent.gameObject;
			RectTransformScaleAnim[] scaleAnimScripts = instructionText.GetComponents<RectTransformScaleAnim>();

			scaleAnimScripts[0].IsUpdating = true;
			scaleAnimScripts[1].IsUpdating = true;
			instructionText.GetComponent<TextMeshProUGUI>().color = Color.white;
		}
	}
}