using UnityEngine;

namespace Game.Logic
{
    /// <summary>
    /// Scriptable Object containing data for single wave in game
    /// </summary>
    [CreateAssetMenu(fileName = "Wave Settings", menuName = "Game Scriptables/Wave Settings", order = 2)]
    public class WaveSettings : ScriptableObject
    {
        [field: SerializeField]
        public WaveEnemySettings[] WaveEnemies { get; private set; }
    }

    /// <summary>
    /// Single enemy type settings
    /// </summary>
    [System.Serializable]
    public struct WaveEnemySettings
    {
        [field: SerializeField]
        public EnemyType EnemyType { get; private set; }
        [field: SerializeField]
        public float[] TimelineSpawns { get; private set; }
    }

    public enum EnemyType
    {
        BasicWalker,
        FastWalker,
        HeavyWalker
    }
}