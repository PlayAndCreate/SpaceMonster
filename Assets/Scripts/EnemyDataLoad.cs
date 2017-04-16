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


    List<string> EnemyData;
    Dictionary<string, int> enemyName;

    // Use this for initialization
    void Start() {
        enemyControll = enemyController.GetComponent<EnemyController>();
        if (isLoad) {
            Load("enemyData");
        } else {
            Save(@"Assets/Resources/enemyData.csv");
        }
        DontDestroyOnLoad(gameObject);
    }

    bool Save(string filePath) {
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        Encoding shiftJis = Encoding.GetEncoding("SHIFT_JIS");
        StreamWriter sw = new StreamWriter(fs, shiftJis);
        sw.Close();
        return true;
    }

    bool Load(string filePath) {
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;

        string[] EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        int i = 3;//ヘッダー、説明部分を飛ばす
        int index = 0;//敵のデータを入れる配列の添字
        while(true){
            if(EnemyList[i] == ""){
                Debug.Log(EnemyList[i] + " is null");
                break;
            }
            if (EnemyList[i] == "stage" + (i-1)){
                i += 1;
                var EnemyDataCol = EnemyList[i].Split(',');

            }
            if(EnemyList[i] == "end"){
                i += 1;
                index += 1;
            }

            Debug.LogWarningFormat("" + EnemyList[i]);
            //enemyControll.Freq[name] = int.Parse(EnemyDataCol[1]);
            //enemyControll.Attack[name] = int.Parse(EnemyDataCol[2]);
            i++;
        }
        return true;
    }
}