using System;
using UnityEngine;

namespace Game.Logic
{
    using Game.AI;

    /// <summary>
    /// Scriptable Object containing current game state and handling events
    /// </summary>
    [CreateAssetMenu(fileName = "Game State", menuName = "Game Scriptables/Game State", order = 3)]
    public class GameState : ScriptableObject
    {
        public int CurrentScore { get; private set; }
        public int CurrentWave { get; private set; }
        public int CurrentPlayerHealth { get; private set; }

        [SerializeField]
        private GameSettings _gameSettings;

        public event Action GameReset;
        public void OnGameReset()
        {
            CurrentScore = 0;
            CurrentWave = 0;
            CurrentPlayerHealth = _gameSettings.DefaultPlayerHealth;
            GameReset?.Invoke();
        }

        public event Action<Enemy> EnemyActivated;
        public void OnEnemyActivated(Enemy enemy)
        {
            EnemyActivated?.Invoke(enemy);
        }

        public event Action<Enemy> EnemyDeactivated;
        public void OnEnemyDeactivated(Enemy enemy)
        {
            EnemyDeactivated?.Invoke(enemy);
        }

        public event Action Scored;
        public void OnScored()
        {
            CurrentScore++;
            Scored?.Invoke();
        }

        public event Action WaveFinished;
        public void OnWaveFinished()
        {
            CurrentWave++;
            WaveFinished?.Invoke();
        }

        public event Action<int> DamagedPlayer;
        public void OnDamagedPlayer(int damage)
        {
            CurrentPlayerHealth -= damage;
            if(CurrentPlayerHealth < 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            DamagedPlayer?.Invoke(damage);
        }

        public event Action<int> HealedPlayer;
        public void OnHealedPlayer(int healPoints)
        {
            HealedPlayer?.Invoke(healPoints);
        }
    }
}