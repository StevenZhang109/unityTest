using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float Speed;
    public float Range;
    public GameObject Effect;
    public int AtkDamage;
    protected int _row;
    protected GameModel _model;
    protected SearchZombie _search;
    protected GameObject _target;
    public int Row
    {
        get { return _row; }
        set { _row = value;
            if (_row >= 0 && _row < StageMap.MaxRow)
            {
                _model.bulletList[_row].Add(gameObject);
            }

        }
       
    }
     void Awake()
    {
        _search = GetComponent<SearchZombie>();
        _model = GameModel.GetInstance();
        _row = -1;
    }
     void Update()
    {

        transform.Translate(Speed*Time.deltaTime,0,0);

        if (_row < 0 && StageMap.MaxRow <= _row)
        {
            _target = null;
        }
        _target = _search.SearchClosestZombie(_row,0,Range);
        if (_target)
        {
            _target.SendMessage("Blink");
            HitEffect();
            _target.GetComponent<ZombieHealth>().Damage(AtkDamage);
        }
    }

    protected virtual void HitEffect()
    {
        if (Effect)
        {
            GameObject newEffect = Instantiate(Effect);
            newEffect.transform.position = transform.position;
            Destroy(newEffect,0.2f);
        }
        DoDestroy();
    }

    public void DoDestroy()
    {
        if (_row >= 0 && _row < StageMap.MaxRow)
        {
            _model.bulletList[Row].Remove(gameObject);
        }
        Destroy(gameObject);
    }
}
