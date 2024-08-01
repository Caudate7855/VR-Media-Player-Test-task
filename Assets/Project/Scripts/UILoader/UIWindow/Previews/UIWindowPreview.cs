using System;
using Project.UILoader.Previews.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UILoader.Previews
{
    public class UIWindowPreview : MonoBehaviour
    {
        public event Action<UIWindowPreview> OnButtonPressed;
            
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private int _previewID;

        private PreviewStates _currentState;

        private void Awake()
        {
            _currentState = PreviewStates.Unselected;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClickHandler);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClickHandler);
        }

        public Image GetImage()
        {
            return _image;
        }

        public void SetState(PreviewStates newState)
        {
            _currentState = newState;
        }

        public PreviewStates GetState()
        {
            return _currentState;
        }
        
        private void OnButtonClickHandler()
        {
            OnButtonPressed?.Invoke(this);
        }
        
    }
}