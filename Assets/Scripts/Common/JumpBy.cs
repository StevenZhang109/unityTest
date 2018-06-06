using UnityEngine;
using System.Collections;

public class JumpBy : MonoBehaviour {

     public Vector3 offset;
    public float height;
    public float time;
    Vector3 oriPos;
    Vector3 offsetSpeed;
    float curTime=0f;

    void Awake() {
        enabled = false;
    }
    //这种组件是动态加载的 初始化时要计算的东西需要创造者提供 不能在awake中自我进行 
    public void Begin() {
        offsetSpeed = offset / time;
        oriPos = transform.position;
        enabled = true;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (curTime < time) { 
            curTime += Time.deltaTime;
            Vector3 newPos = oriPos + offset * curTime;
            newPos.y += Mathf.Sin(curTime / time * Mathf.PI)*height;
            transform.position = newPos;
        }
    }
}
