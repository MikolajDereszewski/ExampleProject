using UnityEngine;

namespace Game.Effects
{
    /// <summary>
    /// Animates the scenery drawing shader
    /// </summary>
    public class SceneryShaderAnimation : MonoBehaviour
    {
        [SerializeField]
        private float _animationSpeed;
        [SerializeField]
        private float _maxShaderValue;

        private float _animationState;

        private void Awake()
        {
            _animationState = 0f;
            Shader.SetGlobalFloat("_FadeRange", 0f);
        }

        private void OnDestroy()
        {
            Shader.SetGlobalFloat("_FadeRange", 1000f);
        }

        void Update()
        {
            if (_animationState < _maxShaderValue)
            {
                Shader.SetGlobalFloat("_FadeRange", _animationState);
                _animationState += _animationSpeed * Time.deltaTime;
            }
        }
    }
}