using Cysharp.Threading.Tasks;

namespace Project.AssetLoader
{
    public interface IAssetLoader
    {
        public UniTask<T> Load<T>(string path);

        public void Unload();
    }
}