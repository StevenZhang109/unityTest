using UnityEngine;
using System.Collections;

public class PlantHealth : MonoBehaviour {
    public int Hp;
    protected Animator _anmt;
    protected GameModel _model;
    protected PlantGrow _grow;
    protected void Awake()
    {
        _anmt = GetComponentInChildren<Animator>();
        _model = GameModel.GetInstance();
        _grow = GetComponent<PlantGrow>();
    }
    public virtual void Damage(int value)
    {
        Hp -= value;
        if (Hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        _model.PlantsInMap[_grow.Row, _grow.Col] = null;
        Destroy(gameObject);
    }
	
}
