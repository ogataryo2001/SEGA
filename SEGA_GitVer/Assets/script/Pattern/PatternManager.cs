using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// パターンの種類
/// </summary>
public enum NumberPattern
{
    p1,
    p2,
    p3,
    p4,
    p5,
    p6,
    p7,
    p8,
    p9,
};

/// <summary>
/// パターンの難易度
/// </summary>
public enum DifficultyPattern
{
    Easy,
    Nomal,
    Hard,
};

class PatternManager : MonoBehaviour
{
    [SerializeField] GameObject EasyPattern1;
    [SerializeField] GameObject EasyPattern2;
    [SerializeField] GameObject EasyPattern3;
    [SerializeField] GameObject EasyPattern4;
    [SerializeField] GameObject EasyPattern5;
    [SerializeField] GameObject NomalPattern1;
    [SerializeField] GameObject NomalPattern2;
    [SerializeField] GameObject NomalPattern3;
    [SerializeField] GameObject HardPattern1;

    /// <summary>
    /// 難易度変更に使用
    /// </summary>
    public static DifficultyPattern m_DifficultyPatten;

    /// <summary>
    /// 簡単なパターンをまとめたもの
    /// </summary>
    List<GameObject> EasyPattern;

    /// <summary>
    /// 普通くらいの難易度のパターン
    /// </summary>
    List<GameObject> NomalPattern;

    /// <summary>
    /// 激ムズパターン（増やそうか
    /// </summary>
    List<GameObject> HardPattern;

    /// <summary>
    /// 現在アクティブ状態のパターン
    /// </summary>
    private GameObject currentPattern;

    /// <summary>
    /// 各パターンの保存場所
    /// </summary>
    private List<string> correctRoute;

    /// <summary>
    /// お邪魔パターン
    /// </summary>
    private GameObject obstaclePattern;

    /// <summary>
    /// 適当な数（０～８）
    /// </summary>
    public static int randomValue = 0;

    /// <summary>
    /// 現在のパターンの範囲の最小値
    /// </summary>
    private float lowLimitRange;

    /// <summary>
    /// 現在のパターンの範囲の最大値
    /// </summary>
    private float highLimitRange;

    /// <summary>
    /// 各パターンの数の下限値
    /// </summary>
    private const float easyLowLimitRange = 0.0f;

    /// <summary>
    /// 各パターンの数の上限値
    /// </summary>
    private const float easyHighLimitRange = 5.0f;

    /// <summary>
    /// 各パターンの数の下限値
    /// </summary>
    private const float nomalLowLimitRange = 0.0f;

    /// <summary>
    /// 各パターンの数の上限値
    /// </summary>
    private const float nomalHighLimitRange = 3.0f;

    /// <summary>
    /// パターンのカウント
    /// </summary>
    private int pCount = 0;

    /// <summary>
    /// 各難易度のパターンを何回回すか
    /// </summary>
    private int pCount_Easy = 3;

    /// <summary>
    /// 各難易度のパターンを何回回すか
    /// </summary>
    private int pCount_Nomal = 2;

    /// <summary>
    /// 各難易度のパターンを何回回すか
    /// </summary>
    private int pCount_Hard = 1;

    /// <summary>
    /// パターンカウントのリセット
    /// </summary>
    private const int Reset_pCount = 0;



    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        // 初期化
        correctRoute = new List<string>();
        EasyPattern = new List<GameObject> { EasyPattern1,EasyPattern2,EasyPattern3
                                                , EasyPattern4, EasyPattern5};
        NomalPattern = new List<GameObject> { NomalPattern1, NomalPattern2, NomalPattern3 };
        HardPattern = new List<GameObject> { HardPattern1 };
        m_DifficultyPatten = DifficultyPattern.Easy;

        lowLimitRange = easyLowLimitRange;
        highLimitRange = easyHighLimitRange;

