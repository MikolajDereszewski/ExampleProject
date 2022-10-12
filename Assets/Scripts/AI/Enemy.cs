using UnityEngine;

namespace Game.AI
{
    using Game.Utilities;
    using Game.Effects;
    using System;

    /// <summary>
    /// Enemy element in pool
    /// </summary>
    public class Enemy : GenericPoolElement<EnemyProperties>
    {
        public float CurrentHealth { get; private set; }

        [SerializeField]
        private EnemyMovement _enemyMovementController;
        [SerializeField]
        private EnemyAttack _enemyAttackController;
        [SerializeField]
        private float _baseHealth;

        private BloodSplashPool _bloodSplashPool;

        public override void Activate(EnemyProperties properties, Action onDeactivated)
        {
            base.Activate(properties, onDeactivated);
            transform.position = properties.SpawnPosition;
            gameObject.SetActive(true);
            CurrentHealth = _baseHealth;
            _bloodSplashPool = properties.BloodSplashPool;
            _enemyMovementController.InitializeMovement(this);
            _enemyAttackController.InitializeAttack(this);
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
            base.Deactivate();
        }

        /// <summary>
        /// Take damage at given point with normal for particles
        /// </summary>
        /// <param name="damagePoints">Damage dealt</param>
        /// <param name="point">Hit position</param>
        /// <param name="normal">Hit normal</param>
        public void Damage(float damagePoints, Vector3 point, Vector3 normal)
        {
            _bloodSplashPool.Activate(point, -normal);
            CurrentHealth -= damagePoints;
            if(CurrentHealth <= 0f)
            {
                Deactivate();
            }
        }
    }
}