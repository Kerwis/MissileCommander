using System;
using Playground;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(menuName = "Create TowerData", fileName = "TowerData", order = 1)]
	public class TowerData : ScriptableObject
	{
		public Missile MissilePrefab;
		public GameObject AmmoUIPrefab;

		public int AmmoCount;

		public int[] MissileRows = { 1, 2, 3, 4 };
		public float ShotForce = 1;
	}
}