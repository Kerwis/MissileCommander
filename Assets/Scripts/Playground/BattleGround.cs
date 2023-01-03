using System;
using System.Collections.Generic;
using Data;
using Managers;
using Playground.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Playground
{
	public class BattleGround : MonoBehaviour
	{
		[SerializeField]
		private Tower _towerLeft;
		[SerializeField]
		private Tower _towerMiddle;
		[SerializeField]
		private Tower _towerRight;
		
		//Maybe move it to data
		[SerializeField]
		private string _aimPositionActionName = "AimPosition";
		[SerializeField]
		private string _fireActionPrefixName = "Fire";
		[SerializeField]
		private string _towerLeftName = "Left";
		[SerializeField]
		private string _towerMiddleName = "Middle";
		[SerializeField]
		private string _towerRightName = "Right";

		[SerializeField]
		private List<City> _cities;

		[SerializeField]
		private Enemy _enemy;

		[SerializeField]
		private GameUI _gameUI;

		private void Awake()
		{
			RegisterInGame();
		}

		private void RegisterInGame()
		{
			Game.RegisterBattleGround(this);
		}

		public void Setup(GameData data)
		{
			SetupTower(_towerLeft, _towerLeftName, data, true);
			SetupTower(_towerMiddle, _towerMiddleName, data);
			SetupTower(_towerRight, _towerRightName, data, true);
			
			//TODO
			_towerLeft.OnDemolishTower += () => _gameUI.LeftTowerStatus.text = "OFF";
			_towerMiddle.OnDemolishTower += () => _gameUI.MiddleTowerStatus.text = "OFF";
			_towerRight.OnDemolishTower += () => _gameUI.RightTowerStatus.text = "OFF";
			
			foreach (var city in _cities)
			{
				city.Setup();
				city.OnDemolishTower += CheckCities;
			}
		}

		private void SetupTower(Tower tower, string towerName, GameData data, bool isSide = false)
		{
			tower.Setup(data.ActionMap.FindAction(_aimPositionActionName),
				data.ActionMap.FindAction(_fireActionPrefixName + towerName),
				isSide ? data.SideTowerData : data.MiddleTowerData);
		}
		
		private void CheckCities()
		{
			foreach (var city in _cities)
			{
				if(!city.IsDestroyed)
					return;
			}

			EndGame();
		}

		private void EndGame()
		{
			_enemy.EndFiring();
			_towerLeft.EndGame();
			
			_gameUI.GameOver();
			Game.GameOver();
		}
	}
}