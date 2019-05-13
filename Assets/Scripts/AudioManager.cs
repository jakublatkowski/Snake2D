using System;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 *  AudioManager is a class used for managing all audio clips in game.
 *  Here is created Array of sounds available from other scripts
 *  AudioManager has an ability to globally change music or sounds volume
 * </summary>
 */

public class AudioManager : MonoBehaviour
{
    [Header("Volume Settings:")]
    [SerializeField]
    [Range(0f, 1f)]
    private float _soundsVolume = 1f;

    [SerializeField]
    [Range(0f, 1f)]
    private float _musicVolume = .3f;

    [Header("Sounds Table:")]
    public Sound[] sounds;

    //static reference to existing instance of AudioManager
    public static AudioManager instance;

    public float SoundsVolume
    {
        get { return _soundsVolume; }
        set
        {
            if (_soundsVolume < 0f)
            {
                Debug.LogWarning("Sounds volume lower than 0");
                _soundsVolume = 0f;
            }
            else if (_soundsVolume > 1f)
            {
                Debug.LogWarning("Sounds volume greater than 1");
                _soundsVolume = 1f;
            }
            else
            {
                _soundsVolume = value;
            }
        }
    }

    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            if (_musicVolume < 0f)
            {
                Debug.LogWarning("Music volume lower than 0");
                _musicVolume = 0f;
            }
            else if (_musicVolume > 1f)
            {
                Debug.LogWarning("Music volume greater than 1");
                _musicVolume = 1f;
            }
            else
            {
                _musicVolume = value;
            }
        }
    }

    /*<summary>
     * Awake Method checks if there is any AudioManager and if not creates a new one;
     * Than it assigns every sound to a new AudioSource component to be able to play it whenever it's needed.
     *</summary>   
     */
    void Awake()
    {

        if(AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }
        else
        {
            //if other AudioManager exists destroy new one
            Destroy(gameObject);
            return;
        }

        // allows data transfering between scenes
        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    void Start()
    {
        PlaySound("MainTheme");
    }


    public void PlaySound(string name)
    {
        //Find in sounds array sound with given name
        Sound sound = Array.Find(sounds, s => s.name == name);

        if(sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        //and play it
        sound.audioSource.Play();
    }


    public void StopPlayingSound(string name)
    {
        //Find in sounds array sound with given name
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }

        //and stop playing it
        sound.audioSource.Stop();
    }

    /*<summary>
     * Updating audio volume
     *</summary>
     */
    void Update()
    {
        foreach(Sound s in sounds)
        {
            if(s.type == Type.SOUND)
            {
                s.audioSource.volume = AudioManager.instance.SoundsVolume;
            }
            if(s.type == Type.MUSIC)
            {
                s.audioSource.volume = AudioManager.instance.MusicVolume;
            }
        }
    }

}
