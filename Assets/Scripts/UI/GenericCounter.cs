using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Counter displaying various values in UI
    /// </summary>
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(RectTransform))]
    public class GenericCounter : MonoBehaviour
    {
        [SerializeField]
        private bool _useScaleAnimation;
        [SerializeField]
        private Vector2 _scaleAnimationRange;
        [SerializeField]
        private float _scaleAnimationStep;
        [SerializeField]
        private float _scaleAnimationRevertSpeed;
        [SerializeField]
        private string _textPrefix;

        private RectTransform _textRectTransform;
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _textRectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(_useScaleAnimation && _textRectTransform.localScale.x > _scaleAnimationRange.x)
            {
                _textRectTransform.localScale = Vector3.Lerp(_textRectTransform.localScale, Vector3.one, _scaleAnimationRevertSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Updates the visuals with integer
        /// </summary>
        /// <param name="counterValue">Integer counter value to show</param>
        public void UpdateText(int counterValue)
        {
            _text.text = string.Concat(_textPrefix, ": ", counterValue);
            AnimateStep();
        }

        /// <summary>
        /// Updates the visuals with float
        /// </summary>
        /// <param name="counterValue">Float counter value to show</param>
        public void UpdateText(float counterValue)
        {
            _text.text = string.Concat(_textPrefix, ": ", counterValue);
            AnimateStep();
        }

        private void AnimateStep()
        {
            if (_useScaleAnimation)
            {
                _textRectTransform.localScale = Vector2.one * Mathf.Clamp(_textRectTransform.localScale.x + _scaleAnimationStep, _scaleAnimationRange.x, _scaleAnimationRange.y);
            }
        }
    }
}