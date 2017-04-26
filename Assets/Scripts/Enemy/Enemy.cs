using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : EnemyBase {
    // Use this for initialization
    public GameObject Hpbar;
    Slider slider;
    float shotSpeed;
    Vector2 playerPos;
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
        shotSpeed = speed + 1.0f;

    }
    public int debugShotInterval;

    void Shot() {
        debugShotInterval = shotInterval;
        if (start_cnt <= 0 && cnt % shotInterval == 0 && isShot) {
            switch (type) {
                case "Enemy1":
                case "Enemy2": {
                        var angle = Mathf.Atan2(transform.position.y - playerPos.y,
                                                transform.position.x - playerPos.x);
                        angle = angle * 180 / Mathf.PI;
                        //Debug.Log("angle = "+angle);
                        var bullet1 = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                        bullet1.GetComponent<EnemyBullet>().Create();
                        bullet1.GetComponent<Rigidbody2D>().velocity = -bullet1.transform.right.normalized * shotSpeed;
                        break;
                    }
            }
        }
    }

    void Move() {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Player>().transform.position;
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

    public void OnTriggerEnter2D(Collider2D other) {
        GameObject obj = other.gameObject;
        switch (obj.tag) {
            case "Player":
                hp -= 50;
                break;
            case "PlayerBullet":
                hp -= 10;
                GameObject explode = Instantiate(explosion, obj.transform.position, Quaternion.identity);
                Destroy(obj);
                explode.GetComponent<Transform>().localScale=new Vector2(2.0f * 0.7f, 2.0f * 0.7f);
                break;
        }
    }
}
