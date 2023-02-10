using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    /// <summary>
    /// エフェクト　矢
    /// </summary>
    [SerializeField] GameObject effect_allow;

    /// <summary>
    /// 攻撃用のパーティクル
    /// </summary>
    [SerializeField] ParticleSystem attackParticle;

    /// <summary>
    /// コンボキャンバス
    /// </summary>
    [SerializeField] GameObject ComboCanvas;

    /// <summary>
    /// プレイヤーの能力値取得用
    /// </summary>
    private PlayerStatus m_PlayerStatus;

    /// <summary>
    /// 矢のスクリプトのアクティブ状態管理
    /// </summary>
    private AllowEvent m_AllowEvent;

    /// <summary>
    /// コンボカウント取得用
    /// </summary>
    private PatternCombo m_PatternCombo;

    /// <summary>
    /// 送る値（パーティクル）
    /// </summary>
    private float sendValue_attack;

    /// <summary>
    /// 加算値
    /// </summary>
    private const float additionValue = 5.0f;

    /// <summary>
    /// 減算値
    /// </summary>
    private const float subtractionValue = 10.0f;

    /// <summary>
    /// 上昇上限値
    /// </summary>
    private const float additionLimitValue = 40.0f;

    /// <summary>
    /// 今のコンボ取得変数
    /// </summary>
    private float m_NowCombo;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerStatus = gameObject.GetComponent<PlayerStatus>();
        m_AllowEvent = effect_allow.GetComponent<AllowEvent>();
        m_PatternCombo = ComboCanvas.GetComponent<PatternCombo>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 矢のアクティブ化と発射
    /// </summary>
    public void Invoke_allow()
    {
        effect_allow.SetActive(true);
        m_AllowEvent.enabled = true;
    }

    /// <summary>
    /// パーティクルの再生
    /// </summary>
    public void Invoke_attackParticle()
    {
        attackParticle.Play();
    }

    /// <summary>
    /// パーティクルのダメージ上昇量計算
    /// </summary>
    public void Calculation_attackDamageUp()
    {
        m_NowCombo = m_PatternCombo.Get_ComboCount();
        if(sendValue_attack <= additionLimitValue)
        {
            sendValue_attack = m_PlayerStatus.p_skillDamage() + (additionValue * m_NowCombo);
        }
    }

    /// <summary>
    /// パーティクルのダメージ減少量計算
    /// </summary>
    public void Calculation_attackDamageDown()
    {
        sendValue_attack -= subtractionValue;
    }

    /// <summary>
    /// パーティクルの計算後のダメージ処理
    /// </summary>
    /// <returns>攻撃値</returns>
    public float Get_attackDamage()
    {
        return sendValue_attack;
    }

    /// <summary>
    /// パーティクルの停止処理
    /// </summary>
    public void Stop_attackParticle()
    {
        attackParticle.Stop();
        attackParticle.Clear();
    }
}