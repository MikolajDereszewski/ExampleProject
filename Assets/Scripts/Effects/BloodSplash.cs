using System.Collections;
using UnityEngine;

namespace Game.Effects
{
    using Game.Utilities;
    using System;

    /// <summary>
    /// Blood splash effect element in pool
    /// </summary>
    public class BloodSplash : GenericPoolElement<BloodSplashProperties>
    {
        [SerializeField]
        private ParticleSystem _particleSystem;
        [SerializeField]
        private ParticleSystem _subParticleSystem;

        public override void Activate(BloodSplashProperties properties, Action onDeactivated)
        {
            base.Activate(properties, onDeactivated);
            transform.position = properties.Position;
            transform.rotation = Quaternion.LookRotation(properties.Normal, Vector3.up);
            gameObject.SetActive(true);
            _particleSystem.Play();
            StartCoroutine(WaitForParticlesDeath());
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
            _particleSystem.Stop();
            base.Deactivate();
        }

        private IEnumerator WaitForParticlesDeath()
        {
            yield return null;
            yield return new WaitUntil(() => _particleSystem.particleCount == 0 && _subParticleSystem.particleCount == 0);
            Deactivate();
        }
    }
}