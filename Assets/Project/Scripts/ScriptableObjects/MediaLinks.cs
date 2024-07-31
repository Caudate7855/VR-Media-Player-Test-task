using System.Collections.Generic;
using Project.SerializableMedia;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "MediaLinks", menuName = "Media/Links", order = 1)]
    public class MediaLinks : ScriptableObject
    {
        public List<SerializableMediaLinks> SerializableMediaLinks => _serializableMediaLinks;
        
        [SerializeField] private List<SerializableMediaLinks> _serializableMediaLinks;
    }
}