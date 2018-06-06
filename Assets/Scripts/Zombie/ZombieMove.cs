using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour {

    public float speed = 0.2f;
    float _curSpeed ;
    float _lowSpeed;//for animation
    SlowDown _slowDown;
    public int Row;

    public void WalkStop() { _curSpeed = _lowSpeed; }
    public void WalkResume() { _curSpeed = speed; }
	void Start () {
        _lowSpeed = speed * 0.1f;
        _curSpeed = speed;
        _slowDown = GetComponent<SlowDown>();
	}
	

	void Update () {
        transform.Translate(-_curSpeed*Time.deltaTime*_slowDown.ratio,0,0);
	}
}
