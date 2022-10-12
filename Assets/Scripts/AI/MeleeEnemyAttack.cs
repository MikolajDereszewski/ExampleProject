using UnityEngine;

namespace Game.AI
{
    using Game.Logic;

    /// <summary>
    /// Enemy attack for simple melee
    /// </summary>
    public class MeleeEnemyAttack : EnemyAttack
    {
        [SerializeField]
        private GameState _gameState;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _enemy.Deactivate();
                _gameState.OnDamagedPlayer(_damage);
            }
        }
    }
}