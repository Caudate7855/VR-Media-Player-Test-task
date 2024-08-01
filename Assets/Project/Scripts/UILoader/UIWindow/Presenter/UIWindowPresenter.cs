using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.UILoader
{
    public class UIWindowPresenter
    {
        private UIWindowView _uiWindowView;
        private UIWindowModel _uiWindowModel;

        private  IAssetLoader _assetLoader;
        private  MediaLinks _mediaLinks;

        public UIWindowPresenter(IAssetLoader assetLoader ,string uiWindowViewAddress, MediaLinks mediaLinks, Camera camera)
        {
            _assetLoader = assetLoader;
            _mediaLinks = mediaLinks;
            
            Initialize(uiWindowViewAddress, camera);
        }

        private async void Initialize(string uiWindowViewAddress, Camera camera)
        {
            await InitializeWindow(uiWindowViewAddress, camera);
            await InitializePreviews();
        }

        private async UniTask InitializeWindow(string uiWindowViewAddress, Camera camera)
        {
            var uiWindowView = await _assetLoader.Load<UIWindowView>(uiWindowViewAddress);
            _uiWindowView = Object.Instantiate(uiWindowView);
            _uiWindowView.MainCamera = camera;
            _uiWindowView.Canvas.worldCamera = camera;
        }

        private async UniTask InitializePreviews()
        {
            for (int i = 0, count = _uiWindowView.Previews.Count; i < count; i++)
            {
                _uiWindowView.Previews[i].Image.sprite = 
                    await URLMediaLoader.URLMediaLoader.LoadImageAsync(_mediaLinks.SerializableMediaLinks[i].PreviewURL);
            }
        }
    }
}