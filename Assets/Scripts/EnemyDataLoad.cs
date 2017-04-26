using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class EnemyDataLoad : MonoBehaviour {
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
                Debug.LogFormat("{0},{1},{2},{3},{4}", key, enemyDataList[i].enemyDataDict[key].attack, enemyDataList[i].enemyDataDict[key].hp,
                                enemyDataList[i].enemyDataDict[key].freq, enemyDataList[i].enemyDataDict[key].score);
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
        string csvEnemyData = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
        string[] enemyData = csvEnemyData.Split('\n');
        var header = enemyData[0].Split(',');//説明部分
        int i = 0; //読み込んでいるcsvの行を入れる
        int index = 0;// 敵のデータを入れる配列の添字

        while (true) {
            if (enemyData[i] == null) {
                break;
            }
            if (enemyData[i] == "stage" + (index + 1)) {
                i++;
                while (true) {
                    Debug.Log("i = " + i + "index = " + index);
                    var enemyDataCol = enemyData[i].Split(',');
                    enemyDataList[index].enemyDataDict.Add(enemyDataCol[0], new EnemyData(int.Parse(enemyDataCol[1]), int.Parse(enemyDataCol[2]),
                                                                                          int.Parse(enemyDataCol[3]), int.Parse(enemyDataCol[4])));
                    i++;
                    /*if (i > 41) {
                        break;
                    }
                    */
                    if(enemyData[i] == null){
                        break;
                    }
                    if (enemyData[i] == "end") {
                            index++;
                            break;
                        }
                }
            }
            i++;
            /*
            if (i < 42) {
                i++;
            }
            */
        }
        Debug.Log("end of file,line is " + (i + 1));
        return true;
    }
}