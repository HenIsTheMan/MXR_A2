using UnityEngine;

namespace MXR.General {
    [ExecuteInEditMode]
    internal sealed class TerrainOrigin: MonoBehaviour {
        private enum Alignment: byte {
            TopLeft,
            TopCenter,
            TopRight,
            MiddleLeft,
            MiddleCenter,
            MiddleRight,
            BottomLeft,
            BottomCenter,
            BottomRight,
            Amt
        }

        #region Fields

        private bool shldAdjust;

        [SerializeField]
        private Alignment alignment;

        [SerializeField]
        private Terrain terrain;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal TerrainOrigin(): base() {
            shldAdjust = false;

            alignment = Alignment.Amt;

            terrain = null;
        }

        static TerrainOrigin() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnValidate() {
            UnityEngine.Assertions.Assert.IsTrue(
                alignment != Alignment.Amt,
                "alignment != Alignment.Amt"
            );
        }

        private void Update() {
            if(!shldAdjust) {
                return;
            }

            Vector3 terrainSize = terrain.terrainData.size;

            switch(alignment) {
                case Alignment.TopLeft:
                    break;
                case Alignment.TopCenter:
                    break;
                case Alignment.TopRight:
                    break;
                case Alignment.MiddleLeft:
                    break;
                case Alignment.MiddleCenter:
                    break;
                case Alignment.MiddleRight:
                    break;
                case Alignment.BottomLeft:
                    break;
                case Alignment.BottomCenter:
                    break;
                case Alignment.BottomRight:
                    break;
            }
        }

        #endregion
    }
}