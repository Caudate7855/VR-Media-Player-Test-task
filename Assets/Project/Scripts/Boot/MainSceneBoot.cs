using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Boot
{
    public class MainSceneBoot: MonoBehaviour
    {
        [Inject] private Camera _mainCamera;
        [Inject] private MediaLinks _mediaLinks;
        
        private AssetLoader _assetLoader = new();

        private void Awake()
        {
            var camera = Instantiate(_mainCamera);
            new UIWindowController(_assetLoader, _mediaLinks, camera);
        }
    }
}