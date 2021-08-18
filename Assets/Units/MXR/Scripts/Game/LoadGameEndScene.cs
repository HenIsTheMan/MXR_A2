using MXR.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MXR.General.LoadSceneTypes;

namespace MXR {
    internal sealed class LoadGameEndScene: MonoBehaviour {
        #region Fields

        [SerializeField]
        private string winText;

        [SerializeField]
        private string loseText;

        internal static LoadGameEndScene globalObj; //LOL

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal LoadGameEndScene() : base() {
            winText = string.Empty;
            loseText = string.Empty;
        }

        static LoadGameEndScene() {
            globalObj = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            globalObj = this;
        }

        #endregion

        internal void OnGameEnd(bool hasWon) {
            SceneManager sceneManager = SceneManager.globalObj;

            sceneManager.LoadScene("GameEndScene", LoadSceneType.Single, () => {
                GameObject.Find("GameEndContentText").GetComponent<TMP_Text>().text = hasWon ? winText : loseText;

                GameObject backButtonGO = GameObject.Find("BackButton");
                Button button = backButtonGO.GetComponent<Button>();
                button.onClick.AddListener(() => {
                    backButtonGO.GetComponent<CommonSceneTransitionButton>().OnClick();
                    //button.GetComponent<PlayAudio>().PlaySound("Press0");
                });
            });
        }
    }
}