using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefense : MonoBehaviour
{
    /// <summary>
    /// コンボキャンバス
    /// </summary>
    [SerializeField] GameObject ComboCanvas;

    /// <summary>
    /// コンボ取得用
    /// </summary>
    private PatternCombo m_PatternCombo;

    /// <summary>
    /// ダメージ軽減カウント
    /// </summary>
    private float ratioCount = 0.0f;

    /// <summary>
    /// 簡単なパターンでの軽減率
    /// </summary>
    private const float ratioValue = 0.03f;

    /// <summary>
    /// 軽減率の上昇上限
    /// </summary>
    private const float ratioLimitValue = 0.35f;


    private void Start()
    {
        m_PatternCombo = ComboCanvas.GetComponent<PatternCombo>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// ディフェンスフェイズのアクションを変える関数
    /// </summary>
    public void action_defenseCondition()
    {
        if (ConditionManager.playerMode == Condition.defense)
        {
            if(ratioLimitValue >= ratioCount || m_PatternCombo.Get_ComboCount() == 0)
            {
                ratioCount = m_PatternCombo.Get_ComboCount() * ratioValue;

                // 上限を越えたら上限値まで下げる
                if(ratioCount > ratioLimitValue)
                {
                    ratioCount = ratioLimitValue;
                }
            }
        }
    }

    /// <summary>
    /// ratioCountのゲッターセッター
    /// </summary>
    public float GetSet_raioCount
    {
        get { return this.ratioCount; }
        set { this.ratioCount = value; }
    }
 }