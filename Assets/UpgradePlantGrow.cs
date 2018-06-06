using UnityEngine;
using System.Collections;

public class UpgradePlantGrow :PlantGrow{
    public string targetPlantTag;
    public override bool CanGrowInMap(int row, int col)
    {
        GameObject plant = model.PlantsInMap[row, col];
        if (plant) {
            if (plant.tag == targetPlantTag) {
                return true;
            }
        }
        return false;
    }
    public override void Grow(int row, int col)
    {
        Row = row;
        Col = col;
        Destroy(model.PlantsInMap[row, col]);
        model.PlantsInMap[row, col] = gameObject;
        display.SetOrderByRow(row);

        if (shadow)
        {
            shadow.gameObject.SetActive(true);
        }
        if (Soil)
        {
            GameObject temp = Instantiate(Soil);
            temp.transform.position = transform.position - Vector3.up * 0.1f;
            Destroy(temp, 0.2f);
        }
        gameObject.SendMessage("AfterGrow");
    }
   
}
