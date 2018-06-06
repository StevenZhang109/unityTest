using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HandleForPlants : MonoBehaviour {
    public AudioClip PlantGrow;
    public AudioClip SeedLift;
    public AudioClip SeedCancel;
    GameObject _transparentPlant;//半透明植物 种植位置提示
    GameObject _underMousePlant;//鼠标下的植物 按下就种了
    PlantGrow _underMousePlantGrow;
    GameModel _model;
    Card _curCard;//当前选中的卡片
    int _row = -1;
    int _col =-1;
    void Start()
    {
        _model = GameModel.GetInstance();
    }
    void Update() 
    {
        handleMouseMotion();
        handleMouseDown();
    }
   
    void handleMouseMotion()
    {
        if (_underMousePlant)
        {
            Vector3 pos = Utility.GetMouseWorldPos();
            Vector3 plantPos = pos/*+(Vector3.up*0.3f)*/;
            plantPos.y -= 0.3f;
            _underMousePlant.transform.position = plantPos;
            if (StageMap.IsPointInMap(plantPos))
            {
                StageMap.GetRowCol(plantPos, out _row, out _col);

                if (_underMousePlantGrow.CanGrowInMap(_row, _col))
                {
                    _transparentPlant.transform.position = StageMap.PlantPosByRowCol(_row, _col);
                }
                else
                {
                    _col = _row = -1;
                    _transparentPlant.transform.position = new Vector3(1000, 1000, 0);
                }
            }
            else
            {
                _col = _row = -1;
                _transparentPlant.transform.position = new Vector3(1000, 1000, 0);
            }
        }

    }
    void handleMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            GameObject curSel = EventSystem.current.currentSelectedGameObject;

            if (curSel && curSel.tag == "Card")
            {
                cancelSeletedCard();
                Card card = curSel.GetComponent<Card>();
                if (card.CardCanSelect())//有点无语的写法 通知别人调自己的方法... 虽然有些参数在别人那里 
                    SetSelectedCard(card);
                AudioManager.GetInstance().PlaySound(SeedLift);
            }
            //Collider2D collider = Physics2D.OverlapPoint(Utility.GetMouseWorldPos());
            //if (collider != null)//点击到碰撞盒
            //{
            //    cancelSeletedCard();
            //    if (collider.gameObject.tag == Utility.Card)
            //    {
            //        collider.gameObject.SendMessage("OnCardSelect");//有点无语的写法 通知别人调自己的方法... 虽然有些参数在别人那里 
            //        AudioManager.GetInstance().PlaySound(SeedLift);
            //    }
            //}
            else if (_underMousePlant != null)//有选择的植物（鼠标下有植物）
            {
                if (_row != -1)//row col合法
                {
                    _underMousePlant.transform.position = _transparentPlant.transform.position;
                
                    //if (_underMousePlantGrow.CanGrowInMap(_row, _col)) return;
                    _underMousePlant.GetComponent<PlantGrow>().Grow(_row, _col);
                    AudioManager.GetInstance().PlaySound(PlantGrow);

                    _underMousePlant = null;
                    Destroy(_transparentPlant);

                    _transparentPlant = null;
                    _curCard.Growed();
                    _curCard= null;
                }
                //else
                //{
                //    cancelSeletedCard();
                //}
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            cancelSeletedCard();
        }
    }
    void cancelSeletedCard()
    {

        if (_curCard) {
            Destroy(_transparentPlant);
            Destroy(_underMousePlant);
            _transparentPlant = null;
            _underMousePlant = null;
            //_curCard.state = Card.State.Normal;
            _curCard = null;
        }
        AudioManager.GetInstance().PlaySound(SeedCancel);
       
    }
    public void SetSelectedCard(Card card)
    {
        //card.state = Card.State.Cd;
        _curCard = card;
        _transparentPlant = Instantiate(card.Plant);
        _transparentPlant.transform.position = new Vector3(1000, 1000, 0);
        _transparentPlant.GetComponent<PlantSpriteDisplay>().SetAlpha(0.5f);
        _underMousePlant = Instantiate(card.Plant);
        _underMousePlant.GetComponent<PlantSpriteDisplay>().SetOrder(10000);
        _underMousePlant.transform.position=new Vector3(1000, 1000, 0);
        _underMousePlantGrow = _underMousePlant.GetComponent<PlantGrow>();
    }
}
