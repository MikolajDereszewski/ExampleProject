using UnityEngine;

namespace Game.AI
{
    using Game.Utilities;
    using Game.Effects;

    /// <summary>
    /// Properties for pooled enemies
    /// </summary>
    public class EnemyProperties : IGenericPoolElementProperties
    {
        public Vector3 SpawnPosition { get; set; }
        public BloodSplashPool BloodSplashPool { get; set; }
    }
}