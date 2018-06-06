using UnityEngine;
using System.Collections;
using System;

public class ZombieSpriteDisplay : MonoBehaviour,ISpriteDisplay {
    SpriteRenderer _shadow, _zombie;
    public int OrderOffset = 2;
    void Awake() {
        _shadow = transform.Find("Shadow").GetComponent<SpriteRenderer>();
        _zombie = transform.Find("zombie").GetComponent<SpriteRenderer>();
    }

    public void SetAlpha(float a)
    {
        Color color = _zombie.color;
        color.a = a;
        _zombie.color = color;
        _shadow.color = color;
    }

    public void SetOrder(int order)
    {
        _zombie.sortingOrder = order;

    }

    public void SetOrderByRow(int row)
    {
        _zombie.sortingOrder = 1000 * (row + 1) + OrderOffset;
    }

    public void SetColor(float r,float g,float b)
    {
        Color color = new Color(r, g, b, _zombie.color.a);
        _zombie.color = color;
    }
    public void Blink()
    {
        StopCoroutine("blink");
        StartCoroutine("blink");
        
    }

    IEnumerator blink()
    {
        //float adder = 0;
        //while (adder < 3.14f) {
        //    adder += 0.05f;
        //    SetAlpha(Mathf.Abs((Mathf.Cos(adder))+0.5f)/1.5f);
        //    yield return 0;
        //}
        float curAlpha = 1;
        const float target1 = 0.5f;
        //float storeAlpha = _zombie.color.a;
        while (curAlpha >= target1)
        {
            curAlpha -= 0.08f;
            SetAlpha(curAlpha);
            yield return null;
        }
        while (curAlpha <= 1)
        {
            curAlpha += 0.08f;
            SetAlpha(curAlpha);
            yield return null;
        }
        //SetColor(1, 1, 1);
        SetAlpha(1);
    }
}
