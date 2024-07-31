using UnityEngine;
using Zenject;

namespace Project.IoC
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SoInstaller : ScriptableObjectInstaller<SoInstaller>
    {
        [SerializeField] private MediaLinks _mediaLinks;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MediaLinks>()
                .FromInstance(_mediaLinks)
                .AsSingle();
        }
    }
}