using System.Collections;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Handles shooting bullets from the pool
    /// </summary>
    public class Gun : MonoBehaviour
    {
        [field: SerializeField]
        public GunProperties CurrentGunProperties { get; private set; }
        [SerializeField]
        private BulletPool _bulletPool;
        [SerializeField]
        private Transform _gunBarrel;

        private BulletProperties _currentBulletProperties;
        private Coroutine _shootingInterval;

        private void Awake()
        {
            _currentBulletProperties = new BulletProperties()
            {
                GunProperties = CurrentGunProperties,
                GunBarrel = _gunBarrel,
            };
        }

        /// <summary>
        /// Activates bullet with properties of current gun, if shooting interval allows it
        /// </summary>
        public void TryShoot()
        {
            if(_shootingInterval != null)
            {
                return;
            }
            _bulletPool.Activate(_currentBulletProperties);
            _shootingInterval = StartCoroutine(ShootingInterval(CurrentGunProperties));
        }

        /// <summary>
        /// Applies gun properties from given upgrade
        /// </summary>
        /// <param name="gunProperties">Gun properties to apply</param>
        public void ApplyGunUpgrade(GunProperties gunProperties)
        {
            CurrentGunProperties = gunProperties;
            _currentBulletProperties = new BulletProperties()
            {
                GunProperties = CurrentGunProperties,
                GunBarrel = _gunBarrel,
            };
        }

        private IEnumerator ShootingInterval(GunProperties properties)
        {
            yield return new WaitForSeconds(properties.ShotInterval);
            _shootingInterval = null;
        }
    }
}