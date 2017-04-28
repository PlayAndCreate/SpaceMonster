﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public GameObject bullet;
    float kSpeed = 3.0f;
    float shotFreq,shotCnt;
    float shotSpeed = 6.0f;

    GameObject explosion;
    public static int score;
    public int HP, maxHP;
    public int power, atk;

    public Text powerText;

	// Use this for initialization
	void Start () {
        HP = 500;
        shotFreq = 8.0f;
        power = 1;
        explosion = Resources.Load("Prefabs/explosion_32") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        powerText.text = "power : "+power;
        PlayerMove();
        shotCnt++;
        Shot();
    }

    void Shot() {
        if (Input.GetKey(KeyCode.Space) && (int)(shotCnt % shotFreq) == 0) {
            if (power >= 8) {
                BulletCreate(0, 0, shotSpeed, 180);
            }
            if (power >= 7) {
                BulletCreate(0, 0, shotSpeed, -135);
            }
            if (power >= 6) {
                BulletCreate(0, 0, shotSpeed, 135);
            }
            if (power >= 5) {
                BulletCreate(0, 0, shotSpeed, -90);
            }
            if (power >= 4) {
                BulletCreate(0, 0, shotSpeed, 90);
            }
            if (power >= 3) {
                BulletCreate(0, 0, shotSpeed, -45);
            }
            if (power >= 2) {
                BulletCreate(0, 0, shotSpeed, 45);
            }
            //powerが１以下でも弾出せるようにしました
            //if (power >= 0) {
                BulletCreate(0, 0, shotSpeed, 0);
            //}
        }
    }

    void BulletCreate(float dx, float dy, float speed, float angleZ) {
        GameObject obj = Instantiate(bullet, new Vector2(transform.position.x + dx, transform.position.y + dy), Quaternion.Euler(0,0,angleZ));
        obj.GetComponent<Rigidbody2D>().velocity = obj.transform.right.normalized * speed;
    }

    void PlayerMove() {
        float x = Input.GetAxisRaw("Horizontal") * 1.7f;
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction) {
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(3, 1));
        // プレイヤーの座標を取得
        Vector2 pos = transform.position;
        // 移動量を加える
        pos += direction * kSpeed * Time.deltaTime;
        // プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        // 制限をかけた値をプレイヤーの位置とする
        transform.position = pos;
    }

    public void ScoreUP() {
        score += 10;
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag) {
            case "Enemy":
                HP -= 10;
                break;
            case "EnemyBullet":
                GameObject explode = Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
                explode.GetComponent<Transform>().localScale = new Vector2(2.0f * 0.7f, 2.0f * 0.7f);
                power -= 1;

                Destroy(other.gameObject);
                break;
                case "Meteor":
                power += 1;
                Destroy(other.gameObject);
                break;
        }
    }
}
