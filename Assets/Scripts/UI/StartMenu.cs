using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    //只有重置关卡和回到主菜单
    public void RestartStage(float delay)
    {
        Invoke("RestartStage", delay);
    }
    public void RestartStage() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackHome() {
        SceneManager.LoadScene("HomeMenu");
    }
}
