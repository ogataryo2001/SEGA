using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Failure : MonoBehaviour
{
    /// <summary>
    /// キャラクター
    /// </summary>
    [SerializeField] GameObject Character;

    /// <summary>
    /// コンボキャンバス
    /// </summary>
    [SerializeField] GameObject ComboCanvas;

    /// <summary>
    /// 失敗時のテキストのゲームオブジェクト
    /// </summary>
    [SerializeField] GameObject textFailureObj;

    /// <summary>
    /// 失敗時に出すオブジェクトのテキスト部分
    /// </summary>
    [SerializeField] TextMeshProUGUI textFailureText;

    /// <summary>
    /// kyassyu
    /// </summary>
    private CollisionDetection m_CollisionDetection;

    /// <summary>
    /// kyassyu
    /// </summary>
    private CorrectRouteDetermination m_CorrectRouteDetermination;

    /// <summary>
    /// kyassyu
    /// </summary>
    private PatternCombo m_PatternCombo;

    /// <summary>
    /// ダメージ減算用
    /// </summary>
    private PlayerEffect m_PlayerEffect;

    /// <summary>
    /// コンボリセット用（初期値：０）
    /// </summary>
    private const int comboReset = 0;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        // キャッシュ
        m_CollisionDetection = gameObject.GetComponent<CollisionDetection>();
        m_CorrectRouteDetermination = gameObject.GetComponent<CorrectRouteDetermination>();
        m_PatternCombo = ComboCanvas.GetComponent<PatternCombo>();
        m_PlayerEffect = Character.GetComponent<PlayerEffect>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    internal static void SetFailureText()
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// 失敗時の処理
    /// </summary>
    public void FailureAction()
    {

        // 全フラグを下げる
        FlagManager.is_endOfAction = false;
        FlagManager.is_processAsAWhole = false;
        FlagManager.is_firstTouchDot = false;


        // コンボと通った道順のクリア
        m_CorrectRouteDetermination.Clear_transitPointStorage();
        m_CollisionDetection.Clear_positionOfPassage();

        // コンボカウントのリセット
        m_PatternCombo.SetComboCount(comboReset);


        // 判断のためのカウントの初期化
        m_CorrectRouteDetermination.Clear_jugmentCount();
    }


    /// <summary>
    /// textFailureの非アクティブ化
    /// </summary>
    private void NotActive_textFailure()
    {
        textFailureObj.SetActive(false);
    }
}
