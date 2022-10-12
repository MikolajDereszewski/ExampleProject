using UnityEngine;
using UnityEngine.Events;

namespace Game.Input
{
    /// <summary>
    /// Handles interaction and invokes UnityEvent on click
    /// </summary>
    public class Controls : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _onClicked;

        private void Update()
        {
#if UNITY_EDITOR
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _onClicked?.Invoke();
            }
#else
            if(UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _onClicked?.Invoke();
            }
#endif
        }
    }
}