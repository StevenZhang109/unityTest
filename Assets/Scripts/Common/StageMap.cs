using UnityEngine;
using System.Collections;

public class StageMap {

    public const int MaxRow = 5;
    public const int MaxCol = 9;
    public const float TopGrid = 2.3f;
    public const float LeftGrid = -4.46f;
    public const float BottomGrid = -2.7f;
    public const float RightGrid = 2.74f;
    public const float GridWidth = 0.8f;
    public const float GridHeight = 1;

    public static Vector2 PlantPosByRowCol(int row,int col)
    {
        return new Vector2(LeftGrid+0.4f+GridWidth*col,TopGrid-0.5f-GridHeight*row);
    }
    public static Vector2 ZombiePosByRow(int row)
    {
        float offsetX = Random.Range(1.2f, 1.8f);
        return new Vector2(RightGrid + offsetX, TopGrid - 0.3f - row * GridHeight);
    }
    public static bool IsPointInMap(Vector3 point)
    {
        return LeftGrid<=point.x && point.x <= RightGrid 
            && BottomGrid<=point.y && point.y <= TopGrid ;
    }
    public static void GetRowCol(Vector2 point,out int row,out int col)
    {
        col = Mathf.FloorToInt((point.x-LeftGrid)/GridWidth);
        row = Mathf.FloorToInt((TopGrid - point.y) / GridHeight);
    }
	
}
