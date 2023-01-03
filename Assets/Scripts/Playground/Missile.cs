using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Playground
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Animator))]
	public class Missile : MonoBehaviour
	{
		private Rigidbody2D _rigidbody2D;
		private Animator _animator;
		private float _sqrTime;
		private IObjectPool<Missile> _pool;
		private static readonly int AnimatorExplodeHash = Animator.StringToHash("Explode");

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
		}

		public void SetTarget(Vector2 targetPosition, float shotForce)
		{
			_rigidbody2D.velocity = targetPosition.normalized * shotForce;
			_sqrTime = targetPosition.magnitude / _rigidbody2D.velocity.magnitude;
		}

		private void FixedUpdate()
		{
			_sqrTime -= Time.fixedDeltaTime;
			if (_sqrTime < 0)
				Explode();
		}

		private void Explode()
		{
			_rigidbody2D.velocity = Vector2.zero;
			_animator.SetTrigger(AnimatorExplodeHash);
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			Explode();
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			//Explode();
		}

		public void Destroy()
		{
			if (_pool != null)
			{
				_pool.Release(this);
				gameObject.SetActive(false);
			}
			else
			{
				Destroy(gameObject);	
			}
		}

		public void SetPool(IObjectPool<Missile> pool)
		{
			_pool = pool;
		}
	}
}