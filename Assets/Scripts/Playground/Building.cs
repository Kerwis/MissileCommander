using System;
using UnityEngine;

namespace Playground
{
	public class Building : MonoBehaviour
	{
		[SerializeField]
		private GameObject _good;
		[SerializeField]
		private GameObject _destroyed;
		protected bool _isDestroyed;

		public bool IsDestroyed => _isDestroyed;

		public delegate void OnDemolish();

		public OnDemolish OnDemolishTower;

		public void Repair()
		{
			_isDestroyed = false;
			_good.SetActive(true);
			_destroyed.SetActive(false);
		}
		
		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!_isDestroyed)
			{
				Demolish();
			}
		}

		private void Demolish()
		{
			_isDestroyed = true;
			_good.SetActive(false);
			_destroyed.SetActive(true);
			
			OnDemolishTower?.Invoke();
		}
	}
}