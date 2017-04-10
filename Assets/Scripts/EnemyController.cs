using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Player player;
    //敵出現頻度と攻撃力の連想配列
    public Dictionary<string, int> Freq = new Dictionary<string, int>();
    public Dictionary<string, int> Attack = new Dictionary<string, int>();

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

    }


    void EncountEnemy1(){


    }
}

