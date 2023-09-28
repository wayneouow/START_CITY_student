using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Meta
{
    [CreateAssetMenu(fileName = "audio-list", menuName = "城市遊戲/遊戲音效清單")]
    public class LOAudioListMeta : ScriptableObject
    {
        public List<AudioClip> AudioList = new List<AudioClip>();
    }
}
