using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "MediaLinks", menuName = "Media/Links", order = 1)]
    public class MediaLinks : ScriptableObject
    {
        [SerializeField] private List<string> _previewImagesLinks;
        [SerializeField] private List<string> _videoLinks;
    }
}