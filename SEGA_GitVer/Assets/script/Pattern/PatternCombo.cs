using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatternCombo : MonoBehaviour
{
    /// <summary>
    /// コンボの時に使うパネル
    /// </summary>
    [SerializeField] GameObject comboPanel;


    /// <summary>
    /// コンボ用のパーティクル
    /// </summary>
    [SerializeField] GameObject comboParticle;


    /// <summary>
    /// コンボカウントのオブジェクト
    /// </summary>
    [SerializeField] GameObject comboCountObj;

    /// <summary>
    /// コンボcountするテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI ComboCountText;

    /// <summary>
    /// パネルを動かすよう
    /// </summary>
    private Animator m_Animator;

    /// <summary>
    /// コンボのカウント
    /// </summary>
    private float comboCount = 0.0f;

    /// <summary>
    /// コンボゼロ
    /// </summary>
    private const float Min_combo = 0.0f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_Animator = comboPanel.GetComponent<Animator>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    // コンボテキストにアクセスして実行するため
    internal static void SetCombotext()
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// コンボ関連関数
    /// </summary>
    public void AddComb()
    {
        MovePanel();
        AddComboCount();
    }


    /// <summary>
    /// パネルの動きの関数
    /// </summary>
    private void MovePanel()
    {
        // パーティクルの再生
        comboParticle.SetActive(true);
        comboPanel.SetActive(true);
        m_Animator.Play("comboPanel", 0);
    }


    /// <summary>
    /// コンボのcount関数
    /// </summary>
    private void AddComboCount()
    {
        // コンボ加算
        comboCount++;

        // テキストのフォーマットを設定する
        ComboCountText.text = string.Format("{0}コンボ", comboCount);

        // コンボがゼロ以上ならテキスト表示
        if(comboCount > Min_combo)
        {
            comboCountObj.SetActive(true);
        }
    }


    /// <summary>
    /// combocountへのセッター
    /// </summary>
    /// <param name="SetCount">セットしたい数字</param>
    public void SetComboCount(int SetCount)
    {
        this.comboCount = SetCount;
        comboCountObj.SetActive(false);
    }


    /// <summary>
    /// コンボ取得
    /// </summary>
    /// <returns>現在のコンボ数</returns>
    public float Get_ComboCount()
    {
        return comboCount;
    }
}
