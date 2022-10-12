using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Single health bar in health system
    /// </summary>
    public class HealthPoint : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private RectTransform _rectTransform;
        [SerializeField]
        private float _animationSpeed;
        [SerializeField]
        private Gradient _gradient;
        [SerializeField]
        private AnimationCurve _scaleCurve;

        private Coroutine _fading;
        private float _fadeState;

        private void Awake()
        {
            _fadeState = 0f;
        }

        /// <summary>
        /// Begins fade in animation
        /// </summary>
        /// <param name="delay">Animation start delay</param>
        public void AnimateFadeIn(float delay)
        {
            if (_fading != null)
            {
                StopCoroutine(_fading);
            }
            _fading = StartCoroutine(FadeIn(delay));
        }

        /// <summary>
        /// Begins fade out animation
        /// </summary>
        /// <param name="delay">Animation start delay</param>
        public void AnimateFadeOut(float delay)
        {
            if(_fading != null)
            {
                StopCoroutine(_fading);
            }
            _fading = StartCoroutine(FadeOut(delay));
        }

        private void UpdateImage()
        {
            _image.color = _gradient.Evaluate(_fadeState);
            _rectTransform.localScale = Vector3.one * _scaleCurve.Evaluate(_fadeState);
        }

        private IEnumerator FadeIn(float delay)
        {
            yield return new WaitForSeconds(delay);
            for (; _fadeState < 1f; _fadeState += _animationSpeed * Time.deltaTime)
            {
                UpdateImage();
                yield return null;
            }
            _fadeState = 1f;
            UpdateImage();
            _fading = null;
        }

        private IEnumerator FadeOut(float delay)
        {
            yield return new WaitForSeconds(delay);
            for (; _fadeState > 0f; _fadeState -= _animationSpeed * Time.deltaTime)
            {
                UpdateImage();
                yield return null;
            }
            _fadeState = 0f;
            UpdateImage();
            _fading = null;
        }
    }
}