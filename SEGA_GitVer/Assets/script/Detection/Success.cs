using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    /// <summary>
    /// キャラクター
    /// </summary>
    [SerializeField] GameObject Character;

    /// <summary>
    /// コンボ加算するため
    /// </summary>
    [SerializeField] GameObject ComboCanvas;

    /// <summary>
    /// ダメージ割合軽減するため
    /// </summary>
    private PlayerDefense m_PlayerDefense;

    /// <summary>
    /// 描いていたパターンの判定をクリア
    /// </summary>
    private CollisionDetection m_CollisionDetection;

    /// <summary>
    /// 格納していた道順のクリア
    /// </summary>
    private CorrectRouteDetermination m_CorrectRouteDetermination;

    /// <summary>
    /// パターンの削除と再セット
    /// </summary>
    private PatternManager m_PatternManager;

    /// <summary>
    /// コンボ加算用
    /// </summary>
    private PatternCombo m_PatternCombo;

    /// <summary>
    /// ダメージ加算用
    /// </summary>
    private PlayerEffect m_PlayerEffect;

    /// <summary>
    /// プレイヤーのスキル管理クラス
    /// </summary>
    private PlayerSkillManager m_PlayerSkillManager;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerDefense = Character.GetComponent<PlayerDefense>();
        m_CollisionDetection = gameObject.GetComponent<CollisionDetection>();
        m_CorrectRouteDetermination = gameObject.GetComponent<CorrectRouteDetermination>();
        m_PatternManager = gameObject.GetComponent<PatternManager>();
        m_PatternCombo = ComboCanvas.GetComponent<PatternCombo>();
        m_PlayerEffect = Character.GetComponent<PlayerEffect>();
        m_PlayerSkillManager = Character.GetComponent<PlayerSkillManager>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 成功時の処理関数
    /// </summary>
    public void SuccessAction()
    {

        // 全体のアクション
        m_PatternCombo.AddComb();
        // コンディションがアタックの時
        m_PlayerEffect.Calculation_attackDamageUp();
        // コンディションがディフェンスの時
        m_PlayerDefense.action_defenseCondition();
        // スキルゲージの上昇
        m_PlayerSkillManager.Rising_SkillGauge();

        // 成功のためのフラグを立てる
        FlagManager.is_playerAction = true;

        // 行動終了処理
        FlagManager.is_endOfAction = false;
        FlagManager.is_processAsAWhole = false;
        FlagManager.is_firstTouchDot = false;


        // 通過点保存場所のクリア
        m_CorrectRouteDetermination.Clear_transitPointStorage();
        m_CollisionDetection.Clear_positionOfPassage();

        // 成功したためｍパターンのリセットと次パターンのセット
        m_PatternManager.NotActive_patterns();
        m_PatternManager.SetActive_Pattern();
        //m_PatternManager.SetActive_EnemyObstacle();

        // 判断のためのカウントの初期化
        m_CorrectRouteDetermination.Clear_jugmentCount();
    }

}