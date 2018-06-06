using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
    SpriteRenderer _render;
    private float _time;
    private float _curTime;
    void Awake()
    {
        enabled = false;
        _render = GetComponent<SpriteRenderer>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	
	void Update () {
        _curTime += Time.deltaTime;
        Color color = _render.color;
        if (_curTime < _time)
        {
            color.a = 1 - _curTime / _time;
        }
        else
        {
            color.a = _curTime / _time - 1;
            if (_curTime > _time * 2)
            {
                enabled = false;
            }
        }
        _render.color = color;
	}

    public void Begin(float t)
    {
        enabled = true;
        _time = t / 2;
        _curTime = 0;
    }
}
