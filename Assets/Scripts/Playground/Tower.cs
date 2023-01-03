using System;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Playground
{
	public class Tower : Building
	{
		[SerializeField]
		private Ammo _ammo;

		private TowerData _data;
		private InputAction _aimPosition;
		private InputAction _shotAction;

		public void Setup(InputAction aim, InputAction shotAction, TowerData towerData)
		{
			if(towerData == null)
				return;
			_ammo.Setup(towerData);
			_data = towerData;
			_aimPosition = aim;
			_shotAction = shotAction;

			_shotAction.performed += OnShotActionPerformed;
		}

		private void OnShotActionPerformed(InputAction.CallbackContext context)
		{
			Shot(Camera.main.ScreenToWorldPoint(_aimPosition.ReadValue<Vector2>()));
		}

		private void Shot(Vector2 position)
		{
			if (_ammo.CanShot)
			{
				SpawnMissile(position);
				_ammo.Shot();
			}
		}

		private void SpawnMissile(Vector2 position)
		{
			var missile = Instantiate(_data.MissilePrefab, transform);
			missile.SetTarget(position - (Vector2)transform.position, _data.ShotForce);
		}

		private void OnDestroy()
		{
			if (_shotAction != null)
				_shotAction.performed -= OnShotActionPerformed;
		}
	}
}
