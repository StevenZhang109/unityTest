using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour {

    public Toggle musicToggle,soundToggle;

    public Slider musicSlider, soundSlider;

    AudioManager am;
    void Awake() {
        am = AudioManager.GetInstance();
    }
    void Start() {
        am.Clear();
        am.MusicOn = musicToggle.isOn;
        am.SoundOn = soundToggle.isOn;
        am.MusicVolume = musicSlider.value;
        am.SoundVolume = soundSlider.value;
    }

    public void OnMusicToggleChange(){
        am.MusicOn = musicToggle.isOn;
        if (musicToggle.isOn)
        {
            am.ResumeMusic();
        }
        else {
            am.PauseMusic();
        }
    }
    public void OnSoundToggleChange()
    {
        am.SoundOn = soundToggle.isOn;
        if (soundToggle.isOn)
        {
            am.ResumeAllSounds();
        }
        else
        {
            am.PauseAllSounds();
        }
    }

    public void onMusicSliderValueChanged() {
        am.MusicVolume = musicSlider.value;
    }

    public void OnSoundSliderValueChanged() {
        am.SoundVolume = soundSlider.value;
    }
}
