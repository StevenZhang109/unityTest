using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum ZombieType {
    Zombie1,
    Zombie2,
    FlagZombie,
    ConeHeadZombie,
    BucketHeadZombie
}
[System.Serializable]
public struct Wave
{
    [System.Serializable]
    public struct Data
    {
        public ZombieType zombieType;
        public uint Count;
    }
    public bool isLargeWave;
    [Range(0f,1f)]
    public float percentage;
    public Data[] zombieData;
}
public class GameController : MonoBehaviour
{
    public GameObject zombie1;
    GameModel _model;
    //public GameObject progressBar;
    public float readyTime;
    public float elapsedTime;
    public float playTime;

    public GameObject sunPrefab;
    public float sunInterval;

    public AudioClip readySound, zombieComing, hugeWaveSound, finalWavesound;
    public Wave[] waves;
    public Slider progressBar;

    public AudioClip loseMusic, winMusic;
    public string nextStage;
    private bool isLostGame = false;

    public GameTip gameLabel;

    //public GameObject cardDialog;
    //public GameObject sunLabel;
    public GameObject cardDialog;//卡片选取界面
    public GameObject toolBar;
    //public GameObject shovelBg;
    public GameObject btn_sumbit/*, btn_reset*/;

    public int initSun;

    public void AfterSelectCard() {
        //btn_reset.SetActive(false);
        btn_sumbit.SetActive(false);
        Destroy(cardDialog);
        toolBar.GetComponent<CardSelect>().enabled = false;
        //Destroy(cardDialog);
     
        Camera.main.transform.position = new Vector3(-0.78f, 0, -1f);
        InvokeRepeating("ProduceSun", sunInterval, sunInterval);
        StartCoroutine("workFlow");
    }
    IEnumerator gameReady()
    {
        yield return new WaitForSeconds(0.5f);
        MoveBy move = Camera.main.gameObject.AddComponent<MoveBy>();
        //move.Offset = new Vector2(3.25f, 0);
        move.Offset = new Vector2(5.7f, 0);
        move.MoveTime = 1.5f;
        move.Begin();
        yield return new WaitForSeconds(2f);
        //sunLabel.SetActive(true);
        //shovelBg.SetActive(true);
        btn_sumbit.SetActive(true);
        toolBar.SetActive(true);
        //btn_reset.SetActive(true);
        //cardDialog.SetActive(true);
    }
    IEnumerator workFlow()
    {
        yield return gameLabel.ShowStartTip();
        GetComponent<HandleForPlants>().enabled = true;
        yield return new WaitForSeconds(readyTime);
        AudioManager.GetInstance().PlaySound(readySound);
       
        showProgressBar();
        AudioManager.GetInstance().PlaySound(zombieComing);
        for (int i = 0; i < waves.Length; i++) {
            yield return StartCoroutine(waitForWavePercentage(waves[i].percentage));
            if (waves[i].isLargeWave) {
                StopCoroutine(UpdateProgress());

                gameLabel.showApproachingTip();

                //yield return StartCoroutine(WaitForZombieClear());
                yield return new WaitForSeconds(1f);
                AudioManager.GetInstance().PlaySound(hugeWaveSound);
                yield return new WaitForSeconds(1f);
                StartCoroutine(UpdateProgress());
            }
            if (i + 1 == waves.Length) {
                gameLabel.ShowFinalTip();
                AudioManager.GetInstance().PlaySound(finalWavesound);
            }
            StartCoroutine( createZombieWave( waves[i]));
        }
        yield return StartCoroutine(WaitForZombieClear());
        yield return new WaitForSeconds(2f);
        winGame();

    }
    IEnumerator WaitForZombieClear() {
        while (true) {
            bool hasZombie = false;
            for (int row = 0; row < StageMap.MaxRow; row++) {
                if (_model.zombieList[row].Count != 0) {
                    hasZombie = true;
                    break;
                }
            }
            if (hasZombie) { 
                yield return new WaitForSeconds(0.1f);
            }
            else {
                break;
            }
        }
    }
    IEnumerator waitForWavePercentage(float percentage) {
        while (true) {
            if ((elapsedTime / playTime) >= percentage)
            {
                break;
            }
            else {
                yield return null;
            }
        }
    }

