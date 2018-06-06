using UnityEngine;
using System.Collections;

public class Utility{
 
    public const string tag_Card = "Card";   //顶部卡片栏的卡片

    public const string tag_SelectCard = "SelectCard";  //游戏开始选卡片的界面里的卡片

    //public const string tag_flyCard = "FlyCard";
    public static Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
