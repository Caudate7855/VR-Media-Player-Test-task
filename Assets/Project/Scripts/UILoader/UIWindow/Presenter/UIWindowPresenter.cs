using UnityEngine;

namespace Project.UILoader
{
    public class UIWindowPresenter
    {
        private UIWindowView _uiWindowView;
        private UIWindowModel _uiWindowModel;

        private IAssetLoader _assetLoader;
        private MediaLinks _mediaLinks;

        public UIWindowPresenter(IAssetLoader assetLoader ,string uiWindowViewAddress, MediaLinks mediaLinks)
        {
            _assetLoader = assetLoader;
            _mediaLinks = mediaLinks;
            
            InitializeWindow(uiWindowViewAddress);
        }

        private async void InitializeWindow(string uiWindowViewAddress)
        {
            _uiWindowView = await _assetLoader.Load<UIWindowView>(uiWindowViewAddress);
            Object.Instantiate(_uiWindowView);
        }
    }
}