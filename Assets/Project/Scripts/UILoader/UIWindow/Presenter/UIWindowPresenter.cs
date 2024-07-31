using UnityEngine;

namespace Project.UILoader
{
    public class UIWindowPresenter
    {
        private IAssetLoader _assetLoader;
        
        private UIWindowView _uiWindowView;
        private UIWindowModel _uiWindowModel;

        public UIWindowPresenter(IAssetLoader assetLoader ,string uiWindowViewAddress)
        {
            _assetLoader = assetLoader;
            InitializeWindow(uiWindowViewAddress);
        }

        private async void InitializeWindow(string uiWindowViewAddress)
        {
            _uiWindowView = await _assetLoader.Load<UIWindowView>(uiWindowViewAddress);
            GameObject.Instantiate(_uiWindowView);
        }
    }
}