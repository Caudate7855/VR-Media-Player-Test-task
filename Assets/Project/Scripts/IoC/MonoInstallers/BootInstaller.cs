using UnityEngine;
using Zenject;

namespace Project.IoC.MonoInstallers
{
    public class BootInstaller : MonoInstaller<BootInstaller>
    {
        [SerializeField] private Camera _mainCamera;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_mainCamera)
                .AsSingle();
            
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();
        }
    }
}