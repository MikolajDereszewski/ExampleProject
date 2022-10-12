using UnityEngine;

namespace Game.AI
{
    /// <summary>
    /// Base for all movement types
    /// </summary>
    public abstract class EnemyMovement : MonoBehaviour
    {
        protected Enemy _enemy;

        /// <summary>
        /// Initializes the movement
        /// </summary>
        /// <param name="enemy">Referenced enemy</param>
        public virtual void InitializeMovement(Enemy enemy)
        {
            _enemy = enemy;
        }
    }
}