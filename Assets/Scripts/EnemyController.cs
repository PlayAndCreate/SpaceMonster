using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    Player player;

    int cnt=0;

    //敵出現頻度と攻撃力の連想配列
    public Dictionary<string, int> Freq = new Dictionary<string, int>();
    public Dictionary<string, int> Attack = new Dictionary<string, int>();

    [System.Serializable]
    public class Prefabs {
        public GameObject enemy;
    }
    public Prefabs prefabs;

    public void AddEnemyData(string name, int freq, int attack) {
        Freq.Add(name, freq);
        Attack.Add(name, attack);
    }

    // Use this for initialization
    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        AddEnemyData("enemy1", 80, 20);

        foreach (KeyValuePair<string, int> pair in Freq) {
            Debug.Log(pair.Key + " freq is " + pair.Value);
        }
    }

    // Update is called once per frame
    void Update() {
        cnt++;
        EncountEnemy1(Freq["enemy1"],1,1);
    }

    /// <summary>
    /// 敵が出現するときにどのような位置に出現するか
    /// </summary>
    class EncountPosType {
        public GameObject Random(GameObject prefab, float posY_1, float posY_2, float posX = 7.0f) {
            var obj = Instantiate(prefab, new Vector2(posX,UnityEngine.Random.Range(posY_1, posY_2)), Quaternion.identity) as GameObject;
            return obj;
        }
    }
    EncountPosType encountPosType = new EncountPosType();



    void EncountEnemy1(int freq, int shotInterval, int atk, float speed = 1.0f) {
        var tempHP = 0;
        int random = 0;
        if (cnt % (freq + random) == 0) {
            random = Random.Range(-20, 20);
            var enemy = encountPosType.Random(prefabs.enemy, -3.5f, 3.5f);
            var mNum = Random.Range(0, 1 + 3);
            bool isShot = true;
            if (mNum == 0) {
                isShot = false;
            } else if (mNum == 1) {
                isShot = true;
            }
            switch (player.power) {
                case 1:
                    tempHP = 50;
                    break;
                case 2:
                    tempHP = 150;
                    break;
                case 3:
                    tempHP = 250;
                    break;
            }
            var rand = Random.Range(0, 3 + 1);
            enemy.GetComponent<Enemy>().Create("Enemy1",speed,tempHP,atk,shotInterval,isShot);
        }
    }
}

