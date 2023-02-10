using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    /// <summary>
    /// アニメーター
    /// </summary>
    private Animator m_Animator;

    /// <summary>
    /// 経過時間
    /// </summary>
    private float elapsedTime;

    private const float startTime = 3.0f;

    /// <summary>
    /// 一度きり用
    /// </summary>
    private bool is_once;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        elapsedTime = Time.time;
        is_once = false;
    }

    private void Update()
    {
        if(Time.time - elapsedTime > startTime && !is_once)
        {
            m_Animator.SetBool("bow_dead", true);
            is_once = true;
        }
    }

    private void SetBool_false()
    {
        m_Animator.SetBool("bow_dead", false);
    }

}
