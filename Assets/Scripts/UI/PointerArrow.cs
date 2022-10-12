using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    using Game.Utilities;
    using Game.AI;

    /// <summary>
    /// Pointer arrow element in pool
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    public class PointerArrow : GenericPoolElement<PointerArrowProperties>
    {
        public Enemy ReferencedEnemy { get; private set; }

        [SerializeField]
        private float _dotThreshold;
        [SerializeField]
        private Gradient _dangerGradient;
        [SerializeField]
        private Vector2 _distanceGradientMap;
        [SerializeField]
        private float _distanceFromCenter;

        private Image _image;
        private RectTransform _rectTransform;
        private Transform _camera;

        private void Update()
        {
            if (ReferencedEnemy.transform != null && _camera != null)
            {
                Vector3 enemyToCamera = (ReferencedEnemy.transform.position - _camera.position);
                float dotProduct = Vector3.Dot(_camera.forward, enemyToCamera.normalized);
                if(dotProduct < _dotThreshold)
                {
                    _image.enabled = true;
                    float distanceGradient = Mathf.Lerp(0f, 1f, (enemyToCamera.magnitude - _distanceGradientMap.x) / _distanceGradientMap.y);
                    _image.color = _dangerGradient.Evaluate(distanceGradient);
                    Vector3 projectedCamera = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
                    Vector3 projectedEnemy = Vector3.ProjectOnPlane(enemyToCamera, Vector3.up).normalized;
                    float angle = Vector3.Angle(projectedCamera, projectedEnemy);
                    float crossProductY = Vector3.Cross(projectedCamera, projectedEnemy).y;
                    _rectTransform.localRotation = Quaternion.Euler(0f, 0f, crossProductY < 0 ? angle : -angle);
                    angle *= Mathf.Deg2Rad;
                    _rectTransform.localPosition = (Vector3.up * Mathf.Cos(angle) + Vector3.right * Mathf.Sin(crossProductY < 0 ? -angle : angle)) * _distanceFromCenter;
                }
                else
                {
                    _image.enabled = false;
                }
            }
        }

        public override void Initialize(Transform parent)
        {
            base.Initialize(parent);
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void Activate(PointerArrowProperties properties, Action onDeactivated)
        {
            base.Activate(properties, onDeactivated);
            _image.enabled = false;
            _rectTransform.localPosition = Vector3.zero;
            _rectTransform.localScale = Vector3.one;
            ReferencedEnemy = properties.ReferencedEnemy;
            _camera = properties.Camera;
            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            _image.enabled = false;
            ReferencedEnemy = null;
            _camera = null;
            gameObject.SetActive(false);
            base.Deactivate();
        }
    }
}