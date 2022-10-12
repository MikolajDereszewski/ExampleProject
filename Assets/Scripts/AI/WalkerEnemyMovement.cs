using UnityEngine;
using UnityEngine.AI;

namespace Game.AI
{
    /// <summary>
    /// Enemy movement for simple walking on NavMesh
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class WalkerEnemyMovement : EnemyMovement
    {
        private NavMeshAgent _agent;

        /// <summary>
        /// Initializes NavMesh walking towards center of map
        /// </summary>
        /// <param name="enemy"></param>
        public override void InitializeMovement(Enemy enemy)
        {
            base.InitializeMovement(enemy);
            if (_agent == null)
            {
                _agent = GetComponent<NavMeshAgent>();
            }
            _agent.SetDestination(Vector3.zero);
        }
    }
}