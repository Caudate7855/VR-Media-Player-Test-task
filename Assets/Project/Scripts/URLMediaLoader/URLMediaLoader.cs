using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Project.URLMediaLoader
{
    public class URLMediaLoader
    {
        public static async Task<Sprite> LoadImageAsync(string url)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                var operation = uwr.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    return null;
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    return sprite;
                }
            }
        }
    }
}