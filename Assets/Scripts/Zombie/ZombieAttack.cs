using UnityEngine;
using System.Collections;

public class ZombieAttack : MonoBehaviour
{
    public AudioClip AttackSound;

    public int atk = 30;
    public float cd = 1;
    public float Range;
    protected Animator _anmt;
    protected GameModel _model;
    protected ZombieMove _move;
    protected AudioSource _audioSound;

    protected GameObject _target;
    void Awake()
    {
        _anmt = GetComponentInChildren<Animator>();
        _model = GameModel.GetInstance();
        _move = GetComponent<ZombieMove>();
        //_audioSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (_target == null)
        {
            _target = SearchPlant();
        }
        if (_target && _move.enabled)
        {
            _move.enabled = false;
            _anmt.SetBool("isAttacking", true);
            _audioSound = AudioManager.GetInstance().PlaySound(AttackSound, true);
            Invoke("DoAttack", cd);
        }
        else if (!_target && !_move.enabled)
        {
            _move.enabled = true;
            _anmt.SetBool("isAttacking", false);
            AudioManager.GetInstance().StopSound(_audioSound);
            CancelInvoke("DoAttack");
        }
    }

    public void DoAttack()
    {
        if (_target)
        {
            _target.GetComponent<PlantHealth>().Damage(atk);
        }
        Invoke("DoAttack", cd);
    }

    public void OnDisable()
    {
        AudioManager.GetInstance().StopSound(_audioSound);
    }

    public GameObject SearchPlant()
    {
        GameObject target = null;
        float minDis = 100000;
        for (int col = 0; col < StageMap.MaxCol; col++)
        {
            GameObject plant = _model.PlantsInMap[_move.Row, col];
            if (plant && plant.GetComponent<PlantHealth>())
            {
                float dis = transform.position.x - plant.transform.position.x;
                if (0 <= dis && dis <= Range)
                {
                    if (minDis > dis)
                    {
                        minDis = dis;
                        target = plant;
                    }
                }
            }
        }
        return target;
    }

    //public void OnDestroy()
    //{
       
    //}
}
