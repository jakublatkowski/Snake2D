using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*<summary>
 * OptionsMenu assigns values from option sliders to AudioManager and GameManager where they are storing.
 * </summary>
 */
public class OptionsMenu : MonoBehaviour
{

    public Slider gameSpeedSlider;

    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;

    public void SetSoundsVolume(float volume)
    {
        AudioManager.instance.SoundsVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.MusicVolume = volume;
    }

    public void SetGameSpeed(float interval)
    {
        GameManager.instance.GameSpeed = interval;
    }

    /*<summary>
     * On Start set all sliders to values from Managers to has appropriate values
     * </summary>
     */
    void Start()
    {
        gameSpeedSlider.value = GameManager.instance.GameSpeed;
        soundVolumeSlider.value = AudioManager.instance.SoundsVolume;
        musicVolumeSlider.value = AudioManager.instance.MusicVolume;
    }
}