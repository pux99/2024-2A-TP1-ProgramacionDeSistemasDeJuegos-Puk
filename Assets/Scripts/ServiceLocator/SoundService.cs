using UnityEngine;
using Pool;
using Audio;

public class SoundService :MonoBehaviour, ISoundService
{
    private void Awake()
    {
        ServiceLocator serviceLocator = ServiceLocator.Instance;
        serviceLocator.RegisterService<ISoundService>(this);
    }

    public void PLay(AudioClipData audioClip, Vector3 position, Quaternion rotation)
    {
        AudioSourcePool.Instance.GetElement(transform.position, transform.rotation).Play(audioClip);
    }
}
