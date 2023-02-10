using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{

    /// <summary>
    /// 矢の生成用
    /// </summary>
    private PlayerEffect m_PlayerEffect;

    /// <summary>
    /// 音楽再生用
    /// </summary>
    private PlaySoundSE m_PlaySoundSE;

    /// <summary>
    /// アニメーション管理用
    /// </summary>
    private PlayerAnim m_PlayerAnim;

    /// <summary>
    /// プレイヤーのスキル管理クラス
    /// </summary>
    private PlayerSkillManager m_PlayerSkillManager;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerEffect = gameObject.GetComponentInParent<PlayerEffect>();
        m_PlaySoundSE = gameObject.GetComponentInParent<PlaySoundSE>();
        m_PlayerAnim = gameObject.GetComponentInParent<PlayerAnim>();
        m_PlayerSkillManager = gameObject.GetComponentInParent<PlayerSkillManager>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 矢の生成イベント
    /// </summary>
    private void Instance_allow()
    {
        if (!FlagManager.is_allowInstance)
        {
            m_PlayerEffect.Invoke_allow();
            FlagManager.is_allowInstance = true;
        }
    }

    /// <summary>
    /// 攻撃用パーティクルの生成
    /// </summary>
    private void Instance_attackParticle()
    {
        m_PlayerEffect.Invoke_attackParticle();
    }

    /// <summary>
    /// 音の再生
    /// </summary>
    private void PlaySound()
    {
        m_PlaySoundSE.OnPlaySounds();
    }

    /// <summary>
    /// アタックのアニメーションループの終了
    /// </summary>
    private void Branch_GearBurstAnim()
    {
        m_PlayerAnim.p_GuarBurstAnimBranch();
    }

    /// <summary>
    /// ぎあばーすとの終了
    /// </summary>
    private void End_GearBurstAnim()
    {
        m_PlayerAnim.p_EndGearBurstAnim();
        m_PlayerSkillManager.Init_SkillGauge();
        ConditionManager.playerMode = Condition.attack;
    }

    /// <summary>
    /// アタックアニメーションの終了
    /// </summary>
    private void End_attackAction()
    {
        m_PlayerAnim.p_End_attack();
    }

    /// <summary>
    /// damageAnimation
    /// </summary>
    private void End_damageAction()
    {
        m_PlayerAnim.p_EndDamageAnim();
    }

    /// <summary>
    /// ループの解除
    /// </summary>
    private void SetBool_Loop()
    {
        m_PlayerAnim.SetBool_defenseLoopFalse();
    }

    /// <summary>
    /// 死亡アニメーションの停止
    /// </summary>
    private void End_Dead()
    {
        m_PlayerAnim.p_EndDeadAnim();
    }
}