    IEnumerator UpdateProgress() {
        while (true)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime / playTime;
            //progressBar.GetComponent<ProgressBar>().SetProgress(elapsedTime/playTime);
            yield return 0;
        }
    }

    void showProgressBar() {
        progressBar.gameObject.SetActive(true);
        StartCoroutine(UpdateProgress());
    }
    IEnumerator createZombieWave(Wave wave) {
        foreach (Wave.Data data in wave.zombieData) {
            for (int i = 0; i < data.Count; i++) {
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                CreateOneZombie(data.zombieType);
            }

        }
    }

    //创建僵尸
    void CreateOneZombie(ZombieType type)
    {
       
        GameObject zombie=null;
        switch (type) {
            case ZombieType.Zombie1:
                zombie = Instantiate(zombie1);
                break;
            default:
                break;
        }
        int row = Random.Range(0, StageMap.MaxRow);
        zombie.transform.position = StageMap.ZombiePosByRow(row);
        zombie.GetComponent<ZombieMove>().Row = row;
        zombie.GetComponent<ZombieSpriteDisplay>().SetOrderByRow(row);
        _model.zombieList[row].Add(zombie);
        

    }
    void OnDrawGizmos()
    {
         DeBugDrawGrid(new Vector2(-4.46f,-2.73f),0.8f,1f,9,5,Color.blue);
    }

    void DeBugDrawGrid(Vector3 _orgin, float x, float y, int col, int row, Color color)
    {
        for (int i = 0; i < col + 1; i++)
        {
            Vector3 startPoint = _orgin + Vector3.right * i * x;
            Vector3 endPoint = startPoint + Vector3.up * row * y;
            Debug.DrawLine(startPoint, endPoint, color);
        }
        for (int i = 0; i < row + 1; i++)
        {
            Vector3 startPoint = _orgin + Vector3.up * i * y;
            Vector3 endPoint = startPoint + Vector3.right * col * x;
            Debug.DrawLine(startPoint, endPoint, color);
        }
    }
    void Awake()
    {
        _model = GameModel.GetInstance();
        Time.timeScale = 1;
    }
    // Use this for initialization
    void Start()
    {
        _model.clear();
        _model.sun = initSun;
        // ProduceSun();
        StartCoroutine("gameReady");
       
        List<float> flags = new List<float>();
        for (int i=0;i<waves.Length;i++) {
            if (waves[i].isLargeWave) {
                flags.Add(waves[i].percentage);
            }
        }
        //cardDialog.SetActive(false);
        //sunLabel.SetActive(false);
        //shovelBg.SetActive(false);
        //btn_reset.SetActive(false);
        btn_sumbit.SetActive(false);
        GetComponent<HandleForPlants>().enabled = false;
        //progressBar.GetComponent<ProgressBar>().InitWithFlag(flags.ToArray());
        progressBar.gameObject.SetActive(false);
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            _model.sun += 50;
        }

        if (!isLostGame) {
            for (int row=0;row<_model.zombieList.Length;row++) {
                foreach (GameObject zombie in _model.zombieList[row]) {
                    if (zombie.transform.position.x<StageMap.LeftGrid-0.4f) {
                        LoseGame();
                        isLostGame = true;
                        return;
                    }
                }
            }
        }
    }

    void ProduceSun() {
        float x = Random.Range(StageMap.LeftGrid, StageMap.RightGrid);
        float y = Random.Range(StageMap.BottomGrid, StageMap.TopGrid);
        float startY = StageMap.TopGrid + 1.5f;
        GameObject sun = Instantiate(sunPrefab);
        sun.transform.position = new Vector3(x, startY);
        MoveBy move = sun.AddComponent<MoveBy>();
        move.Offset = new Vector3(0,y-startY,0);
        move.MoveTime = (startY - y) / 1f;
        move.Begin();
    }

    void LoseGame() {
        gameLabel.ShowLostTip();
        GetComponent<HandleForPlants>().enabled = false;
        CancelInvoke("ProduceSun");
        AudioManager.GetInstance().PlayMusic(loseMusic,false);
        GetComponent<StartMenu>().RestartStage(4.5f);
    }
    void winGame() {
        CancelInvoke("ProduceSun");
        AudioManager.GetInstance().PlayMusic(winMusic,false);
        Invoke("GotoNextStage",3f);
    }

    void GotoNextStage() {
        SceneManager.LoadScene(nextStage);
    }
}
