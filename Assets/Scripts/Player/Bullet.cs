using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    using Game.Utilities;

    /// <summary>
    /// Bullet in pool
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : GenericPoolElement<BulletProperties>
    {
        public float Damage { get; private set; }
        
        private Rigidbody _rigidbody;

        public override void Initialize(Transform parent)
        {
            _rigidbody = GetComponent<Rigidbody>();
            base.Initialize(parent);
        }

        public override void Activate(BulletProperties properties, Action onDeactivated)
        {
            Damage = properties.GunProperties.Damage;
            transform.localScale = properties.GunProperties.BulletScale;
            transform.position = properties.GunBarrel.position;
            transform.rotation = Quaternion.LookRotation(properties.GunBarrel.forward, Vector3.up);
            _rigidbody.useGravity = properties.GunProperties.UseGravity;
            gameObject.SetActive(true);
            _rigidbody.AddForce(properties.GunBarrel.forward * properties.GunProperties.ForceApplied, ForceMode.Impulse);
            StartCoroutine(AutoDestroyAfterTime(properties.GunProperties.AutoDestroyTime));
            base.Activate(properties, onDeactivated);
        }

        public override void Deactivate()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            base.Deactivate();
        }

        private IEnumerator AutoDestroyAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            Deactivate();
        }
    }
}