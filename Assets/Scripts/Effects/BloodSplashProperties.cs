using UnityEngine;

namespace Game.Effects
{
    using Game.Utilities;

    /// <summary>
    /// Properties for pooled blood splash effect
    /// </summary>
    public class BloodSplashProperties : IGenericPoolElementProperties
    {
        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
    }
}