using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// Handles health points bar on the screen
    /// </summary>
    public class HealthPointController : MonoBehaviour
    {

        [SerializeField]
        private HealthPoint _hpPrefab;

        private Stack<HealthPoint> _healthPoints;
        private Stack<HealthPoint> _reducedHealthPoints;

        private int _maxHPCount;

        private void Awake()
        {
            _healthPoints = new Stack<HealthPoint>();
            _reducedHealthPoints = new Stack<HealthPoint>();
        }

        /// <summary>
        /// Reinitializes the health bar based on maximum possible player HP
        /// </summary>
        /// <param name="maxHPCount">Maximum player HP to show on screen</param>
        public void InitializeHealth(int maxHPCount)
        {
            while(_healthPoints.Count > 0)
            {
                Destroy(_healthPoints.Pop().gameObject);
            }
            _maxHPCount = maxHPCount;
            _reducedHealthPoints = new Stack<HealthPoint>();
            for (int i = 0; i < _maxHPCount; i++)
            {
                HealthPoint element = Instantiate(_hpPrefab, transform);
                element.AnimateFadeIn((float)i * 0.1f);
                _healthPoints.Push(element);
            }
        }

        /// <summary>
        /// Animates health reduction if possible
        /// </summary>
        /// <param name="hpLost">Number of bars to fade out</param>
        public void ReduceHealth(int hpLost)
        {
            for (int i = 0; i < hpLost; i++)
            {
                if(_healthPoints.Count <= 0)
                {
                    break;
                }
                HealthPoint element = _healthPoints.Pop();
                element.AnimateFadeOut((float)i * 0.1f);
                _reducedHealthPoints.Push(element);
            }
        }

        /// <summary>
        /// Animates health restoration if possible
        /// </summary>
        /// <param name="hpRestored">Number of bars to fade in</param>
        public void RestoreHealth(int hpRestored)
        {
            for (int i = 0; i < hpRestored; i++)
            {
                if (_reducedHealthPoints.Count <= 0)
                {
                    break;
                }
                HealthPoint element = _reducedHealthPoints.Pop();
                element.AnimateFadeIn(0f);
                _healthPoints.Push(element);
            }
        }
    }
}