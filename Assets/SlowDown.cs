using UnityEngine;
using System.Collections;

public class SlowDown : MonoBehaviour {

    [HideInInspector]
    public float ratio = 1f;

    private ZombieSpriteDisplay _display;
    private Animator _anmt;
    private int _state;// 0  1
    private float _slowDownRatio;

    void Awake()
    {
        _display = GetComponent<ZombieSpriteDisplay>();
        _anmt = transform.Find("zombie").GetComponent<Animator>();
        _state = 0;
    }

    public void ActionSlowDown(float time, float val)
    {
        _slowDownRatio = val;
        _state = 1;
        UpdateAction();
        if (IsInvoking("RemoveSeedDown"))
        {
            CancelInvoke("RemoveSeedDown");
        }
        Invoke("RemoveSeedDown", time);
    }

    public void RemoveSeedDown()
    {
        _state = 0;
        UpdateAction();
    }
    void UpdateAction()
    {
        float val;
        if (_state == 1)
        {
            val = _slowDownRatio;
            _display.SetColor(0.5f, 0.5f, 1f);
        }
        else
        {
            val = 1;
            _display.SetColor(1f, 1f, 1f);
        }
        ratio = val;
        _anmt.speed = val;
    }
}
