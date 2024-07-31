using Project.UILoader;
using Zenject;

namespace Project.IoC.MonoInstallers
{
    public class BootInstaller : MonoInstaller<BootInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();
            
            Container
                .Bind<UIWindowPresenter>()
                .AsSingle();
        }
    }
}