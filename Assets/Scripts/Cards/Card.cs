using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    public enum State {
        Normal,
        NoSun,
        Cd,
        Disable //for 卡片选取界面
    }
  

    public Sprite enableSprite;
    public Sprite disableSprite;
    public int price;
    public float cd;
    float _cdTime;
 
    public GameObject Plant;
    bool _isGrowed;
    [HideInInspector]
    public State state = State.Normal;
    GameModel _model;
   
    Image _btnImg;
    Image _cdImg;
    void Awake() {
        _btnImg = GetComponent<Button>().image;
        _cdImg = transform.FindChild("cd").GetComponent<Image>();
        _model = GameModel.GetInstance();

      
        //renderer = GetComponent<SpriteRenderer>();
        //renderer.sprite = enableSprite;
        //Plant.GetComponent<PlantGrow>().price = price;
    }
   
    public bool CardCanSelect()
    {
        if (state == State.Normal)
        {
            return true;
            //_plantHandler.SetSelectedCard(this);
            //}else {
            //    renderer.sprite = disableSprite;
            //}
        }
        return false;
    }

    void UpdateUI() {
      
        if (state == State.Normal)
        {
            //renderer.sprite = enableSprite;
            _btnImg.sprite = enableSprite;
        }
        else
        {
            //renderer.sprite = disableSprite;
            _btnImg.sprite = disableSprite;
        }
    }
    void checkSun() {
        if (_model.sun < price && state != State.Cd)
            state = State.NoSun;
        else {
            if (state == State.Cd)
            {
                return;
            }
            else
            {
                state = State.Normal;
                _isGrowed = false;
            }
        }
    }
    public void Growed() {
        _isGrowed = true;
        state = State.Cd;
        _cdTime = cd;
        _cdImg.fillAmount = 1;
        _model.sun -= price;
        //UpdateUI();
    }
    void Update() {
        UpdateUI();
        if (state == State.Disable) return;
        checkSun();
     
        if(state==State.Cd&&_isGrowed)
        {
            _cdTime -= Time.deltaTime;
            _cdImg.fillAmount = _cdTime / cd;
            if (_cdTime <= 0)
            { state = State.Normal;
                _isGrowed = false;
            }
        }
    }
    //public bool CardEnable{
    //    get {
    //        if (_btnImg.sprite == enableSprite)
    //            return true;
    //        else
    //            return false;
    //    }
    //}
    public void SetSpriteEnable(bool enable) {
        if (enable)
        {
            _btnImg.sprite = enableSprite;
        }
        else
        {
            _btnImg.sprite = disableSprite;
        }
    }
}
