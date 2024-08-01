using System;
using Project.UILoader;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using Zenject;

namespace Project.Boot
{
    public class MainSceneBoot: MonoBehaviour
    {

        [Inject] private Camera _mainCamera;
        [Inject] private MediaLinks _mediaLinks;
        
        private AssetLoader _assetLoader;
        
        private UIWindowPresenter _window;
        private string _windowPath = "UIWindow";

        private void Awake()
        {
            _assetLoader = new();
            var camera = Instantiate(_mainCamera);
            _window = new UIWindowPresenter(_assetLoader, _windowPath, _mediaLinks, camera);
        }
    }
}