using UnityEngine;
using System.Collections;

public class SearchZombie : MonoBehaviour {
    GameModel _model;
    void Awake()
    {
        _model = GameModel.GetInstance();
    }
    public bool isZombieInRange(int row, float min, float max)
    {
        foreach (GameObject b in _model.zombieList[row])
        {
            float dis = b.transform.position.x - transform.position.x;
            if (min <= dis && dis <= max)
                return true;
            
               
        }
        return false;
    }
    public GameObject SearchClosestZombie(int row, float min, float max)
    {
        float minDis = 10000f;
        GameObject closestZombie = null;
        foreach (GameObject b in _model.zombieList[row])
        {
           
            float dis = b.transform.position.x - transform.position.x;
            if (min <= dis && dis <= max && Mathf.Abs(dis)<minDis )
            {
                minDis = Mathf.Abs(dis);
                closestZombie = b;
            }


        }
        return closestZombie;
    }
    public object[] SearchRowZombiesInRange(int row, float min, float max)
    {
        ArrayList zombies = new ArrayList();
        foreach (GameObject zombie in _model.zombieList[row])
        {
            float dis = zombie.transform.position.x - transform.position.x;
            if (min <= dis && dis <= max)
            {
                zombies.Add(zombie);
            }
        }
        return zombies.ToArray();
    }
}
