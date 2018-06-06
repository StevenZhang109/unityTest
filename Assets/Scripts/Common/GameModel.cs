using UnityEngine;
using System.Collections;

public class GameModel{
   public GameObject[,] PlantsInMap;
   static GameModel _instance;
   public static GameModel Instance { get { return _instance == null ? _instance = new GameModel() : _instance; } }
  public static GameModel GetInstance()
    {
      
        return _instance==null?_instance=new GameModel():_instance;
    }
    public ArrayList[] bulletList;
    public ArrayList[] zombieList;
    public int sun;
    GameModel()
    {
        clear();

    }
    //初始化
    public void clear()
    {
        PlantsInMap = new GameObject[StageMap.MaxRow, StageMap.MaxCol];
        bulletList = new ArrayList[StageMap.MaxRow];
        zombieList = new ArrayList[StageMap.MaxRow];
        for (int i = 0; i < bulletList.Length; i++)
        { bulletList[i] = new ArrayList();
            zombieList[i] = new ArrayList();
        }
    }
	
}
