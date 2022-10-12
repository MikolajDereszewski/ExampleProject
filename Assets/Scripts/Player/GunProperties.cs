using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Contains gun and bullet properties
    /// </summary>
    [CreateAssetMenu(fileName = "Gun Properties", menuName = "Game Scriptables/Gun Properties", order = 0)]
    public class GunProperties : ScriptableObject
    {
        [field: SerializeField]
        public float ForceApplied { get; private set; }
        [field: SerializeField]
        public bool UseGravity { get; private set; }
        [field: SerializeField]
        public float Damage { get; private set; }
        [field: SerializeField]
        public float ShotInterval { get; private set; }
        [field: SerializeField]
        public float AutoDestroyTime { get; private set; }
        [field: SerializeField]
        public Vector3 BulletScale { get; private set; }
    }
}