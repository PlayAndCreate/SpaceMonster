using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject bullet;
    float kSpeed = 3.0f;
    float shotFreq,shotCnt;

    public static int score;
    public int HP, maxHP;
    public int power, atk;

	// Use this for initialization
	void Start () {
        shotFreq = 10.0f;
        power = 1;
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMove();
        shotCnt++;
        Shot();
    }

    void Shot() {
        if (Input.GetKey(KeyCode.Space) && shotCnt % shotFreq == 0) {
            BulletCreate(0, 0, 6.0f);
        }
    }

    void BulletCreate(float dx, float dy,float speed) {
        GameObject obj = Instantiate(bullet, new Vector2(transform.position.x + dx, transform.position.y + dy), Quaternion.identity);
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
}
