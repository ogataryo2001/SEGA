using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutinPlayerAnim : MonoBehaviour
{
    /// <summary>
    /// キャラクター格納リスト
    /// </summary>
    [SerializeField] List<GameObject> character;

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
    /// 一度きり用
    /// </summary>
    bool is_once;

    private void Start()
    {
        startTime = Time.time;
        is_once = false;
    }


    private void Update()
    {
        nowTime = Time.time;
        if (nowTime - startTime > MAX_TIME && !is_once)
        {
            foreach(var n in character)
            {
                n.GetComponent<Animator>().SetBool("gearBurst", true);
                is_once = true;
            }
        }
    }



    /// <summary>
    /// Animation Event でのフラグ下げ
    /// </summary>
    private void BoolFalse_Angry()
    {
        foreach(var n in character)
        {
            n.GetComponent<Animator>().SetBool("gearBurst", false);
        }
    }
}