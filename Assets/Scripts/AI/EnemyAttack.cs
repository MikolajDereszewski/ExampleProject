using UnityEngine;

namespace Game.AI
{
    /// <summary>
    /// Base for all attack types
    /// </summary>
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField]
        protected int _damage;

        protected Enemy _enemy;

        /// <summary>
        /// Initializes the attacks
        /// </summary>
        /// <param name="enemy">Referenced enemy</param>
        public virtual void InitializeAttack(Enemy enemy)
        {
            _enemy = enemy;
        }
    }
}