using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    /// <summary>
    /// 体力取得用
    /// </summary>
    private EnemyStatus m_EnemyStatus;

    /// <summary>
    /// 体力の半分の値
    /// </summary>
    private const float half_HP = 0.5f;

    /// <summary>
    /// 攻撃の発動確率　15%
    /// </summary>
    private const float attackProbability = 25.0f;

    /// <summary>
    /// 行動するかの計算をするまでの待機時間
    /// </summary>
    private float probabilityTime;

    /// <summary>
    /// 現在時刻の保存変数
    /// </summary>
    private float nowTime;

    /// <summary>
    /// 攻撃までの時間の最大待機時間
    /// </summary>
    private const float Max_waitTime = 10.0f;

    /// <summary>
    /// 攻撃までの時間の最低待機時間
    /// </summary>
    private const float Min_waitTime = 7.0f;

    /// <summary>
    /// スキルが発動したか
    /// </summary>
    private bool is_skillInvoke = false;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        // 体力の取得のため
        m_EnemyStatus = gameObject.GetComponentInParent<EnemyStatus>();
        // ゲーム開始時間の取得
        nowTime = Time.time;
        // 初期待機時間
        probabilityTime = Max_waitTime;
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        Enemy_Movement();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 敵の行動処理関数
    /// </summary>
    public void Enemy_Movement()
    {
        if(FlagManager.is_direction)
        {
            Attack_EstablishmentCalculations();
        }
        if(m_EnemyStatus.Get_HpValue() <= half_HP)
        {
            Skill_invoke();
        }
    }


    /// <summary>
    /// 攻撃するかの確率計算をする関数
    /// </summary>
    public void Attack_EstablishmentCalculations()
    {
        if (ConditionManager.enemyMode != Condition.attack || ConditionManager.enemyMode != Condition.skill)
        {
            var n = Random.Range(0, 100);
            var m = Time.time;

            if(nowTime + probabilityTime < m)
            {
                nowTime = Time.time;
                if (n < attackProbability)
                {
                    // 攻撃するなら最大待機時間で再抽選
                    probabilityTime = Max_waitTime;
                    ConditionManager.enemyMode = Condition.attack;
                }
                else
                {
                    // 攻撃しないなら最小待機時間で再抽選
                    probabilityTime = Min_waitTime;
                }
            }
        }
    }

    /// <summary>
    /// スキルの発動関数
    /// </summary>
    public void Skill_invoke()
    {
        if (ConditionManager.enemyMode != Condition.attack
            && !is_skillInvoke)
        {
            ConditionManager.enemyMode = Condition.skill;
            is_skillInvoke = true;
        }
    }
}