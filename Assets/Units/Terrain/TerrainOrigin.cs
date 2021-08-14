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

        [SerializeField]
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
            Vector3 terrainSizeHalved = terrainSize * 0.5f;
            float terrainLocalPosY = terrain.transform.localPosition.y;
            Console.Log($"Terrain size: {terrainSize}");

            switch(alignment) {
                case Alignment.TopLeft:
                    terrain.transform.localPosition = new Vector3(
                        0.0f,
                        terrainLocalPosY,
                        -terrainSize.z
                    );
                    break;
                case Alignment.TopCenter:
                    terrain.transform.localPosition = new Vector3(
                        -terrainSizeHalved.x,
                        terrainLocalPosY,
                        -terrainSize.z
                    );
                    break;
                case Alignment.TopRight:
                    terrain.transform.localPosition = new Vector3(
                        -terrainSize.x,
                        terrainLocalPosY,
                        -terrainSize.z
                    );
                    break;
                case Alignment.MiddleLeft:
                    terrain.transform.localPosition = new Vector3(
                        0.0f,
                        terrainLocalPosY,
                        -terrainSizeHalved.z
                    );
                    break;
                case Alignment.MiddleCenter:
                    terrain.transform.localPosition = new Vector3(
                        -terrainSizeHalved.x,
                        terrainLocalPosY,
                        -terrainSizeHalved.z
                    );
                    break;
                case Alignment.MiddleRight:
                    terrain.transform.localPosition = new Vector3(
                        -terrainSize.x,
                        terrainLocalPosY,
                        -terrainSizeHalved.z
                    );
                    break;
                case Alignment.BottomLeft:
                    terrain.transform.localPosition = Vector3.zero;
                    break;
                case Alignment.BottomCenter:
                    terrain.transform.localPosition = new Vector3(
                        -terrainSizeHalved.x,
                        terrainLocalPosY,
                        0.0f
                    );
                    break;
                case Alignment.BottomRight:
                    terrain.transform.localPosition = new Vector3(
                        -terrainSize.x,
                        terrainLocalPosY,
                        0.0f
                    );
                    break;
            }

            shldAdjust = false;
        }

        #endregion
    }
}