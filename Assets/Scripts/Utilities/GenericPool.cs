using System.Collections.Generic;
using UnityEngine;

namespace Game.Utilities
{
    /// <summary>
    /// Base for pool for object pooling
    /// </summary>
    /// <typeparam name="T">Type of created pool element</typeparam>
    /// <typeparam name="I">Type of parameters passed to pool element</typeparam>
    public abstract class GenericPool<T, I> : MonoBehaviour where T : GenericPoolElement<I> where I : IGenericPoolElementProperties
    {
        [SerializeField]
        private T _poolElementPrefab;
        [SerializeField]
        private int _poolSize;
        [SerializeField]
        private Transform _poolParent;

        protected Stack<T> _poolStack;
        protected List<T> _activated;

        private void Awake()
        {
            _poolStack = new Stack<T>();
            _activated = new List<T>();
            if (_poolParent == null)
            {
                _poolParent = transform;
            }
            for (int i = 0; i < _poolSize; i++)
            {
                T poolElement = Instantiate(_poolElementPrefab);
                poolElement.Initialize(_poolParent);
                _poolStack.Push(poolElement);
            }
        }

        /// <summary>
        /// Activates the pool element (if available) with given properties
        /// </summary>
        /// <param name="properties">Properties with which the element will be initialized</param>
        /// <returns>Activated element</returns>
        public virtual T Activate(I properties)
        {
            T element = _poolStack.Peek();
            if (element != null)
            {
                element.Activate(properties, () => Deactivate(element));
                _poolStack.Pop();
                _activated.Add(element);
                return element;
            }
            return null;
        }

        /// <summary>
        /// Deactivates given element
        /// </summary>
        /// <param name="element">Element to deactivate</param>
        protected virtual void Deactivate(T element)
        {
            if(element != null && _activated.Contains(element))
            {
                _activated.Remove(element);
                _poolStack.Push(element);
            }
        }
    }
}