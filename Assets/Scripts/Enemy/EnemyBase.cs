﻿using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {
    public GameObject BulletPrefab;
    protected Player player;
    protected GameObject explosion;

    //public GameObject textHpPrefab;
    protected GameObject itemPrefab;

    public string type;
    public float speed;
    public int hp, maxHP;
    public int attack;
    public bool isShot;
    public int start_cnt, cnt;
    public int shotInterval;
    public Vector2 pos;

    public virtual void Create(string type,float speed, int maxHP, int attack, int shotInterval = 70, bool isShot = true) {
        this.type = type;
        this.speed = speed;
        this.attack = attack;
        this.maxHP = maxHP;
        this.isShot = isShot;

        this.shotInterval = shotInterval;
    }
    //アイテムを降らす関数
    /*
    protected void ItemMake(Vector3 pos) {
        var num = Random.Range(0, 1 + 2);
        if (num == 0) {
            Instantiate(itemPrefab, pos, Quaternion.identity);
        }
    }
    */

    // Use this for initialization
    public virtual void Start() {

    }

    protected void Init() {
        player= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        explosion = Resources.Load("Prefabs/explosion_32") as GameObject;
        itemPrefab = Resources.Load("Prefabs/item") as GameObject;
    }

    // Update is called once per frame
    //public virtual void Update () {}
}
