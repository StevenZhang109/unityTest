using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider2D))]
public class Sun : MonoBehaviour
{
    public AudioClip sound;
    public Vector3 disappearPos;
    public int value = 25;
    public float absorbTime = 0.5f;
    public float disappearTime;

    GameModel _model;
    Vector3 speed;
    Vector3 scaleSpeed;

    void Awake()
    {
        _model = GameModel.GetInstance();
        scaleSpeed = Vector3.one / absorbTime;

        Destroy(gameObject, disappearTime);
        enabled = false;
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime);
        transform.localScale = transform.localScale - scaleSpeed * Time.deltaTime;
    }
    void OnMouseDown()
    {
        _model.sun += value;
        MoveBy move = GetComponent<MoveBy>();
        if (move)
        {
            move.enabled = false;
        }
        enabled = true;
        speed = (disappearPos - transform.position) / absorbTime;
        AudioManager.GetInstance().PlaySound(sound);
        Destroy(gameObject, absorbTime);
    }
    // Use this for initialization
    void Start()
    {

    }

   
}
