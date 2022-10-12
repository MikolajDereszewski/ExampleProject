using System;
using UnityEngine;

namespace Game.Utilities
{
    /// <summary>
    /// Base for pool elements
    /// </summary>
    /// <typeparam name="T">Type of parameters to activate element with</typeparam>
    public abstract class GenericPoolElement<T> : MonoBehaviour where T : IGenericPoolElementProperties
    {
        private Action _onDeactivated;

        /// <summary>
        /// Called when pool initializes all elements on Awake
        /// </summary>
        /// <param name="parent"></param>
        public virtual void Initialize(Transform parent)
        {
            gameObject.SetActive(false);
            transform.parent = parent;
        }

        /// <summary>
        /// Activates the pool element with given properties
        /// </summary>
        /// <param name="properties">Properties with which the element will be initialized</param>
        /// <param name="onDeactivated">Callback passed from GenericPool</param>
        public virtual void Activate(T properties, Action onDeactivated) => _onDeactivated = onDeactivated;

        /// <summary>
        /// Deactivates element and returns it to pool
        /// </summary>
        public virtual void Deactivate() => _onDeactivated?.Invoke();
    }
}