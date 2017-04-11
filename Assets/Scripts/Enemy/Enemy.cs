using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : EnemyBase {
    // Use this for initialization
    public GameObject Hpbar;
    Slider slider;
    public override void Start() {
        Init();
        pos = transform.position;
        hp = maxHP;
        start_cnt = 5;
        //start_cnt = Random.Range(7, 1 + 15);
        cnt = 0;
        //HPバー

        //slider = Hpbar.GetComponent<Slider>();
        //slider.maxValue = maxHP;

        //shotInterval = Random.Range(50,1+90);
    }
    public int debugShotInterval;

    void Shot() {
        debugShotInterval = shotInterval;
        if (start_cnt <= 0 && cnt % shotInterval == 0 && isShot) {
            switch (type) {
                case "Enemy1":
                case "Enemy2": {
                        var bullet1 = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 90)) as GameObject;
                        bullet1.GetComponent<EnemyBullet>().Create();
                        break;
                        /*
                        var bullet2 = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 180)) as GameObject;
                        bullet2.GetComponent<EnemyBullet>().Create();
                        var bullet3 = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 215)) as GameObject;
                        bullet3.GetComponent<EnemyBullet>().Create();
                        break;
*/
                    }
            }
        }
    }
    

    void Move() {
        pos.x -= speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {
        start_cnt--;
        cnt++;
        Move();
        if (hp <= 0) {
            player.ScoreUP();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Shot();
        transform.position = pos;
    }
    public void HitBox(string bulletType) {
        if (bulletType == "Normal") {
            hp -= 40;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        var targetTag = other.gameObject.tag;
        if (targetTag == "Player") {
            hp -= 50;
        }
    }
}
