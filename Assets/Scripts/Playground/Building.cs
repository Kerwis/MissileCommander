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
				_isDestroyed = true;
				_good.SetActive(false);
				_destroyed.SetActive(true);
			}
		}
	}
}