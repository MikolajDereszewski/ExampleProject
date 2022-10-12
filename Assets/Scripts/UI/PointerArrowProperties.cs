using UnityEngine;

namespace Game.UI
{
    using Game.Utilities;
    using Game.AI;

    /// <summary>
    /// Properties for pooled arrow pointer in UI
    /// </summary>
    public class PointerArrowProperties : IGenericPoolElementProperties
    {
        public Enemy ReferencedEnemy { get; set; }
        public Transform Camera { get; set; }
    }
}