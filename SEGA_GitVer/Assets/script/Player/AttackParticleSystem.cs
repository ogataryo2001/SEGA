using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticleSystem: MonoBehaviour
{

    /// <summary>
    /// 爆発パーティクル
    /// </summary>
    [SerializeField] ParticleSystem explotionParticle;

    /// <summary>
    /// ダメージ取得用
    /// </summary>
    private PlayerEffect m_PlayerEffect;

    /// <summary>
    /// 攻撃力
    /// </summary>
    private float offensiveAbility;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerEffect = gameObject.GetComponentInParent<PlayerEffect>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 敵に当たったら
    /// </summary>
    /// <param name="collision">敵</param>
    private void OnParticleCollision(GameObject other)
    {
        // ダメージアニメーションの再生
        other.GetComponentInChildren<MonsterAnimation>().e_DamageAnim();

        offensiveAbility = m_PlayerEffect.Get_attackDamage();
        other.GetComponent<EnemyStatus>().Calculation_damage(offensiveAbility);

        m_PlayerEffect.Stop_attackParticle();

        explotionParticle.Play();
    }
}
