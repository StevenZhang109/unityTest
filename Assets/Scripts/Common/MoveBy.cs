using UnityEngine;
using System.Collections;

public class MoveBy : MonoBehaviour {
    public Vector2 Offset;
    public float MoveTime;
    float _curTime=0;
    Vector2 _velocity;

    void Awake()
    {
        enabled = false;
       
       
    }
    public void Begin()
    {
        enabled = true;
        _velocity = Offset / MoveTime;
    }

	void Update () {
        if (_curTime >= MoveTime) Destroy(this);
        _curTime += Time.deltaTime;
        transform.Translate(_velocity*Time.deltaTime);
	}
}
