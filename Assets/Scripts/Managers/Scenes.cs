using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class Scenes
	{
		private static readonly string InitSceneName = "Init";
		private static string _preInitSceneName = "";
		public enum ScenesNames //Need to reflect what we set in Builds Settings
		{
			Menu = 0,
			Game = 1,
		}
		public static void LoadScene(ScenesNames sceneName)
		{
			SceneManager.LoadScene((int)sceneName);
		}

		public static void LoadInit()
		{
			_preInitSceneName = SceneManager.GetActiveScene().name;
			if (_preInitSceneName != InitSceneName)
				SceneManager.LoadScene(InitSceneName);

		}

		public static void LoadSceneAfterInit()
		{
			if(_preInitSceneName == "")
				LoadScene(ScenesNames.Menu);
			else
				SceneManager.LoadScene(_preInitSceneName);
		}
	}
}