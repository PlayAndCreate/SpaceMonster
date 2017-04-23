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
        public void Add(string name, int hp, int attack, int score, int freq) {
            enemyDataDict.Add(name, new EnemyData(hp, attack, score, freq));
            Debug.LogWarningFormat("name is {0},hp is {1},attack is {2}, freq is {3}", name, hp, attack, freq);
        }
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

        var KeyList = new List<string>(enemyDataList[0].enemyDataDict.Keys);
        Debug.LogWarning("enemy list");
        foreach (var key in KeyList) {
            Debug.LogFormat("" + enemyDataList[0].enemyDataDict[key].attack);
        }

    }

    bool Save(string filePath) {
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        Encoding shiftJis = Encoding.GetEncoding("SHIFT_JIS");
        StreamWriter sw = new StreamWriter(fs, shiftJis);
        sw.Close();
        return true;
    }

    bool Load1(string filePath) {
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
        string[] EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        int i = 2;//ヘッダー、説明部分を飛ばす
        int index = 0;//敵のデータを入れる配列の添字
        while (true) {
            if (i > EnemyList.Length - 1) {
                Debug.Log("EOF,number is " + (EnemyList.Length - 1));
                break;
            }
            /*
            Debug.Log("0:index = " + index+" , EnemyList[i] = " + EnemyList[i]);
            //Debug.Log(" , EnemyList[i] = " + EnemyList[i]);

            //Debug.Log("70: index = "+index+" i = "+i);
            Debug.LogFormat("94: EnemyList[{0}] == {1}",i,EnemyList[i]);
            Debug.LogFormat("95: is {0}", Equals(EnemyList[i],"stage" + (index + 1)));
            */
            if (EnemyList[i] == "stage" + (index + 1)) {
                Debug.Log("index = " + index);
                var EnemyDataCol = EnemyList[i].Split(',');
                while (true) {
                    enemyDataList[index].Add(EnemyDataCol[0], int.Parse(EnemyDataCol[1]), int.Parse(EnemyDataCol[2]),
                                               int.Parse(EnemyDataCol[3]), int.Parse(EnemyDataCol[4]));
                }
                i += 1;
            }
            if (EnemyList[i] == "end") {
                Debug.Log("end");
                i += 1;
                index += 1;
            }
            //Debug.LogWarningFormat((i + 1) + ": " + EnemyList[i]);
            //enemyControll.Freq[name] = int.Parse(EnemyDataCol[1]);
            //enemyControll.Attack[name] = int.Parse(EnemyDataCol[2]);
            i++;
        }
        return true;
    }
    /// <summary>
    /// Load the specified filePath.
    /// </summary>
    /// <returns>The load.</returns>
    /// <param name="filePath">File path.</param>
    bool Load2(string filePath) {//上のコードがわかりにくいので新しく関数を書いている
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
        string[] EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        int i = 0, index = 0;//ヘッダー、説明部分を飛ばす 敵のデータを入れる配列の添字
        while (true) {
            Debug.Log("i = " + i);
            if (EnemyList[i] == "") {
                Debug.Log("EOF,line is " + (i + 1));
                break;
            }
            if (EnemyList[i] == "stage" + (index + 1)) {
                Debug.Log("line is " + i + " , string = " + EnemyList[i]);
                i++;
            }
            if (EnemyList[i] == "end") {
                Debug.Log("end");
                index++;
                i++;
            }
            i++;
        }
        return true;
    }

    bool Load(string filePath) {//上のコードがわかりにくいので新しく関数を書いている2
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
        var EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        int i = 0, index = 0;//ヘッダー、説明部分を飛ばす 敵のデータを入れる配列の添字

        foreach (var str in EnemyList) {
            Debug.LogWarning(str);
        }

        while (true) {
            if (EnemyList[i] == "") {
                break;
            }
            Debug.LogFormat("first EnemyList[{0}] = {1} , index = {2}", i, EnemyList[i], index);
            if (EnemyList[i] == "stage" + (index + 1)) {
                Debug.Log("line is " + (i + 1) + " , string = " + EnemyList[i]);
                i++;
                Debug.Log("stage: next string = " + EnemyList[i]);
            }
            if (EnemyList[i] == "end") {
                index++;
                i++;
                Debug.Log("end: next string = " + EnemyList[i]);
            }
            i++;
            Debug.LogFormat("second EnemyList[{0}] = {1} , index = {2}",i,EnemyList[i], index);
        }
        Debug.Log("end of file,line is " + i);
        return true;
    }
}