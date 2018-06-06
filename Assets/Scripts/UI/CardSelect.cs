using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour {
    public GameObject[] cards;
    public int maxCardNumber;
    float xOffset = 1.1f, yOffset = 0.6f;

    ArrayList selectedCards = new ArrayList();
    List<GameObject> _selectedCards = new List<GameObject>();

    //ArrayList barCardlist = new ArrayList();
    //List<GameObject> _cardInBarList = new List<GameObject>();

    Dictionary<GameObject, int> _cardsInBar = new Dictionary<GameObject, int>();//顶部卡片引用 key关联到选取界面的卡片们的索引  想通过什么查 就把它设成key
    List<Card> _cardsInContainer = new List<Card>();//选取界面的卡片引用 用来设黑白
    List<bool> _cardsEnable = new List<bool>(); //每个enable值对应上面那个list的每个卡片  选过的卡片 enable为false
    List<GameObject> _flyCardList = new List<GameObject>();

    GameObject _gameController;
    GameObject _cardBar;
    public GameObject[] _cardBarBgList;

    

    void Awake() {

        //GameObject test = Instantiate(cards[0]);
        //test.transform.parent = gameObject.transform;
        //test.transform.localPosition = Vector3.zero;
        //test.transform.localScale = Vector3.one;
        //test.transform.SetAsLastSibling();

        _gameController = GameObject.Find("GameController");
        //_cardBar = GameObject.Find("Cards");
        _cardBar = transform.Find("cardBar/cards").gameObject;
       
    }
    void Start() {
        Transform container = transform.Find("plantSelectDialog/selectCardPanel/cardContainer");
        for (int i = 0; i < cards.Length; i++) {

            GameObject cardObj = createCard(cards[i], container);
            cardObj.tag = Utility.tag_SelectCard;
            Card card = cardObj.GetComponent<Card>();
            _cardsInContainer.Add(card);


        }
    }
    GameObject createCard(GameObject prefab,Transform parent) {
        GameObject Obj = Instantiate(prefab);
        Obj.transform.parent =parent;
        Obj.transform.position = prefab.transform.position;//有的卡是直接放在layout里的 这些属性设了没用 纯粹给飞的牌用的
        Obj.transform.localScale = Vector3.one;
        Obj.transform.SetAsLastSibling();
        Obj.tag = "Untagged";
        RectTransform rect = Obj.transform as RectTransform;
        rect.sizeDelta = new Vector2(46,65);
        return Obj;
    }
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
           
            GameObject curSel = EventSystem.current.currentSelectedGameObject;
            if (curSel && curSel.tag == Utility.tag_SelectCard && _cardsInBar.Count < maxCardNumber)
            {
                Card card = curSel.GetComponent<Card>();
                if (card.state == Card.State.Disable) return;
                GameObject flyCard = createCard(curSel, transform);
                _flyCardList.Add(flyCard);
                //flyCard.tag = Utility.tag_flyCard;
                card.state = Card.State.Disable;

                GameObject newCard = createCard(curSel, _cardBar.transform);
                newCard.tag = Utility.tag_Card;
                newCard.GetComponent<Card>().state = Card.State.Normal;
                newCard.SetActive(false);
                _cardsInBar.Add(newCard, card.transform.GetSiblingIndex());

                Vector3 flyTarget = _cardBarBgList[_cardsInBar.Count - 1].transform.position;

                StartCoroutine(flyToBar(flyCard, flyTarget, newCard));


            }
            else if (curSel && curSel.tag == Utility.tag_Card)
            {
                //if (_cardsInBar.ContainsKey(curSel))
                //{
              
               
               

                GameObject flyCard = createCard(curSel, transform);
                _flyCardList.Add(flyCard);
                //flyCard.tag = Utility.tag_flyCard;
                StartCoroutine(flyToContainer(flyCard, _cardsInContainer[_cardsInBar[curSel]].transform.position, _cardsInContainer[_cardsInBar[curSel]]));
                Destroy(curSel);
                _cardsInBar.Remove(curSel);
                //_cardsInContainer[_cardsInBar[curSel]].state = Card.State.Normal;
                //Destroy(curSel);
                //StartCoroutine(flyToContainer(curSel, _cardsInContainer[_cardsInBar[curSel]].transform.position, _cardsInContainer[_cardsInBar[curSel]]));

                //}
            }
          
        }
    }

    IEnumerator flyToBar(GameObject obj, Vector3 target,GameObject newCard) {
       

        Vector3 v=Vector3.zero;
        while (obj&&Vector3.SqrMagnitude(obj.transform.position-target)>0.00001f) {
            
            obj.transform.position = Vector3.SmoothDamp(obj.transform.position, target, ref v, 0.2f);
            yield return null;
        }
        Destroy(obj);
        newCard.SetActive(true);
    }

    IEnumerator flyToContainer(GameObject obj, Vector3 target, Card containerCard)
    {
        //obj.transform.SetParent(null);
        Vector3 v = Vector3.zero;
        Image objImg = obj.GetComponent<Image>();
        while (obj && Vector3.SqrMagnitude(obj.transform.position - target) > 0.00001f)
        {
            obj.transform.position = Vector3.SmoothDamp(obj.transform.position, target, ref v, 0.2f);
            Color c = objImg.color;
            c.a -= Time.deltaTime;
            objImg.color = c;
            yield return null;
        }
        Destroy(obj);
        containerCard.state = Card.State.Normal;
    }


    public void Submit() {

        //if (_cardsInBar.Count < maxCardNumber) return;
        _gameController.GetComponent<GameController>().AfterSelectCard();
        //StopAllCoroutines();
        foreach (GameObject o in _flyCardList)
        {
            Destroy(o);
        }
    }

}
