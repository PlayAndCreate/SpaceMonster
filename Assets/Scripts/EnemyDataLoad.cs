using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class EnemyDataLoad : MonoBehaviour {
    const int kEnemyAll = 4;//敵のすべての数
    public bool isDebug = false;
    public bool isLoad = true;

    public GameObject enemyController;
    EnemyController enemyControll;

    public class EnemyData {
        public int hp, attack, score, freq;

        public EnemyData(int hp, int attack, int score, int freq) {
            this.hp = hp;
            this.attack = attack;
            this.score = score;
            this.freq = freq;
        }
    }

    public class StageEnemyData {
        public StageEnemyData() {
            enemyDataDict = new Dictionary<string, EnemyData>();
        }

        public Dictionary<string, EnemyData> enemyDataDict;
    }
    public List<StageEnemyData> enemyDataList;
    readonly int kStageCount = 5;

    Dictionary<int, string> EnemyName;

    // Use this for initialization
    void Start() {
        enemyControll = enemyController.GetComponent<EnemyController>();

        enemyDataList = new List<StageEnemyData>();
        for (int i = 0; i < kStageCount; i++) {
            enemyDataList.Add(new StageEnemyData());
        }
        if (isLoad) {
            Load("enemyData");
        } else {
            Save(@"Assets/Resources/enemyData.csv");
        }
        DontDestroyOnLoad(gameObject);

        Debug.LogWarning("enemy list");
        for (int i = 0; i < enemyDataList.Count; i++) {
            Debug.LogWarning("Stage" + (i + 1));
            Debug.Log("name,attack,hp,freq,score");
            var KeyList = new List<string>(enemyDataList[i].enemyDataDict.Keys);
            foreach (var key in KeyList) {
            }
        }
    }

    bool Save(string filePath) {
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        Encoding shiftJis = Encoding.GetEncoding("SHIFT_JIS");
        StreamWriter sw = new StreamWriter(fs, shiftJis);
        sw.Close();
        return true;
    }
    bool Load(string filePath) {//上のコードがわかりにくいので新しく関数を書いている2
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
        var EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        int i = 0, index = 0;//ヘッダー、説明部分を飛ばす 敵のデータを入れる配列の添字

        /*foreach (var str in EnemyList) {
            Debug.LogWarning(str);
        }*/

        while (true) {
            if (EnemyList[i] == "") {
                break;
            }
            if (EnemyList[i] == "stage" + (index + 1)) {
                i++;
                while (true) {
                    var EnemyDataCol = EnemyList[i].Split(',');
                                                                                          int.Parse(EnemyDataCol[3]), int.Parse(EnemyDataCol[4])));
                    i++;
                    if (EnemyList[i] == "end") {
                        index++;
                        break;
                    }
                }
            }
        }
        return true;
    }
}