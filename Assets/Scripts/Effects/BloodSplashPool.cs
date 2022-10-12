using UnityEngine;

namespace Game.Effects
{
    using Game.Utilities;

    /// <summary>
    /// Pool for blood splash effect
    /// </summary>
    public class BloodSplashPool : GenericPool<BloodSplash, BloodSplashProperties>
    {
        /// <summary>
        /// Constructs pool element properties and activates single element
        /// </summary>
        /// <param name="position">Position to place effect in</param>
        /// <param name="normal">Vector3 to align effect with</param>
        public void Activate(Vector3 position, Vector3 normal) => Activate(new BloodSplashProperties()
        {
            Position = position,
            Normal = normal
        });
    }
}