using UnityEngine;
using System.Collections;

public class Shovel : MonoBehaviour {
    public RectTransform shovel;
    Vector3 oriPos;
    Quaternion oriRot;
    void Awake() {
        oriPos = shovel.anchoredPosition;
       
        oriRot = shovel.rotation;

        //print(oriPos.x + " " + oriPos.y);
        //print(shovel.transform.localPosition.x +" "+ shovel.transform.localPosition.y);
    }

    void OnSelect() {
       
        //shovel.Rotate(0, 0, 45);
    }

    public void CancelSelect() {
        shovel.anchoredPosition = oriPos;
        shovel.rotation = oriRot;
    }

	
}
