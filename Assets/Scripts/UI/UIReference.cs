using UnityEngine;

namespace Game.UI
{
    using Game.AI;
    using Game.Logic;

    /// <summary>
    /// Handles UI based on game state events
    /// </summary>
    public class UIReference : MonoBehaviour
    {
        [SerializeField]
        private GameState _gameState;
        [SerializeField]
        private Transform _cameraReference;
        [SerializeField]
        private PointerArrowPool _pointerArrowPool;
        [SerializeField]
        private GenericCounter _scoreTextHandler, _waveTextHandles;
        [SerializeField]
        private HealthPointController _healthPointController;

        private void Awake()
        {
            _gameState.GameReset += OnGameReset;
            _gameState.EnemyActivated += OnEnemyActivated;
            _gameState.EnemyDeactivated += OnEnemyDeactivated;
            _gameState.Scored += OnScored;
            _gameState.WaveFinished += OnWaveFinished;
            _gameState.DamagedPlayer += OnDamagedPlayer;
            _gameState.HealedPlayer += OnHealedPlayer;
        }

        private void OnDestroy()
        {
            _gameState.GameReset -= OnGameReset;
            _gameState.EnemyActivated -= OnEnemyActivated;
            _gameState.EnemyDeactivated -= OnEnemyDeactivated;
            _gameState.Scored -= OnScored;
            _gameState.WaveFinished -= OnWaveFinished;
            _gameState.DamagedPlayer -= OnDamagedPlayer;
            _gameState.HealedPlayer -= OnHealedPlayer;
            _gameState.HealedPlayer -= OnHealedPlayer;
        }

        private void OnGameReset()
        {
            _healthPointController.InitializeHealth(_gameState.CurrentPlayerHealth);
        }

        private void OnEnemyActivated(Enemy enemy)
        {
            PointerArrowProperties properties = new PointerArrowProperties()
            {
                ReferencedEnemy = enemy,
                Camera = _cameraReference,
            };
            _pointerArrowPool.Activate(properties);
        }

        private void OnEnemyDeactivated(Enemy enemy)
        {
            _pointerArrowPool.DeactivatePointerReferencingEnemy(enemy);
        }

        private void OnScored()
        {
            _scoreTextHandler.UpdateText(_gameState.CurrentScore);
        }

        private void OnWaveFinished()
        {
            _waveTextHandles.UpdateText(_gameState.CurrentWave + 1);
        }

        private void OnDamagedPlayer(int damage)
        {
            _healthPointController.ReduceHealth(damage);
        }

        private void OnHealedPlayer(int health)
        {
            _healthPointController.RestoreHealth(health);
        }
    }
}