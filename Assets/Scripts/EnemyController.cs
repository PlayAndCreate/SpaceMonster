﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    Player player;

    int cnt = 0;

    //敵出現頻度と攻撃力の連想配列
    public Dictionary<string, int> Freq = new Dictionary<string, int>();
    public Dictionary<string, int> Attack = new Dictionary<string, int>();

    [System.Serializable]
    public class Prefabs {
        public GameObject enemy;
        public GameObject meteor;
    }
    public Prefabs prefabs;

    public void AddEnemyData(string name, int freq, int attack) {
        Freq.Add(name, freq);
        Attack.Add(name, attack);
    }

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        AddEnemyData("enemy1", 120, 20);

        foreach (KeyValuePair<string, int> pair in Freq) {
            Debug.Log(pair.Key + " freq is " + pair.Value);
        }
    }

    // Update is called once per frame
    void Update() {
        setEnemyPower();
        cnt++;
        EncountEnemy1(Freq["enemy1"], 120, 1, Random.Range(1.0f, 3.0f));
        EncountMeteor(60, 1);
    }

    /// <summary>
    /// 敵が出現するときにどのような位置に出現するか
    /// </summary>
    class EncountPosType {
        public GameObject Random(GameObject prefab, float posY_1, float posY_2, float posX = 7.0f) {
            var obj = Instantiate(prefab, new Vector2(posX, UnityEngine.Random.Range(posY_1, posY_2)), Quaternion.identity) as GameObject;
            return obj;
        }
    }
    EncountPosType encountPosType = new EncountPosType();



    void EncountEnemy1(int freq, int shotInterval, int atk, float speed = 1.0f) {
        var tempHP = 0;
        int random = 0;
        if (cnt % (freq + random) == 0) {
            random = Random.Range(-40, 40);
            GameObject enemy = encountPosType.Random(prefabs.enemy, -3.5f, 3.5f);
            int mNum = Random.Range(0, 1 + 3);
            bool isShot = true;
            if (mNum == 0) {
                isShot = false;
            } else if (mNum == 1) {
                isShot = true;
            }
            tempHP = 20;
            switch (player.power) {
                case 1:
                    tempHP = 50;
                    break;
                case 2:
                    tempHP = 70;
                    break;
                case 3:
                    tempHP = 100;
                    break;
            }
            int rand = Random.Range(0, 3 + 1);
            enemy.GetComponent<Enemy>().Create("Enemy1", speed, tempHP, atk, shotInterval, isShot);
        }
    }

    void EncountMeteor(int freq, float speed) {
        int random = 0;
        random = Random.Range(-20, 20);
        if (cnt % (freq + random) == 0) {
            GameObject enemy = encountPosType.Random(prefabs.meteor, -3.5f, 3.5f);
            enemy.GetComponent<Rigidbody2D>().velocity = -enemy.transform.right * 2.0f;
        }
    }

    void setEnemyPower(){
        switch (player.power) {
            case 1:
                Freq["enemy1"] = 70;
                break;
            case 2:
                Freq["enemy1"] = 65;
                break;
            case 3:
                Freq["enemy1"] = 60;
                break;
            case 4:
                Freq["enemy1"] = 55;
                break;
            case 5:
                Freq["enemy1"] = 50;
                break;
            case 6:
                Freq["enemy1"] = 45;
                break;
            case 7:
                Freq["enemy1"] = 40;
                break;
            case 8:
                Freq["enemy1"] = 35;
                break;
        }
    }
}
