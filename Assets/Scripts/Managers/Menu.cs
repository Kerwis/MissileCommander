using UnityEngine;

namespace Managers
{
	public class Menu : MonoBehaviour
	{
		public void Play()
		{
			Scenes.LoadScene(Scenes.ScenesNames.Game);
		}
	}
}
