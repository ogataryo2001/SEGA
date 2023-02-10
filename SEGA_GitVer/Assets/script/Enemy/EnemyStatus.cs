using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [Header("モデル")]
    [SerializeField] GameObject enemyModel;

    /// <summary>
    /// スキル発動後の処理のため
    /// </summary>
    private MonsterAnimation m_MonsterAnimation;

    /// <summary>
    /// スライダー
    /// </summary>
    [SerializeField] Slider slider;

    /// <summary>
    /// HP最大値
    /// </summary>
    [Header("体力値")]
    [SerializeField] float Max_Hp;

    /// <summary>
    /// 攻撃力
    /// </summary>
    [Header("攻撃値")]
    [SerializeField] float attackPawer;

    /// <summary>
    /// スキルの攻撃力
    /// </summary>
    [Header("スキルの攻撃力")]
    [SerializeField] float skillPower;

    /// <summary>
    /// 現在のHP
    /// </summary>
    private float currentHp;

    /// <summary>
    /// スライダーの値の初期化
    /// </summary>
    private const float Init_sliderValue = 1.0f;

    /// <summary>
    /// スライダーの値の最下値
    /// </summary>
    private const float Min_sliderValue = 0.0f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_MonsterAnimation = enemyModel.GetComponent<MonsterAnimation>();

        slider.value = Init_sliderValue;
        currentHp = Max_Hp;
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// スキルでプレイヤーに当たったら判定処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // プレイヤーがガードモーションでなければ
            if(ConditionManager.playerMode != Condition.guard)
            {
                collision.gameObject.
                    GetComponent<PlayerStatus>().Calculation_damage(skillPower);
                m_MonsterAnimation.e_skill_Success();
                Debug.Log("a");
            }
            else if(ConditionManager.playerMode == Condition.guard)
            {
                m_MonsterAnimation.e_skill_Failure();
                Debug.Log("b");
            }
        }
    }

    /// <summary>
    /// ダメージ計算関数
    /// </summary>
    public void Calculation_damage(float damage)
    {
        currentHp -= damage;
        slider.value = currentHp / Max_Hp;

        // スライダーが最小値なら
        if(slider.value <= Min_sliderValue)
        {
            FlagManager.is_pattern = false;
            FlagManager.is_enemyDead = true;
        }
    }

    /// <summary>
    /// 攻撃力の取得
    /// </summary>
    /// <returns>攻撃力</returns>
    public float Get_attackPower()
    {
        return attackPawer;
    }

    /// <summary>
    /// HPの割合取得
    /// </summary>
    /// <returns>HPの割合</returns>
    public float Get_HpValue()
    {
        return slider.value;
    }

    /// <summary>
    /// エネミーのステータスのリセット
    /// </summary>
    public void e_Reset()
    {
        slider.value = Init_sliderValue;
        currentHp = Max_Hp;
    }
}