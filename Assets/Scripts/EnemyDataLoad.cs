using UnityEngine;
using System.IO;
using System.Text;

public class EnemyDataLoad : MonoBehaviour {
    const int kEnemyAll = 4;//敵のすべての数
    public bool isDebug = false;
    public bool isLoad = true;

    public GameObject enemyController;
    EnemyController enemyControll;

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

        for (int i = 0; i < 43; i++) {
            var EnemyDataCol = EnemyList[i + 1].Split(',');//ヘッダーを飛ばす
            Debug.LogWarningFormat(i+":{0}",EnemyDataCol);
            string name = EnemyDataCol[0];
            //enemyControll.Freq[name] = int.Parse(EnemyDataCol[1]);
            //enemyControll.Attack[name] = int.Parse(EnemyDataCol[2]);
        }
        return true;
    }
}