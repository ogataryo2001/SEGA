using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    /// <summary>
    /// スキル発動時に回転する用
    /// </summary>
    [SerializeField] private GameObject parent;

    /// <summary>
    /// シーンコントローラ取得用
    /// </summary>
    [SerializeField] GameObject mainCamera;

    /// <summary>
    /// カットイン呼び出し用
    /// </summary>
    [SerializeField] GameObject CutinCanvas;

    /// <summary>
    /// ステータスリセット用
    /// </summary>
    private EnemyStatus m_EnemyStatus;

    /// <summary>
    /// シーンセット用
    /// </summary>
    private SceneController m_SceneController;

    /// <summary>
    /// フラグ管理よう
    /// </summary>
    private Animator m_Animator;

    /// <summary>
    /// カットイン再生用
    /// </summary>
    private CutinManager m_CutinManager;

    /// <summary>
    /// 攻撃までの予備動作時間
    /// </summary>
    [SerializeField] private float attackDelayTime;

    /// <summary>
    /// 攻撃するまでの現在の待機時間
    /// </summary>
    private float standbyTime;

    /// <summary>
    /// スキル発動するまでの待機時間
    /// </summary>
    [SerializeField] private float skillStandbyTime;

    /// <summary>
    /// 失敗時のダウンタイム
    /// </summary>
    [SerializeField] private float missDownTime;

    /// <summary>
    /// 数字をそろえるために10倍するための定数
    /// </summary>
    private const float twiceValue = 3.0f;

    /// <summary>
    /// 死亡時のアニメーションをスローに
    /// </summary>
    private const float deadSlow = 0.5f;

    /// <summary>
    /// タイムスケールの初期化
    /// </summary>
    private const float Init_timeScale = 1.0f;

    /// <summary>
    /// スキル発動時の回転角度
    /// </summary>
    private Vector3 skillRotate = new Vector3(0, 40, 0);

    /// <summary>
    /// スキル発動前までの角度に戻すよう
    /// </summary>
    private Vector3 skillRotateInit = new Vector3(0, -40, 0);

    /// <summary>
    /// カットイン再生用
    /// </summary>
    private bool is_cutinRelease = false;

    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_EnemyStatus = gameObject.GetComponentInParent<EnemyStatus>();
        m_SceneController = mainCamera.GetComponent<SceneController>();
        m_CutinManager = CutinCanvas.GetComponent<CutinManager>();
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        if(ConditionManager.enemyMode == Condition.attack)
        {
            e_AttackAnim();
        }
        if(ConditionManager.enemyMode == Condition.skill)
        {
            e_skillAnim();
        }
        if(FlagManager.is_enemyDead)
        {
            e_DeadAnim();
        }
    }



    //-----------------------------------------
    // 各関数
    //-----------------------------------------



    //-----------------------------------------
    // 攻撃アニメーション

    /// <summary>
    /// アタック用のアニメーション関数
    /// </summary>
    private void e_AttackAnim()
    {
        m_Animator.SetBool("attack", true);
    }

    /// <summary>
    /// アタックアニメーションの終了処理
    /// </summary>
    private void e_EndAttackAnim()
    {
        m_Animator.SetBool("attack", false);
        ConditionManager.enemyMode = Condition.idle;
    }

    /// <summary>
    /// Loop部分のカウント
    /// </summary>
    private void CountToTransition()
    {
        standbyTime += Time.deltaTime;
        standbyTime = standbyTime * twiceValue;
        if (attackDelayTime <= standbyTime)
        {
            e_EndAttackAnim();
            standbyTime = 0.0f;
        }
    }


    //-----------------------------------------
    // スキルアニメーション

    private void e_skillAnim()
    {
        m_Animator.SetBool("skill", true);

        if(!is_cutinRelease)
        {
            m_CutinManager.EnemyCutin();
            is_cutinRelease = true;
        }
    }

    private void e_skillAnimRotate()
    {
        parent.transform.Rotate(skillRotate);
    }

    private void e_skillLoop()
    {
        m_Animator.SetBool("skill_loop", true);
        StartCoroutine(Wait_skillInvoke(skillStandbyTime));
    }

    private IEnumerator Wait_skillInvoke(float delay)
    {
        yield return new WaitForSeconds(delay);

        m_Animator.SetBool("skill_loop", false);

        yield break;
    }

    /// <summary>
    /// プレイヤーがガードしていないならそのまま攻撃終了
    /// </summary>
    public void e_skill_Success()
    {
        m_Animator.SetBool("skill_Success", true);
    }

    /// <summary>
    /// プレイヤーがガードしていたらダウン
    /// </summary>
    public void e_skill_Failure()
    {
        m_Animator.SetBool("skill_Failure", true);
    }

    private void e_skillMissLoop()
    {
        m_Animator.SetBool("skill_missLoop", true);
        StartCoroutine(e_EndSkillMissLoop(missDownTime));
    }

    private IEnumerator e_EndSkillMissLoop(float delay)
    {
        yield return new WaitForSeconds(delay);

        m_Animator.SetBool("skill_missLoop", false);

        yield break;
    }

    private void e_EndSkillAnim()
    {
        parent.transform.Rotate(skillRotateInit);
        m_Animator.SetBool("skill", false);
        m_Animator.SetBool("skill_Success", false);
        m_Animator.SetBool("skill_Failure", false);
        ConditionManager.enemyMode = Condition.idle;

        is_cutinRelease = false;
    }


    //-----------------------------------------
    // ダメージアニメーション

    /// <summary>
    /// damageAnimationへの遷移
    /// </summary>
    public void e_DamageAnim()
    {
        m_Animator.SetBool("damage", true);
    }

    /// <summary>
    /// ダメージモーションの終了
    /// </summary>
    public void e_EndDamageAnim()
    {
        m_Animator.SetBool("damage", false);
    }


    //-----------------------------------------
    // 死亡アニメーション

    /// <summary>
    /// deadAnimationへの遷移
    /// </summary>
    private void e_DeadAnim()
    {
        m_Animator.SetBool("dead", true);
        Time.timeScale = deadSlow;
    }

    /// <summary>
    /// 死亡アニメーションの終了処理
    /// </summary>
    private void End_deadAnim()
    {
        Time.timeScale = Init_timeScale;
        FlagManager.is_changeScene = true;
        FlagManager.is_enemyDead = false;

        m_EnemyStatus.e_Reset();

        m_SceneController.Set_sceneName("GameClearScene");
    }
}