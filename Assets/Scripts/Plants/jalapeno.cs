using UnityEngine;
using System.Collections;

public class jalapeno : MonoBehaviour {
    public AudioClip explodeSound;
    public GameObject effect;
    public float delayTime;
    bool _afterGrow = false;

    void AfterGrow() {
        _afterGrow = true;
        transform.Find("Plant").GetComponent<Animator>().Rebind();
        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(delayTime);
        GameObject newEffect = Instantiate(effect);
        newEffect.transform.position = new Vector3(-0.8f,transform.position.y+0.2f,0);
        newEffect.GetComponent<SpriteRenderer>().sortingOrder = transform.Find("Plant").GetComponent<SpriteRenderer>().sortingOrder + 1;
        Destroy(newEffect, 1.2f);

        GameModel model = GameModel.GetInstance();
        int row = GetComponent<PlantGrow>().Row;
        object[] zombies = model.zombieList[row].ToArray();
        foreach (GameObject zombie in zombies) {
            zombie.GetComponent<ZombieHealth>().BoomDie();

        }
        AudioManager.GetInstance().PlaySound(explodeSound);
        GetComponent<PlantHealth>().Die();
    }
    void Update() {
        if (!_afterGrow) return;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.2f, 1.2f, 1), 0.02f);
    }
}
