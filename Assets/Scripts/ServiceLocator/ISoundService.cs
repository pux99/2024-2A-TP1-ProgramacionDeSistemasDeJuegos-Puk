using UnityEngine;
using Audio;

public interface ISoundService
{
    void PLay(AudioClipData audioClip,Vector3 position, Quaternion rotation);
}
