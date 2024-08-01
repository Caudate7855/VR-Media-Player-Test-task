using System.Collections.Generic;
using Project.UILoader.Previews;
using UnityEngine;

namespace Project.UILoader
{
    public class UIWindowView : MonoBehaviour
    {
        [SerializeField] public Camera _mainCamera;
        [SerializeField] public Canvas _canvas;
        [SerializeField] private List<UIWindowPreview> _previews;

        public void Initialize(Camera camera)
        {
            _mainCamera = camera;
            _canvas.worldCamera = camera;
        }

        public List<UIWindowPreview> GetPreviews()
        {
            return _previews;
        }
    }
}