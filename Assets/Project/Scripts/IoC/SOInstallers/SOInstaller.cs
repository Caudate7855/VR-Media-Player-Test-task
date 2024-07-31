using UnityEngine;
using Zenject;

namespace Project.IoC
{
    public class SoInstaller : ScriptableObjectInstaller<SoInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<MediaLinks>()
                .AsSingle();
        }
    }
}