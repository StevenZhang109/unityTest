using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardShow : MonoBehaviour {
    private Card card;
    //private TextMesh cdText;
    void Awake() {
        card = GetComponent<Card>();
    }
	// Use this for initialization
	void Start () {
        //int order = GetComponent<SpriteRenderer>().sortingOrder+1;
        //Transform priceText = transform.Find("price");
        //priceText.GetComponent<MeshRenderer>().sortingOrder = order;
        //priceText.GetComponent<TextMesh>().text = card.price.ToString();
        //Transform cd = transform.Find("cd");
        //cd.GetComponent<MeshRenderer>().sortingOrder = order;
        //cdText = cd.GetComponent<TextMesh>();
        transform.FindChild("price").GetComponent<Text>().text =card.price.ToString();
    }
	
	
	void Update () {
        //if (card.state == Card.State.Cd && card.isGrowed)
        //{
        //    cdText.text = card.CdTime.ToString("f1") + "s";
        //}
        //else {
        //    cdText.text = "";
        //}
	}
}
