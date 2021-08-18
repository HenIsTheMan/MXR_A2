using TMPro;
using UnityEngine;

namespace MXR {
    internal sealed class GameControl: MonoBehaviour {
        #region Fields

        internal bool hasGameStarted; //Meh

        private float time;

        [SerializeField]
        private float gameStartDelay;

        [SerializeField]
        private TMP_Text gameCountdownText;

        internal static GameControl globalObj; //Meh

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal GameControl(): base() {
            hasGameStarted = false;
            time = 0.0f;
            gameStartDelay = 0.0f;
            gameCountdownText = null;
        }

        static GameControl() {
            globalObj = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            globalObj = this;

            time = gameStartDelay;
        }

        private void Update() {
            time -= Time.deltaTime;

            gameCountdownText.text = Mathf.Max(0, Mathf.CeilToInt(time)).ToString();

            if(time <= 0.0f) {
                hasGameStarted = true;

                gameCountdownText.enabled = false;

                enabled = false;
            }
        }

        #endregion
    }
}