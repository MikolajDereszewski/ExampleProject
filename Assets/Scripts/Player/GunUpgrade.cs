using System;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Holds data for player's gun upgrade
    /// </summary>
    public class GunUpgrade : MonoBehaviour
    {
        [field: SerializeField]
        public GunProperties GunProperties { get; private set; }

        /// <summary>
        /// Shows the upgrade in given position
        /// </summary>
        /// <param name="position">Position to show upgrade in</param>
        public void ShowUpgrade(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the upgrade and invokes callback
        /// </summary>
        public void CollectUpgrade()
        {
            gameObject.SetActive(false);
            OnCollected();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                Bullet bullet = other.GetComponent<Bullet>();
                bullet?.Deactivate();
                CollectUpgrade();
            }
        }

        public event Action<GunUpgrade> Collected;
        private void OnCollected() => Collected?.Invoke(this);
    }
}