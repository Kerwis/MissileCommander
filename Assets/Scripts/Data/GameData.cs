using UnityEngine;
using UnityEngine.InputSystem;

namespace Data
{
	[CreateAssetMenu(menuName = "Create GameData", fileName = "GameData", order = 0)]
	public class GameData : ScriptableObject
	{
		public InputActionAsset ActionMap;
		
		public TowerData SideTowerData;
		
		public TowerData MiddleTowerData;
	}
}