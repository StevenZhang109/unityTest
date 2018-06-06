using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public bool MusicOn = true;
    public bool SoundOn = true;
    float _musicVolume = 1f;
    float _soundVolume = 1f;

    GameObject _obj;
    AudioSource _audio;
    ArrayList sounds = new ArrayList();

   static AudioManager _instance=null;

    public float MusicVolume {
        get { return _musicVolume; }
        set {
            if (value != _musicVolume)
            {
                _musicVolume = value;
                _audio.volume = value;
            }
        }

    }

    public float SoundVolume {
        get { return _soundVolume; }
        set {
            if (_soundVolume != value) {
                _soundVolume = value;
                foreach (AudioSource src in sounds) {
                    src.volume = value;

                }
            }
        }
    }
    public static AudioManager GetInstance()
    {
       
        if (_instance == null)
        {
            GameObject obj = new GameObject("AudioManager");
            DontDestroyOnLoad(obj);
            _instance = obj.AddComponent<AudioManager>();
            _instance._obj = obj;
            _instance._audio = obj.AddComponent<AudioSource>();
        }
        return _instance;
    }
    public void PlayMusic(AudioClip clip)
    {
        PlayMusic(clip, true);
    }
    public void PlayMusic(AudioClip clip, bool loop)
    {
        _audio.Stop();
        _audio.clip = clip;
        _audio.volume = MusicVolume;
        _audio.loop = loop;
        if (MusicOn && Time.timeScale != 0) {
            _audio.Play();
        }


       
    }

    public void StopMusic()
    {
        _audio.Stop();
    }

    public void PauseMusic() {
        _audio.Pause();
    }

    public void ResumeMusic() {
        if (MusicOn&&Time.timeScale!=0) {
            _audio.Play();
        }
    }
    public AudioSource PlaySound(AudioClip clip) {
      return  PlaySound(clip, false);
    }
    public AudioSource PlaySound(AudioClip clip, bool loop) {
        AudioSource source = _obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = SoundVolume;
        source.loop = loop;
        sounds.Add(source);
        if (SoundOn && Time.timeScale != 0){
            source.Play();
        }
        if (!loop) {
            StartCoroutine(DoDestroy(source,clip.length));
        }
        return source;
    }

    IEnumerator DoDestroy(AudioSource sound,float delay) {
        yield return new WaitForSeconds(delay);
        if (sound != null) { 
        Destroy(sound);
        sounds.Remove(sound);
        }
    }

    public void StopSound(AudioSource sound) {
        StartCoroutine(DoDestroy(sound, 0));
    }

    public void PauseAllSounds() {
        foreach (AudioSource src in sounds) {
            src.Pause();
        }
    }

    public void ResumeAllSounds() {
        if (SoundOn&&Time.timeScale!=0) {
            foreach (AudioSource src in sounds) {
                src.Play();
            }
        }
    }

    public void Clear() {
        foreach (AudioSource src in sounds) {
            Destroy(src);
        }
        sounds.Clear();
    }
}
