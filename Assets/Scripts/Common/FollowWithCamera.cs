using UnityEngine;
using System.Collections;

public class FollowWithCamera : MonoBehaviour {
    Vector3 offset;
    void Start() {
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0;
        offset = transform.position - pos;
    }


    void Update() {
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0;
        transform.position =pos+offset;
    }
}
