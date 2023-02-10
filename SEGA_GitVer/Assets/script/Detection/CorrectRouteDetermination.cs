using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectRouteDetermination : MonoBehaviour
{
    /// <summary>
    /// 関数呼び出し用
    /// </summary>
    private Success m_Success;

    /// <summary>
    /// 関数呼び出し用
    /// </summary>
    private Failure m_Failure;

    /// <summary>
    /// 当たり判定管理クラス
    /// </summary>
    private CollisionDetection m_CollisionDetection;

    /// <summary>
    /// パターンの管理クラス
    /// </summary>
    private PatternManager m_PatternManager;

    /// <summary>
    /// 通過点の受け取り変数
    /// </summary>
    private List<GameObject> transitPointStorage;

    /// <summary>
    /// 今のパターンの順番の保管変数
    /// </summary>
    List<string> temporaryCustody = new List<string>(9);

    /// <summary>
    /// 最初から最後まであってたかのカウント
    /// </summary>
    int jugmentCount = 0;

    /// <summary>
    /// jugmentCountのリセット用
    /// </summary>
    const int jugmentCountReset = 0;



    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_Success = gameObject.GetComponent<Success>();
        m_Failure = gameObject.GetComponent<Failure>();
        m_CollisionDetection = gameObject.GetComponent<CollisionDetection>();
        m_PatternManager = gameObject.GetComponent<PatternManager>();
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        //　プレイヤーの処理が一通り終わったら実行
        if (FlagManager.is_processAsAWhole && FlagManager.is_endOfAction)
        {
            Get_pattern();
            Pattern_Judgment();
        }
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// パターンの取得
    /// </summary>
    private void Get_pattern()
    {
        // プレイヤーが描いた順の値を代入
        transitPointStorage = m_CollisionDetection.Get_TransitPoint();
        // 現在表示中の正解のルートの順の値を代入
        temporaryCustody = m_PatternManager.Get_Route();
    }


    /// <summary>
    /// 正解かの検証
    /// </summary>
    private void Pattern_Judgment()
    {
        // 今のパターンと通過したパターンが順番通りになぞれているかのカウント
        for (int i = 0; i < temporaryCustody.Count; i++)
        {
            // 正解のルートの数とプレイヤーが通った点の数が合っているかつ
            // 順番が合っているか
            if (temporaryCustody.Count == transitPointStorage.Count &&
                temporaryCustody[i] == transitPointStorage[i].gameObject.name)
            {
                jugmentCount++;
            }
            else
            {
                break;
            }
        }
        // 正解のルートの長さとカウントが一緒ならば
        if (jugmentCount == temporaryCustody.Count)
        {
            // 成功
           m_Success.SuccessAction();
        }
        else
        {
            // 失敗
            m_Failure.FailureAction();
        }
    }


    /// <summary>
    /// リストクリア関数
    /// </summary>
    public void Clear_transitPointStorage()
    {
        if (transitPointStorage != null)
        {
            transitPointStorage.Clear();
        }
    }


    /// <summary>
    /// 判断のためのカウント
    /// </summary>
    public void Clear_jugmentCount()
    {
        jugmentCount = jugmentCountReset;
    }
}