using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTitle : MonoBehaviour
{
    /// <summary>
    /// アニメータークラス
    /// </summary>
    private Animator m_Animator;

    /// <summary>
    /// 経過時間の取得
    /// </summary>
    private float elapsedTime;

    private const float startTime = 3.0f;

    /// <summary>
    /// 一度だけ通るためのフラグ
    /// </summary>
    private bool is_once;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        elapsedTime = Time.time;
        is_once = false;
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        if(Time.time - elapsedTime > startTime && !is_once)
        {
            m_Animator.SetFloat("second", 1.5f);
            is_once = true;
        }
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// アニメーションの停止用関数
    /// </summary>
    private void SetBool_false()
    {
        m_Animator.SetFloat("second", 0.0f);
    }
}
