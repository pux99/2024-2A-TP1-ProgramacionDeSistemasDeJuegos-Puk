using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioClip))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private OnFinishAction finishAction;
        private AudioSource _source;
        public Action<AudioPlayer> EndOfSound;
        public AudioSource Source
        {
            get
            {
                _source ??= GetComponent<AudioSource>();
                return _source;
            }
        }
        
        public enum OnFinishAction
        {
            None,
            Destroy,
            Deactivate,
        }
        
        public void Play(AudioClipData data)
        {
            Source.loop = data.Loop;
            Source.clip = data.Clip;
            Source.outputAudioMixerGroup = data.Group;
            Source.Play();
            var clipLength = data.Clip.length;
            if (finishAction == OnFinishAction.Destroy)
            {
                StartCoroutine(DestroySelfIn(clipLength));
            }
            else if (finishAction == OnFinishAction.Deactivate)
            {
                StartCoroutine(DeactivateIn(clipLength));
            }
        }

        private IEnumerator DestroySelfIn(float seconds)
        {
            yield return new WaitForSeconds(Mathf.Max(seconds, 0));
            EndOfSound(this);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        
        private IEnumerator DeactivateIn(float seconds)
        {
            yield return new WaitForSeconds(Mathf.Max(seconds, 0));
            EndOfSound(this);
            gameObject.SetActive(false);
        }
    }
}
