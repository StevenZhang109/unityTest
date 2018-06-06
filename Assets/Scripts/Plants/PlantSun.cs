using UnityEngine;
using System.Collections;

public class PlantSun : MonoBehaviour {
    public GameObject sun;
    public int sunCount;
    public float produceCd;
    SpriteRenderer _shining;
    SpriteRenderer _flowerRenderer;
    float cdTime;
    void Awake() {
        cdTime = produceCd / 2;
        enabled = false;
        _flowerRenderer = transform.Find("Plant").GetComponent<SpriteRenderer>();
        _shining= transform.Find("shine").GetComponent<SpriteRenderer>();
    }

    void AfterGrow() {
        enabled = true;
        _shining.sortingOrder = _flowerRenderer.sortingOrder+1;
    }

    void ProduceSun() {
        for (int i=0;i<sunCount;i++) {
            GameObject newSun = Instantiate(sun);
            newSun.GetComponent<SpriteRenderer>().sortingOrder = 10000;
            newSun.transform.position = transform.position;

            
            float randomDis = Random.Range(-StageMap.GridWidth, StageMap.GridWidth);
            float offsetX = Mathf.Sign(randomDis) * 0.3f + randomDis;
            Vector3 offset = new Vector3(offsetX,0,0);
            JumpBy jump = newSun.AddComponent<JumpBy>();
            jump.offset = offset;
            jump.height = Random.Range(0.3f, 0.6f);
            jump.time = Random.Range(0.4f, 0.6f);
            jump.Begin();
        }
    }
    IEnumerator shining() {
       
      
        float f = 0;
        while (f<=Mathf.PI) {
            f += Mathf.PI * Time.deltaTime*0.5f;
            float a = Mathf.Sin(f);
            Color c = _shining.color;
            c.a = a;
            _shining.color = c;
            yield return null;
        }
    }
	void Update () {
        bool shineFlag = true;
        if (cdTime >=1f)
        {
            cdTime -= Time.deltaTime;
        }
        else if (cdTime>=0) {
            cdTime -= Time.deltaTime;
            if (shineFlag)
            {
                StartCoroutine(shining());
                shineFlag = false;
            }
        }
        else {
            cdTime = produceCd;
            ProduceSun();
            shineFlag = true;
        }
	}
}
