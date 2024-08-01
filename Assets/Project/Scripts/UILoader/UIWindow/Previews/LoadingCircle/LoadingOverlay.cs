using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UILoader.Previews.LoadingCircle
{
    public class LoadingOverlay : MonoBehaviour
    {
        private const float ROTATION_SPEED = 0.5f;
        
        [SerializeField] private Image _loadingCircle;
        
        public void Start()
        {
            _loadingCircle.transform.DORotate(new Vector3(0, 0, -360), 1 / ROTATION_SPEED, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}