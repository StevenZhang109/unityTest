using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    public AudioClip pauseSound;
    public Text text;
    AudioManager am;
    void Awake() {
        am = AudioManager.GetInstance();
    }

    public void OnClick() {
        if (text.text == "暂停")
        {
            PauseGame();
        }
        else
            ResumeGame();
    }
    public void PauseGame() {
        // text.text = "继续";
        am.PauseAllSounds();
        am.PlaySound(pauseSound);
        Time.timeScale = 0;
        am.PauseMusic();
    }
    public void ResumeGame() {
        //text.text = "暂停";
        Time.timeScale = 1;
        am.ResumeAllSounds();
        am.PlaySound(pauseSound);
        am.ResumeMusic();
    }
}
