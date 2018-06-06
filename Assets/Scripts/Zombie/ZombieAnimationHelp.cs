using UnityEngine;
using System.Collections;

public class ZombieAnimationHelp : MonoBehaviour {
    public GameObject Zombie;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void WalkStop() {Zombie. SendMessage("WalkStop"); }
    void WalkResume() {Zombie. SendMessage("WalkResume"); }
}
