using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.URLMediaLoader
{
    [UsedImplicitly]
    public class URLMediaLoader
    {
        private static Dictionary<string, Sprite> _cashedSprites = new();
        
        public static async Task<Sprite> LoadImageAsync(string url)
        {
            if (_cashedSprites.TryGetValue(url, out var cashedSprite))
            {
                return cashedSprite;
            }
            
            using var uwr = UnityWebRequestTexture.GetTexture(url);

            var operation = uwr.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            var texture = DownloadHandlerTexture.GetContent(uwr);

            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            _cashedSprites[url] = sprite;
            
            return sprite;
        }
    }
}