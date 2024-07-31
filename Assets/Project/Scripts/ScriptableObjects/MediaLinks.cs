using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "MediaLinks", menuName = "Media/Links", order = 1)]
    public class MediaLinks : ScriptableObject
    {
        public List<string> PreviewImagesLinks => _previewImagesLinks;
        public List<string> VideoLinks => _videoLinks;
        
        [SerializeField] private List<string> _previewImagesLinks;
        [SerializeField] private List<string> _videoLinks;
    }
}