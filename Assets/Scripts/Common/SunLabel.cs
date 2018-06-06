using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SunLabel : MonoBehaviour {
   
    GameModel _model;
    Text _sunLabel;
    void Awake() {
       
        _sunLabel = GetComponent<Text>();
        _model = GameModel.GetInstance();
    }
	
	
	void Update () {
       _sunLabel.text = _model.sun.ToString();
	}
}
