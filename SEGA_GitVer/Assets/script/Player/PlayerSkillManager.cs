using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillManager : MonoBehaviour
{
    /// <summary>
    /// スキルの発動の有無を管理するスライダー
    /// </summary>
    [SerializeField] Slider skillSlider;

    /// <summary>
    /// スキルを発動するためのボタン
    /// </summary>
    [SerializeField] Button skillButton;

    /// <summary>
    /// 上昇値
    /// </summary>
    private const float eleventedValue = 0.1f;

    private const float eleventedValue_guardSuccess = 0.4f;

    /// <summary>
    /// 初期値,最大値
    /// </summary>
    private const float initialValue = 1.0f;

    /// <summary>
    /// スキルが発動できる値
    /// </summary>
    private const float invakealeValue = 0.0f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        skillSlider.value = initialValue;
        skillButton.image.raycastTarget = false;
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------



    /// <summary>
    /// スキルゲージの上昇値計算
    /// </summary>
    public void Rising_SkillGauge()
    {
        // ディフェンス状態でなければスキルゲージをためる
        if(ConditionManager.playerMode != Condition.defense)
        {
            skillSlider.value -= eleventedValue;
        }

        // ０になったらスキル発動可能
        if (skillSlider.value <= invakealeValue)
        {
            FlagManager.is_skillReady = true;
            skillButton.image.raycastTarget = true;
        }
    }

    /// <summary>
    /// スキルゲージの上昇計算ガード成功時用
    /// </summary>
    public void Rising_SkillGauge_guardSuccess()
    {
        skillSlider.value -= eleventedValue_guardSuccess;
    }

    /// <summary>
    /// スキルゲージの初期化
    /// </summary>
    public void Init_SkillGauge()
    {
        skillSlider.value = initialValue;
        FlagManager.is_skillReady = false;
        skillButton.image.raycastTarget = false;
    }
}