using UnityEngine;
using UnityEngine.Audio;


/*<summary>
 * Sound stores every component needed to manage AudioClip from AudioManager.
 * </summary>
 */
[System.Serializable]
public enum Type { SOUND, MUSIC };

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource audioSource;

    public Type type;

    public bool loop;
}