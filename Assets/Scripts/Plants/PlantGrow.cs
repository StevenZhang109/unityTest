using UnityEngine;
using System.Collections;

public class PlantGrow : MonoBehaviour {
    public GameObject Soil;
    [HideInInspector]
    public int Row, Col;
    //[HideInInspector]
    //public int price;

    protected GameModel model;
    protected Transform shadow;
    protected PlantSpriteDisplay display;
    protected void Awake()
    {
        display=GetComponent<PlantSpriteDisplay>();
        model = GameModel.GetInstance();
        shadow = transform.Find("Shadow");
    }
    public virtual bool CanGrowInMap(int row,int col)
    {
        GameObject plant = model.PlantsInMap[row, col];
        if (!plant) return true;
        return false;
    }
    public virtual void Grow(int row,int col)
    {
        Row = row;
        Col = col;
        model.PlantsInMap[row, col] = gameObject;
        display.SetOrderByRow(row);
        if (shadow)
        {
            shadow.gameObject.SetActive(true);
        }
        if (Soil)
        {
            GameObject temp = Instantiate(Soil);
            temp.transform.position = transform.position-Vector3.up*0.1f;
            Destroy(temp, 0.2f);
        }
        gameObject.SendMessage("AfterGrow");
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
