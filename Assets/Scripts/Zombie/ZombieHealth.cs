using UnityEngine;
using System.Collections;
using System;

public class ZombieHealth : MonoBehaviour {
    public AudioClip DamageSound;

    public GameObject Head;
    public int lostHeadHp = 40;
    public Vector3 headOffset;
    public int Hp;
    protected bool _hasHead=true;
    protected Animator _anim;
    void Awake()
    {
        _anim = transform.Find("zombie").GetComponent<Animator>();
        

    }

    public virtual void Damage(int value)
    {
        if (Hp <= 0) return;
        AudioManager.GetInstance().PlaySound(DamageSound);
        Hp -= value;
        _anim.SetInteger("hp", Hp);
        if (Hp <= lostHeadHp&&_hasHead)
        {
            LostHead();
            SendMessage("WalkResume");
        }
        if (Hp <= 0)
        {
            Die();
        }
    }
    protected virtual void LostHead()
    {
        GameObject newHead = Instantiate(Head);
        newHead.transform.position = transform.position + headOffset;
        Destroy(newHead, 3f);
        _hasHead = false;
    }

    protected void Die()
    {
        ZombieMove move = GetComponent<ZombieMove>();
        ZombieAttack atk = GetComponent<ZombieAttack>();
        GameModel.GetInstance().zombieList[move.Row].Remove(gameObject);
        move.enabled = false;
        atk.enabled = false;
        Destroy(gameObject,3.0f);
    }

    public void BoomDie()
    {
        if (Hp <= 0) return;
        _anim.SetTrigger("BoomDie");
        Die();
    }
}
