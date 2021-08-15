using MobfishCardboard;
using UnityEngine;
using UnityEngine.UI;

namespace MXR {
    public class GoogleCardboardUIOverlay: MonoBehaviour {
        private static GoogleCardboardUIOverlay instance;

        [Header("NoVRViewElements")]
        public Button switchVRButton;

        [Header("VRViewElements")]
        public Button scanQRButton;
        public Button closeButton;
        public GameObject splitLine;

        [Header("Options")]
        [SerializeField]
        private bool dontDestroyAndSingleton;

        private bool overlayIsOpen;

        private void Awake() {
            if(dontDestroyAndSingleton) {
                if(instance == null) {
                    DontDestroyOnLoad(gameObject);
                    instance = this;
                } else if(instance != this) {
                    Destroy(gameObject);
                    return;
                }
            }

            scanQRButton.onClick.AddListener(ScanQRCode);
            switchVRButton.onClick.AddListener(SwitchVRView);
            closeButton.onClick.AddListener(CloseVRView);
        }

        // Start is called before the first frame update
        void Start() {
            VRViewChanged();
            CardboardManager.deviceParamsChangeEvent += TriggerRefresh;
            CardboardManager.enableVRViewChangedEvent += VRViewChanged;
        }


        private void OnDestroy() {
            CardboardManager.deviceParamsChangeEvent -= TriggerRefresh;
            CardboardManager.enableVRViewChangedEvent -= VRViewChanged;
        }

        private void ScanQRCode() {
            CardboardManager.ScanQrCode();
            SetEnableQROverlay(true);
        }

        private void SwitchVRView() {
            CardboardManager.SetVRViewEnable(!CardboardManager.enableVRView);
        }

        private void CloseVRView() {
            CardboardManager.SetVRViewEnable(false);
        }

        private void VRViewChanged() {
            SetEnableQROverlay(false);
            SetUIStatus(CardboardManager.enableVRView);
        }

        private void SetUIStatus(bool isVREnabled) {
            scanQRButton.gameObject.SetActive(isVREnabled);
            closeButton.gameObject.SetActive(isVREnabled);
            splitLine.SetActive(isVREnabled);

            switchVRButton.gameObject.SetActive(!isVREnabled);

            if(isVREnabled)
                TriggerRefresh();
        }

        private void SetEnableQROverlay(bool shouldEnable) {
            overlayIsOpen = shouldEnable;
        }

        private void ContinueClicked() {
            TriggerRefresh();

            SetEnableQROverlay(false);
        }

        private void TriggerRefresh() {
            //CardboardManager.RefreshParameters();

            if(CardboardManager.enableVRView && !CardboardManager.profileAvailable) {
                SetEnableQROverlay(true);
            }
        }
    }
}