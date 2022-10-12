using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// Controls when and where upgrades should be shown
    /// </summary>
    public class UpgradeSpawnManager : MonoBehaviour
    {
        [SerializeField]
        private GunUpgrade[] _upgrades;
        [SerializeField]
        private Gun _gunReference;
        [SerializeField]
        private Vector3[] _upgradePositions;
        [SerializeField]
        private Vector2 _randomWaitTime;

        private void Awake() => StartCoroutine(WaitAndSpawnUpgrade());

        private IEnumerator WaitAndSpawnUpgrade()
        {
            yield return new WaitForSeconds(Random.Range(_randomWaitTime.x, _randomWaitTime.y));
            GunUpgrade[] _upgradesSelection = _upgrades.Where(x => x.GunProperties != _gunReference.CurrentGunProperties).ToArray();
            GunUpgrade upgrade = _upgradesSelection[Random.Range(0, _upgradesSelection.Length)];
            upgrade.ShowUpgrade(GetRealUpgradePosition(Random.Range(0, _upgradePositions.Length)));
            upgrade.Collected += OnUpgradeCollected;
        }

        private void OnUpgradeCollected(GunUpgrade upgrade)
        {
            _gunReference.ApplyGunUpgrade(upgrade.GunProperties);
            upgrade.Collected -= OnUpgradeCollected;
            StartCoroutine(WaitAndSpawnUpgrade());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < (_upgradePositions?.Length ?? 0); i++)
            {
                Gizmos.DrawSphere(GetRealUpgradePosition(i), 0.25f);
            }
        }

        private Vector3 GetRealUpgradePosition(int i)
        {
            if(i < 0 || i >= (_upgradePositions?.Length ?? 0))
            {
                return transform.position;
            }
            return transform.position + transform.localToWorldMatrix.MultiplyVector(_upgradePositions[i]);
        }
    }
}