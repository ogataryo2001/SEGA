using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutinMonsterAnim : MonoBehaviour
{
    /// <summary>
    /// 開始時間の取得
    /// </summary>
    float startTime;

    /// <summary>
    /// 現在時刻
    /// </summary>
    float nowTime;

    /// <summary>
    /// 最大経過時間
    /// </summary>
    const float MAX_TIME = 3.0f;

    /// <summary>
    /// アニメーション遷移用
    /// </summary>
    Animator m_Animator;

    /// <summary>
    /// 一度きり用
    /// </summary>
    bool is_once;

    private void Start()
    {
        startTime = Time.time;
        m_Animator = gameObject.GetComponent<Animator>();
        is_once = false;
    }


    private void Update()
    {
        nowTime = Time.time;
        if(nowTime- startTime > MAX_TIME && !is_once)
        {
            m_Animator.SetBool("angry", true);
            is_once = true;
        }
    }



    /// <summary>
    /// Animation Event でのフラグ下げ
    /// </summary>
    private void BoolFalse_Angry()
    {
        m_Animator.SetBool("angry", false);
    }
}
