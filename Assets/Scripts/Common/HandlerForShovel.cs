using UnityEngine;
using System.Collections;

public class HandlerForShovel : MonoBehaviour {

    public AudioClip shovelLift, shovelCancel;
    public GameObject shovelBg;
    GameObject shovel;
    GameObject selectedPlant;

    void Update() {
        handleMouseDownForShovel();
        handleMouseMoveForShovel();
    }

    void handleMouseDownForShovel() {
        if (Input.GetMouseButtonDown(0)) {
            if (shovelBg.GetComponent<Collider2D>().OverlapPoint(Utility.GetMouseWorldPos()))
            {
                CancelSelectShovel();
                shovel = shovelBg.GetComponent<Shovel>().shovel.gameObject;
                shovelBg.SendMessage("OnSelect");
                AudioManager.GetInstance().PlaySound(shovelLift);
            }
            else if (shovel)
            {
                if (selectedPlant)
                {
                    selectedPlant.GetComponent<PlantHealth>().Die();
                    selectedPlant = null;
                    CancelSelectShovel();
                }
            }
            else {
                CancelSelectShovel();
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            CancelSelectShovel();
        }
    }
    void handleMouseMoveForShovel() {
        if (!shovel) return;

        Vector3 pos = Utility.GetMouseWorldPos();
        Vector3 shovelPos = pos;
        shovelPos.x += 0.1f;
        shovelPos.y += 0.1f;
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(shovelBg.transform as RectTransform, Input.mousePosition,Camera.main, out localPos);
        shovel.transform.localPosition =localPos;
       
        if (!StageMap.IsPointInMap(pos)) return;

        int row, col;
        StageMap.GetRowCol(pos, out row, out col);
        GameObject plant = GameModel.GetInstance().PlantsInMap[row, col];

        if (selectedPlant == plant) return; //铁铲从当前指向的植物 到了其他地方（有植物或没植物）

        if (selectedPlant)//如果原来有指向的植物
        {
            selectedPlant.GetComponent<PlantSpriteDisplay>().SetAlpha(1f);

        }
        if (plant)//如果指向了新植物
        {
            selectedPlant = plant;
            selectedPlant.GetComponent<PlantSpriteDisplay>().SetAlpha(0.6f);
        }
        else
        {
            selectedPlant = null;
        }




    }

    void CancelSelectShovel() {
        if (!shovel) return;

        shovelBg.GetComponent<Shovel>().CancelSelect();
        shovel = null;
        AudioManager.GetInstance().PlaySound(shovelCancel);

        if (selectedPlant)
        {
            selectedPlant.GetComponent<PlantSpriteDisplay>().SetAlpha(1f);
            selectedPlant = null;
        }

    }

}
