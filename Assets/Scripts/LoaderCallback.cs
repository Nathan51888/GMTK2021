using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoaderCallback : MonoBehaviour
    {
        private bool _isFirstUpdate = true;
        private void Update()
        {
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
                GameManager.Instance.LoaderCallback();
            }
        }
    }
}