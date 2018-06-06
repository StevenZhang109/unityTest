using UnityEngine;
using System.Collections;

public class RightUpperMenu : MonoBehaviour {
    public AudioClip pauseSound;
    public GameObject menu;//菜单界面
    bool _menuActive = false;
    AudioManager am;
    void Awake()
    {
        am = AudioManager.GetInstance();
    }
    void PauseGame()
    {
        menu.SetActive(true);
        am.PauseAllSounds();
        am.PlaySound(pauseSound);
        Time.timeScale = 0;
        am.PauseMusic();
        _menuActive = !_menuActive;
    }
    void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        am.ResumeAllSounds();
        am.PlaySound(pauseSound);
        am.ResumeMusic();
        _menuActive = !_menuActive;
    }

    public void OnClick() {
        if (_menuActive)
        {
          
            ResumeGame();
        }
        else
        {
            
            PauseGame();
        }
       
    }
    
  
}
