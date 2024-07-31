using Project.AssetLoader;
using Zenject;

namespace Project.IoC.MonoInstallers
{
    public class BootInstaller : MonoInstaller<BootInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader.AssetLoader>()
                .AsSingle();
        }
    }
}