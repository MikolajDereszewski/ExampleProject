using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.Logic
{
    using Game.AI;

    /// <summary>
    /// Controls waves of enemies in game
    /// </summary>
    public class WaveController : MonoBehaviour
    {
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private GameState _gameState;
        [SerializeField]
        private EnemyPool[] _enemyPools;

        private int _currentEnemyCount;

        private void Awake()
        {
            _gameState.EnemyDeactivated += OnEnemyDeactivated;
            _gameState.WaveFinished += StartWave;
        }

        private void Start()
        {
            _gameState.OnGameReset();
            StartWave();
        }

        private void OnDestroy()
        {
            _gameState.EnemyDeactivated -= OnEnemyDeactivated;
            _gameState.WaveFinished -= StartWave;
        }

        private void StartWave()
        {
            WaveSettings settings = _gameSettings.GetWaveOfIndex(_gameState.CurrentWave);
            _currentEnemyCount = 0;
            settings.WaveEnemies
                .ToList()
                .ForEach(x => x.TimelineSpawns
                .ToList()
                .ForEach(y => StartCoroutine(EnemySpawning(_enemyPools.FirstOrDefault(z => z.EnemyTypeInPool == x.EnemyType), y))));
        }

        private void OnEnemyDeactivated(Enemy enemy)
        {
            _currentEnemyCount--;
            if (_currentEnemyCount <= 0)
            {
                _gameState.OnWaveFinished();
            }
        }

        private IEnumerator EnemySpawning(EnemyPool pool, float waitTime)
        {
            _currentEnemyCount++;
            yield return new WaitForSeconds(waitTime);
            pool.ActivateInRandomPosition();
        }
    }
}