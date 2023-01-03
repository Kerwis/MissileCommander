using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Playground
{
	public class Enemy : MonoBehaviour
	{
		[SerializeField]
		private Missile _missilePrefab;

		[SerializeField]
		private Vector2 BattlegroundSize;
		
		[SerializeField]
		private Vector2 BattlegroundOffset;

		private IObjectPool<Missile> _pool;

		private float _timeLeft;

		private bool _firing = true;

		private void Start()
		{
			_pool = new ObjectPool<Missile>(CreateMissile);
		}

		private void Update()
		{
			if(!_firing)
				return;
			
			_timeLeft -= Time.deltaTime;
			if (_timeLeft <= 0)
			{
				Shot();
				_timeLeft = Random.Range(0.5f, 2f);
			}
		}

		private Missile CreateMissile()
		{
			var missile = Instantiate(_missilePrefab, transform);
			missile.SetPool(_pool);
			return missile;
		}

		private void Shot()
		{
			var missile = _pool.Get();
			missile.gameObject.SetActive(true);
			missile.transform.position = new Vector3(Random.Range(-BattlegroundSize.x, BattlegroundSize.x), BattlegroundSize.y + BattlegroundOffset.y);
			missile.SetTarget(
				new Vector2(Random.Range(-BattlegroundSize.x, BattlegroundSize.x),
					(-BattlegroundSize.y + BattlegroundOffset.y) * 2),
				Random.Range(1, 3));
		}

		public void EndFiring()
		{
			_firing = false;
		}
	}
}