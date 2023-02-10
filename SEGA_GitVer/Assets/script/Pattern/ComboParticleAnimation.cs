using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboParticleAnimation : MonoBehaviour
{
    /// <summary>
    /// コンボ用のパーティクルシステム
    /// </summary>
    private ParticleSystem combParticleSystem;

    /// <summary>
    /// パーティクル用のアニメーション
    /// </summary>
    private Animator m_Animator;

    private const float Min_particleCount = 0.0f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        // 取得
        combParticleSystem = gameObject.GetComponent<ParticleSystem>();
        m_Animator = gameObject.GetComponent<Animator>();
    }


    /// <summary>
    /// 動いていいか調べて動かす関数
    /// </summary>
    private void Examine_move()
    {
        if (combParticleSystem.particleCount > Min_particleCount)
        {
            m_Animator.SetBool("move", true);
        }
    }


    /// <summary>
    /// パーティクルシステムの非アクティブ化関数
    /// </summary>
    private void SetActive_false()
    {
        gameObject.SetActive(false);
    }
}
