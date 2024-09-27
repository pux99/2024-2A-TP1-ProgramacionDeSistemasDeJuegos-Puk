using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Pool
{
    public class AudioSourcePool : MonoBehaviour,IPool<AudioPlayer>
    {

        public static AudioSourcePool Instance { get; private set; }

        private void Awake() 
        { 
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
        private Queue<AudioPlayer> _audioSourceQueue =new Queue<AudioPlayer>();
        [SerializeField]private AudioPlayer audioSourcePrefab;

        public AudioPlayer GetElement(Vector3 pos,Quaternion rot)
        {
            if (_audioSourceQueue.Count > 0)
            {
                //Debug.Log("AudioSource reborn");
                gameObject.SetActive(true);
                return _audioSourceQueue.Dequeue();;
            }
            return CreateNewElement(pos,rot);
        }

        private AudioPlayer CreateNewElement(Vector3 pos,Quaternion rot)
        {
            //Debug.Log("New AudioSource Born");
            AudioPlayer newAudioSource = Instantiate(audioSourcePrefab, pos, rot);
            newAudioSource.EndOfSound += ReceiveElement;
            return newAudioSource;
        }

        public void ReceiveElement(AudioPlayer element)
        {
            _audioSourceQueue.Enqueue(element);
        }
    }
}
