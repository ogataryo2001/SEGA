using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    /// <summary>
    /// player
    /// </summary>
    [SerializeField] List<GameObject> character;

    /// <summary>
    /// シーンコントローラー取得用
    /// </summary>
    [SerializeField] GameObject mainCamera;

    /// <summary>
    /// パターンコンボ取得用
    /// </summary>
    [SerializeField] GameObject ComboCanvas;

    /// <summary>
    /// 体力リセット用
    /// </summary>
    private PlayerStatus m_PlayerStatus;

    /// <summary>
    /// シーンセット用
    /// </summary>
    private SceneController m_SceneController;

    /// <summary>
    /// コンボ取得用
    /// </summary>
    private PatternCombo m_PatternCombo;

    /// <summary>
    /// ガード成功時用
    /// </summary>
    private PlayerSkillManager m_PlayerSkillManager;

    /// <summary>
    /// コンボ数
    /// </summary>
    private float playerCombo;

    /// <summary>
    /// コンボゼロ
    /// </summary>
    private const float combo_zero = 0.0f;

    /// <summary>
    /// ガード開始時間
    /// </summary>
    private float guardStartTime = 0.0f;

    /// <summary>
    /// ガードする時間
    /// </summary>
    private const float guardTime = 1.5f;

    /// <summary>
    /// 死亡時のスローモーション
    /// </summary>
    private const float deadSlow = 0.5f;

    /// <summary>
    /// 時間のスピード
    /// </summary>
    private const float timeSpeed = 1.0f;

    /// <summary>
    /// アニメーション用のフラグ管理
    /// bowAttack bowGuard bowGuardLp bowDamage
    /// bowDamage bowGb bowGbLp 
    /// </summary>
    private Dictionary<string, string> animName;



    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerStatus = gameObject.GetComponent<PlayerStatus>();
        m_SceneController = mainCamera.GetComponent<SceneController>();
        m_PatternCombo = ComboCanvas.GetComponent<PatternCombo>();
        m_PlayerSkillManager = gameObject.GetComponent<PlayerSkillManager>();


        // 初期化
        Set_animName();

        // 演出用
        p_RunAnim();

    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        if(FlagManager.is_standingPos && !FlagManager.is_direction)
        {
            End_Run();
        }
        if (FlagManager.is_direction)
        {
            PlayerAction();
        }
        // 死亡フラグがたったら
        if(FlagManager.is_playerDeath)
        {
            p_DeadAnim();
        }
        playerCombo = m_PatternCombo.Get_ComboCount();
    }



    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 名前のセット
    /// </summary>
    private void Set_animName()
    {
        animName = new Dictionary<string, string>
        { 
          {   "b_Attack",   "attack01"          },
          {   "b_Guard",    "guard"             },
          {   "b_GuardLp",  "guard_Loop"        },
          {   "b_Damage",   "damage"            },
          {   "b_Gb",       "gearBurst"         },
          {   "b_GbAttack", "gearBurst_attack"  },
          {   "b_GbMiss",   "gearBurst_miss"    },
          {   "b_Dead",     "dead"              },
          {   "b_Run",      "Run"               }
        };
    }

    /// <summary>
    /// プレイヤーの行動処理
    /// </summary>
    private void PlayerAction()
    {
        // パターンを消してるかつエネミーの各々ターンでなければ攻撃
        if (FlagManager.is_playerAction && ConditionManager.playerMode == Condition.attack)
        {
            p_AttackAnimState();
        }
        if (FlagManager.is_playerAction && ConditionManager.playerMode == Condition.defense)
        {
            p_DefenseAnimState();
        }
        if (FlagManager.is_skillReady && ConditionManager.playerMode == Condition.skill)
        {
            p_GuarBurstAnimState();
        }

    }


    /// <summary>
    /// AttackAnimationへの遷移(フラグを立てる
    /// </summary>
    private void p_AttackAnimState()
    {
        foreach(var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Attack"], true);
        }
        FlagManager.is_playerAction = false;
    }

    /// <summary>
    /// GuarBurstAnimationの遷移(フラグを立てる
    /// </summary>
    private void p_GuarBurstAnimState()
    {
        foreach(var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Gb"], true);
        }
   }

    /// <summary>
    /// GuarBurstAnimationの分岐処理
    /// </summary>
    public void p_GuarBurstAnimBranch()
    {
            if (ConditionManager.playerMode == Condition.skill)
            {
                if(playerCombo != combo_zero)
                {
                    foreach (var n in character)
                    {
                        n.GetComponent<Animator>().SetBool(animName["b_Gb"], false);
                        n.GetComponent<Animator>().SetBool(animName["b_GbAttack"], true);
                    }
                }
                else
                {
                    foreach (var n in character)
                    {
                        n.GetComponent<Animator>().SetBool(animName["b_Gb"], false);
                        n.GetComponent<Animator>().SetBool(animName["b_GbMiss"], true);
                    }
                }
                FlagManager.is_playerGearBurstLoop = false;
            }
    }

    /// <summary>
    /// GearBurstAnimationの終了遷移
    /// </summary>
    public void p_EndGearBurstAnim()
    {
        foreach(var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Gb"], false);
            n.GetComponent<Animator>().SetBool(animName["b_GbAttack"], false);
            n.GetComponent<Animator>().SetBool(animName["b_GbMiss"], false);
        }
    }


    /// <summary>
    /// プレイヤーの攻撃終了処理
    /// </summary>
    public void p_End_attack()
    {
        foreach (var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Attack"], false);
        }
        FlagManager.is_playerAction = false;
    }


    /// <summary>
    /// 防御のアニメーション処理
    /// </summary>
    private void p_DefenseAnimState()
    {
        foreach (var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Guard"], true);
            n.GetComponent<Animator>().SetBool(animName["b_GuardLp"], true);
        }
        ConditionManager.playerMode = Condition.guard;
        guardStartTime = Time.time;
        FlagManager.is_playerAction = false;
    }


    /// <summary>
    /// ディフェンスのループ停止ボタン
    /// </summary>
    public void SetBool_defenseLoopFalse()
    {
        if(guardStartTime + guardTime < Time.time)
        {
            foreach (var n in character)
            {
                n.GetComponent<Animator>().SetBool(animName["b_Guard"], false);
                n.GetComponent<Animator>().SetBool(animName["b_GuardLp"], false);
            }
            if(ConditionManager.playerMode != Condition.attack)
            {
                ConditionManager.playerMode = Condition.defense;
            }
            FlagManager.is_playerAction = false;
        }
    }


    /// <summary>
    /// ダメージ処理分岐
    /// </summary>
    public void p_DamageAnimBranch()
    {
        if (ConditionManager.playerMode != Condition.guard)
        {
            foreach (var n in character)
            {
                n.GetComponent<Animator>().SetBool(animName["b_Damage"], true);
            }
        }
        else
        {
            m_PlayerSkillManager.Rising_SkillGauge_guardSuccess();
        }
    }

    /// <summary>
    /// DamageAnimationの終了処理
    /// </summary>
    public void p_EndDamageAnim()
    {
        foreach (var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Damage"], false);
        }
        FlagManager.is_playerAction = false;
    }

    /// <summary>
    /// ゲームシーンが始まってからの演出のためのRun
    /// </summary>
    private void p_RunAnim()
    {
        foreach (var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Run"], true);
        }
    }

    /// <summary>
    /// ラン終了処理
    /// </summary>
    private void End_Run()
    {
        foreach (var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Run"], false);
        }
    }

    /// <summary>
    /// プレイヤーの死亡アニメーション
    /// </summary>
    private void p_DeadAnim()
    {
        foreach(var n in character)
        {
            n.GetComponent<Animator>().SetBool(animName["b_Dead"], true);
        }
        Time.timeScale = deadSlow;
    }

    /// <summary>
    /// 死亡アニメーションの終了
    /// </summary>
    public void p_EndDeadAnim()
    {
        Time.timeScale = timeSpeed;
        FlagManager.is_changeScene = true;
        FlagManager.is_playerDeath = false;

        m_PlayerStatus.p_Reset();

        m_SceneController.Set_sceneName("GameOverScene");
    }
}