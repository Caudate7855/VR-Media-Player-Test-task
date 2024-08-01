using System;
using Project.UILoader.Previews.Enums;
using Project.UILoader.Previews.LoadingCircle;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UILoader.Previews
{
    public class UIWindowPreview : MonoBehaviour
    {
        public event Action<UIWindowPreview> OnButtonPressed;
        
        public string EpisodeName;

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private LoadingOverlay _loadingOverlay;
        [SerializeField] private int _previewIndex;


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

        public int GetIndex()
        {
            return _previewIndex;
        }
        
        public Image GetImage()
        {
            return _image;
        }

        public LoadingOverlay GetLoadingOverlay()
        {
            return _loadingOverlay;
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