        // 最初のパターンの表示
        SetActive_Pattern();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// パターンの生成
    /// Easyが5回、Nomalが3回、Hardが2回
    /// </summary>
    public void SetActive_Pattern()
    {
        //パターンが出ていない且つ、ディフェンスフェイズなら
        if (!FlagManager.is_pattern && ConditionManager.playerMode == Condition.defense 
            || !FlagManager.is_firstGameScene)
        {
            FlagManager.is_pattern = true;
            switch (m_DifficultyPatten)
            {
                case DifficultyPattern.Easy:
                    var obj = EasyPattern[(int)Random.Range(lowLimitRange, highLimitRange)];
                    obj.SetActive(true);
                    pCount++;
                    if(pCount >= pCount_Easy)
                    {
                        // ノーマルパータンへの引継ぎ
                        m_DifficultyPatten = DifficultyPattern.Nomal;
                        lowLimitRange = nomalLowLimitRange;
                        highLimitRange = nomalHighLimitRange;
                        pCount = Reset_pCount;
                    }
                    break;
                case DifficultyPattern.Nomal:
                    obj = NomalPattern[(int)Random.Range(lowLimitRange, highLimitRange)];
                    obj.SetActive(true);
                    pCount++;
                    if (pCount >= pCount_Nomal)
                    {
                        m_DifficultyPatten = DifficultyPattern.Easy;
                        lowLimitRange = easyLowLimitRange;
                        highLimitRange = easyHighLimitRange;
                        pCount = Reset_pCount;
                    }
                    break;

                    // 使わない今のところ
                case DifficultyPattern.Hard:
                    obj = HardPattern[0];
                    obj.SetActive(true);
                    if(pCount >= pCount_Hard)
                    {
                        m_DifficultyPatten = DifficultyPattern.Easy;
                        lowLimitRange = easyLowLimitRange;
                        highLimitRange = easyHighLimitRange;
                        pCount = Reset_pCount;
                    }
                    break;
            }
            // 今表示中のパターンの取得
            currentPattern = GameObject.FindGameObjectWithTag("Pattern");
            correctRoute = currentPattern.GetComponent<PatternStatus>().Get_routeNumber();
        }
    }


    /// <summary>
    /// 敵による妨害パタ‐ン
    /// </summary>
    public void SetActive_EnemyObstacle()
    {
        if (FlagManager.is_enemyObstacle)
        {
            Debug.Log("a");
            switch (m_DifficultyPatten)
            {
                case DifficultyPattern.Easy:
                    var obj = EasyPattern[(int)Random.Range(lowLimitRange, highLimitRange)];
                    obstaclePattern = Instantiate(obj);
                    obstaclePattern.SetActive(true);
                    obstaclePattern.GetComponent<Image>().color -= new Color(0, 0, 0, 0.25f);
                    break;
                case DifficultyPattern.Nomal:
                    obj = NomalPattern[(int)Random.Range(lowLimitRange, highLimitRange)];
                    obstaclePattern = Instantiate(obj);
                    obstaclePattern.SetActive(true);
                    obstaclePattern.GetComponent<Image>().color -= new Color(0, 0, 0, 0.25f);
                    break;
                case DifficultyPattern.Hard:
                    obj = HardPattern[0];
                    obstaclePattern = Instantiate(obj);
                    obstaclePattern.SetActive(true);
                    obstaclePattern.GetComponent<Image>().color -= new Color(0, 0, 0, 0.25f);
                    break;
            }
        }
    }


    /// <summary>
    /// パターンのリセット
    /// </summary>
    public void NotActive_patterns()
    {
        currentPattern.SetActive(false);
        if(obstaclePattern != null)
        {
            Destroy(obstaclePattern);
        }
        FlagManager.is_pattern = false;
    }

    /// <summary>
    /// ルートを渡す関数
    /// </summary>
    /// <returns>correctRoute</returns>
    public List<string> Get_Route()
    {
        return correctRoute;
    }

    /// <summary>
    /// ディフェンス時のパターンカウントへのセット
    /// </summary>
    public void Set_dPCount(int n)
    {
        pCount = n;
    }

    /// <summary>
    /// パターンの難易度のリセット
    /// </summary>
    public void Reset_DifficultyPattern()
    {
        m_DifficultyPatten = DifficultyPattern.Easy;
    }
}