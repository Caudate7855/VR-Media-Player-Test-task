using System;
using JetBrains.Annotations;

namespace Project.SerializableMedia
{
    [Serializable]
    public class SerializableMediaLinks
    {
        public int EpisodeNumber;
        public string PreviewURL;
        public string VideoURL;
    }
}