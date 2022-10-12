using UnityEngine;

namespace Game.AI
{
    using Game.Utilities;
    using Game.Logic;
    using Game.Effects;

    /// <summary>
    /// Pool for enemies of given type
    /// </summary>
    public class EnemyPool : GenericPool<Enemy, EnemyProperties>
    {
        [field: SerializeField]
        public EnemyType EnemyTypeInPool { get; private set; }
        [SerializeField]
        private GameState _gameState;
        [SerializeField]
        private BloodSplashPool _bloodSplashPool;
        [SerializeField]
        private Vector2 _spawnRadius;
        [SerializeField]
        private Color _gizmoColor;

        /// <summary>
        /// Randomizes spawn position and activates enemy in it
        /// </summary>
        /// <returns>Activated enemy</returns>
        public Enemy ActivateInRandomPosition() => Activate(new EnemyProperties()
        {
            SpawnPosition = GetRandomBoundsPosition(),
            BloodSplashPool = _bloodSplashPool,
        });

        public override Enemy Activate(EnemyProperties properties)
        {
            Enemy enemy = base.Activate(properties);
            _gameState.OnEnemyActivated(enemy);
            return enemy;
        }

        protected override void Deactivate(Enemy element)
        {
            if(element.CurrentHealth <= 0f)
            {
                _gameState.OnScored();
            }
            _gameState.OnEnemyDeactivated(element);
            base.Deactivate(element);
        }

        private Vector3 GetRandomBoundsPosition() => transform.position + Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * transform.forward * Random.Range(_spawnRadius.x, _spawnRadius.y);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * _spawnRadius.x);
            Gizmos.color = _gizmoColor;
            Gizmos.DrawLine(transform.position + transform.forward * _spawnRadius.x, transform.position + transform.forward * _spawnRadius.y);
        }
    }
}