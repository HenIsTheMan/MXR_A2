using UnityEngine;
using UnityEngine.EventSystems;

namespace Vid360 {
    internal sealed class RotateCamOnDrag: MonoBehaviour {
        #region Fields

        [SerializeField]
        private Transform camTransform;

        [SerializeField]
        private float magnitude;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal RotateCamOnDrag(): base() {
            camTransform = null;

            magnitude = 0.0f;
        }

        static RotateCamOnDrag() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Awake() {
            EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((eventData) => {
                OnDragHandler((PointerEventData)eventData);
            });

            eventTrigger.triggers.Add(dragEntry);
        }

        #endregion

        private void OnDragHandler(PointerEventData ptrEventData) {
            if(ptrEventData.delta.x < 0.0f) {
                camTransform.rotation *= Quaternion.Euler(0.0f, magnitude, 0.0f);
            } else {
                camTransform.rotation *= Quaternion.Euler(0.0f, -magnitude, 0.0f);
            }
        }
    }
}