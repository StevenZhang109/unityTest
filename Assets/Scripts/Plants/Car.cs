using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
    public AudioClip sound;
    public int row;
    public float speed;
    private SearchZombie search;
    private bool start = false;
    private float range = 0.2f;

    void Awake()
    {
        search = GetComponent<SearchZombie>();
    }

    void Update()
    {
        if (start)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);
            GameObject zombie = search.SearchClosestZombie(row, 0, range);
            if (zombie) { 
            zombie.GetComponent<ZombieHealth>().Damage(10000);
                StartCoroutine(Stop());
            }
            if (transform.position.x > (StageMap.RightGrid+ 3.5f))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (search.isZombieInRange(row, 0, range))
            {
                start = true;
                AudioManager.GetInstance().PlaySound(sound);
            }
        }
    }

    IEnumerator Stop() {
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        speed = 6;
    }

}
