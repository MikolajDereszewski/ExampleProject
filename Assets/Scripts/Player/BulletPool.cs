using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    using Game.Utilities;

    /// <summary>
    /// Pool for bullets
    /// </summary>
    public class BulletPool : GenericPool<Bullet, BulletProperties> { }
}