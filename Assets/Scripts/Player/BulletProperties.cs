using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    using Game.Utilities;
    using Game.Effects;

    /// <summary>
    /// Properties for pooled bullet
    /// </summary>
    public class BulletProperties : IGenericPoolElementProperties
    {
        public GunProperties GunProperties { get; set; }
        public Transform GunBarrel { get; set; }
    }
}