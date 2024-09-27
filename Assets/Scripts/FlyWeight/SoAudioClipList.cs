using Audio;
using Enemies;
using UnityEngine;

namespace FlyWeight
{
    [CreateAssetMenu(fileName = "AudioClipList", menuName = "ScriptableObjects/AudioClipList")]
    public class SoAudioClipList : ScriptableObject
    {
        public RandomContainer<AudioClipData> clips;
    }
}