using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    /// <summary>
    /// 軽減率を取得するため
    /// </summary>
    private PlayerDefense m_PlayerDefense;

    /// <summary>
    /// スライダー
    /// </summary>
    [SerializeField] Slider slider;

    /// <summary>
    /// HP最大値
    /// </summary>
    [SerializeField] float Max_Hp;

    /// <summary>
    /// 通常攻撃のダメージ
    /// </summary>
    [SerializeField] float nomalAttackDamage;

    /// <summary>
    /// スキルのダメージ
    /// </summary>
    [SerializeField] float skillDamage;

    /// <summary>
    /// 現在のHP
    /// </summary>
    private float currentHp;

    /// <summary>
    /// 最大HPの割合値
    /// </summary>
    private const float maxPercent = 1.0f;

    /// <summary>
    /// 最小HPの割合値
    /// </summary>
    private const float minPercent = 0.0f;

    /// <summary>
    /// ダメージ軽減率の保存
    /// </summary>
    private float damage_Reduction;

    /// <summary>
    /// 軽減率を割合に戻すため
    /// </summary>
    private const float betweenValue = 1.0f;

    /// <summary>
    /// 総合のダメージ量
    /// </summary>
    private float totalDamage;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerDefense = gameObject.GetComponentInParent<PlayerDefense>();

        slider.value = maxPercent;
        currentHp = Max_Hp;
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// ダメージ計算関数
    /// </summary>
    public void Calculation_damage(float damage)
    {
        damage_Reduction = m_PlayerDefense.GetSet_raioCount;
        totalDamage = damage * ( betweenValue - damage_Reduction);
        currentHp -= totalDamage;
        slider.value = currentHp / Max_Hp;

        if (slider.value <= minPercent)
        {
            FlagManager.is_pattern = false;
            FlagManager.is_playerDeath = true;
        }
    }

    /// <summary>
    /// プレイヤーのステータスのリセット
    /// </summary>
    public void p_Reset()
    {
        slider.value = maxPercent;
        currentHp = Max_Hp;
    }

    /// <summary>
    /// ダメージの総量を取得する関数
    /// </summary>
    /// <returns>計算後のダメージ量</returns>
    public float p_damageSuffered()
    {
        return totalDamage;
    }

    public float p_nomalAttackDamage()
    {
        return nomalAttackDamage;
    }

    public float p_skillDamage()
    {
        return skillDamage;
    }
}