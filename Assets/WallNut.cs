using UnityEngine;
using System.Collections;

public class WallNut : MonoBehaviour {
    PlantHealth _plantHealth;
    Animator _anmt;
	// Use this for initialization
	void Start () {
        _plantHealth = GetComponent<PlantHealth>();
        _anmt =transform.Find("Plant"). GetComponent<Animator>();
      
	}
	
	
	void Update () {
        _anmt.SetInteger("hp", _plantHealth.Hp);
	}

    void AfterGrow() {

    }
}
