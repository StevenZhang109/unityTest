using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
    public GameObject FlagPrefab;
     RectTransform _flag;
    public const float leftX = -0.69f;
    public const float rightX = 0.69f;
    Material FillMaterial;
    GameObject _head;
    void Awake() {
        //Transform test = transform.Find("Image");
        _flag = transform.Find("Image").transform as RectTransform;
        Vector3 lpos= _flag.localPosition;
        _flag.anchoredPosition = new Vector2(0,0);
        int i = 0;
        //FillMaterial = transform.Find("fill").GetComponent<SpriteRenderer>().material;
        //_head = transform.Find("head").gameObject;
    }
    public void InitWithFlag(float[] percentage) {
        for (int i = 0; i < percentage.Length; i++) {
           
            //GameObject flag = Instantiate(FlagPrefab);
            //flag.transform.parent = transform;
            //float val = Mathf.Clamp(percentage[i], 0f, 1f);
            //float x = Mathf.Lerp(rightX,leftX,val);
            //flag.transform.localPosition = new Vector3(x,0.06f,0);
        }
    }
    public void SetProgress(float ratio) {
        //ratio = Mathf.Clamp(ratio, 0f, 1f);
        //FillMaterial.SetFloat("_Progress,", ratio);
        //float x = Mathf.Lerp(rightX, leftX, ratio);
        //_head.transform.localPosition = new Vector3(x, 0, 0);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
