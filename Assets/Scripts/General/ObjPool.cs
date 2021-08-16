using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MXR.General {
    internal sealed class ObjPool: MonoBehaviour {
		#region Fields

		private List<GameObject> activeObjs;
		private List<GameObject> inactiveObjs;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal ObjPool(): base() {
			activeObjs = null;
			inactiveObjs = null;
        }

        static ObjPool() {
        }

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		internal void InitMe(int size, [JetBrains.Annotations.NotNull] GameObject prefab, Transform parentTransform) {
			activeObjs = new List<GameObject>(size);
			inactiveObjs = new List<GameObject>(size);

			for(int i = 0; i < size; ++i) {
				GameObject GO = Instantiate(prefab, parentTransform);
				GO.SetActive(false);
				inactiveObjs.Add(GO);
			}
		}

		internal GameObject ActivateObj() {
			GameObject GO = inactiveObjs[0];
			GO.SetActive(true);
			inactiveObjs.RemoveAt(0);
			activeObjs.Add(GO);
			return GO;
		}

		internal void DeactivateObj(GameObject obj) {
			GameObject GO = activeObjs.Where(x => x == obj).SingleOrDefault();

			if(GO != null) {
				GO.SetActive(false);
				inactiveObjs.Add(GO);
				_ = activeObjs.Remove(GO);
			}
		}
    }
}