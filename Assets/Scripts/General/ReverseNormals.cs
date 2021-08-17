using UnityEngine;

namespace MXR.General {
    [ExecuteInEditMode]
    internal sealed class ReverseNormals: MonoBehaviour {
        #region Fields

        [SerializeField]
        private bool shldReverseNormals;

        #endregion

        #region Properties
        #endregion

        #region Ctors and Dtor

        internal ReverseNormals(): base() {
            shldReverseNormals = false;
        }

        static ReverseNormals() {
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void Update() {
            if(shldReverseNormals) {
                MeshFilter meshFilter = GetComponent<MeshFilter>();
                if(meshFilter != null) {
                    Mesh mesh = meshFilter.sharedMesh;
                    Vector3[] normals = mesh.normals;
                    int normalsLen = normals.Length;

                    for(int i = 0; i < normalsLen; ++i) {
                        normals[i] = -normals[i];
                    }
                    mesh.normals = normals;

                    int subMeshCount = mesh.subMeshCount;
                    for(int m = 0; m < subMeshCount; ++m) {
                        int[] triangles = mesh.GetTriangles(m);

                        for(int i = 0; i < triangles.Length; i += 3) {
                            int temp = triangles[i + 0];
                            triangles[i + 0] = triangles[i + 1];
                            triangles[i + 1] = temp;
                        }

                        mesh.SetTriangles(triangles, m);
                    }
                }
            }
            shldReverseNormals = false;
        }

        #endregion
    }
}