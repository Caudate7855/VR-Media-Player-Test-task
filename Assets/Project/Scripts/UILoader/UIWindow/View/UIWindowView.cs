using System.Collections.Generic;
using Project.UILoader.Previews;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace Project.UILoader
{
    public class UIWindowView : MonoBehaviour
    {
        [SerializeField] private MediaPlayer _mediaPlayer;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private List<UIWindowPreview> _previews;
        
        public void Initialize(Camera camera)
        {
            _canvas.worldCamera = camera;
        }

        public MediaPlayer GetMediaPlayer()
        {
            return _mediaPlayer;
        }

        public List<UIWindowPreview> GetPreviews()
        {
            return _previews;
        }
    }
}