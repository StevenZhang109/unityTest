using UnityEngine;
using System.Collections;

public class PlantShoot : MonoBehaviour {
    public GameObject[] Bullets;
    public Vector3 BulletOffset;
    public float Interval;
    public float Cd;
    float _cdTimer ;
    Transform _plant;

    PlantGrow _grow;
    public float Range;
    SearchZombie _search;
    void Awake()
    {
        _grow = GetComponent<PlantGrow>();
        _plant = transform.FindChild("Plant");
        _search = GetComponent<SearchZombie>();
        enabled = false;
        _cdTimer = Cd/2;
    }
    void AfterGrow()
    {
        enabled = true;
    }
    void Update()
    {
        if (_cdTimer > 0&&enabled)
            _cdTimer -= Time.deltaTime;
        else
        
        {
            bool hasZombie = _search.isZombieInRange(_grow.Row,0,Range);
           
            if (!hasZombie) return;
            StartCoroutine("shoot");
           
            _cdTimer = Cd;
        }
    }
    IEnumerator shoot()
    {
        Vector3 pos = transform.position + BulletOffset;
        foreach (var bullet in Bullets) {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = pos;
            newBullet.GetComponent<Bullet>().Row = _grow.Row;
            newBullet.GetComponent<SpriteRenderer>().sortingOrder = 1000 * (_grow.Row+1) + 3;
            StartCoroutine("shootScale");
            yield return new WaitForSeconds(Interval);
        }
    }
    IEnumerator shootScale()
    {
        Vector3 scale = transform.localScale;
        float timer = 0;
        while (timer < 0.15)
        {
            timer += Time.deltaTime;
            scale += 0.02f*Vector3.one;
            _plant.localScale = scale;
            yield return null;
        }
        timer = 0;
        while (timer < 0.15)
        {
            timer += Time.deltaTime;
            scale -= 0.02f * Vector3.one;
            _plant.localScale = scale;
            yield return null;
        }
    }

}
