using UnityEngine;
using UnityEngine.SceneManagement;

namespace CatlikeCoding.NumberFlow.Examples {

	public class MultisceneSwitcher : MonoBehaviour {

		public string nextSceneName;

		void Update () {
			if (Time.timeSinceLevelLoad > 2f) {
				SceneManager.LoadScene(nextSceneName);
			}
		}
	}
}