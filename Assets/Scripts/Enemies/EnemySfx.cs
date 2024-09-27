using System;
using Audio;
using FlyWeight;
using Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySfx : MonoBehaviour
    {
        [SerializeField] private AudioPlayer audioSourcePrefab;
        [SerializeField] private SoAudioClipList spawnClips;
        [SerializeField] private SoAudioClipList explosionClips;
        [SerializeField] private ISoundService _soundService;

        private Enemy _enemy;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();
    
        private void FetchComponents()
        {
            // "a ??= b" is equivalent to "if(a == null) a = b" 
            _enemy ??= GetComponent<Enemy>();
        }

        private void Start()
        {
            _soundService = ServiceLocator.Instance.GetService<ISoundService>();
        }

        private void OnEnable()
        {
            if (!audioSourcePrefab)
            {
                Debug.LogError($"{nameof(audioSourcePrefab)} is null!");
                return;
            }
            _enemy.OnSpawn += HandleSpawn;
            _enemy.OnDeath += HandleDeath;
        }
        
        private void OnDisable()
        {
            _enemy.OnSpawn -= HandleSpawn;
            _enemy.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            PlayRandomClip(explosionClips.clips, audioSourcePrefab);
        }

        private void HandleSpawn()
        {
            PlayRandomClip(spawnClips.clips, audioSourcePrefab);
        }

        private void PlayRandomClip(RandomContainer<AudioClipData> container, AudioPlayer sourcePrefab)
        {
            if (!container.TryGetRandom(out var clipData))
                return;
            _soundService.PLay(clipData,transform.position, transform.rotation);
            //SpawnSource(sourcePrefab).Play(clipData);
        }

        private AudioPlayer SpawnSource(AudioPlayer prefab)
        {
            return Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
