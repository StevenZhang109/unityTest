using UnityEngine;
using System.Collections;
using System;

public class PlantSpriteDisplay : MonoBehaviour ,ISpriteDisplay{
    public int OrderOffset = 0;
    SpriteRenderer _shadow, _plant;
    void Awake()
    {
        _shadow = transform.Find("Shadow").GetComponent<SpriteRenderer>();
        _plant = transform.Find("Plant").GetComponent<SpriteRenderer>();
    }
    public void SetAlpha(float a)
    {
        Color color = Color.white;
        color.a = a;
        _shadow.color = color;
        _plant.color = color;
    }
    public void SetColor(float r, float g, float b)
    {
        Color color = new Color(r, g, b, _plant.color.a);
        _plant.color = color;
    }
    public void SetOrder(int order)
    {
        _plant.sortingOrder = order;
    }

    public void SetOrderByRow(int row)
    {
        _plant.sortingOrder = 1000 * (row + 1) + OrderOffset;
    }
    public void ToYellow() {
        StartCoroutine("toYellow");
    }
    IEnumerator toYellow() {

        while (true) {
            Vector3 c = new Vector3(_plant.color.r,_plant.color.g,_plant.color.b)  ;
            Vector3 yellow = new Vector3(Color.blue.r, Color.blue.g, Color.blue.b);
            Vector3 newColor = Vector3.MoveTowards(c, yellow, 0.05f);

            _plant.color = new Color(newColor.x, newColor.y, newColor.z);
            print(_plant.color.r+" "+ _plant.color.g+" "+_plant.color.b);

            yield return 0;
        }
    }
}
