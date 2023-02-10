using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    /// <summary>
    /// 敵
    /// </summary>
    [SerializeField] GameObject enemy;

    /// <summary>
    /// 敵の攻撃ダメージ
    /// </summary>
    private float Damage_power;

    private void Start()
    {
        Damage_power = enemy.GetComponent<EnemyStatus>().Get_attackPower();
    }

    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// パーティクルがコライダーと接触すると呼ばれる
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    private void OnParticleCollision(GameObject other)
    {
        if (!FlagManager.is_hitPlayer)
        {
            other.GetComponent<PlayerAnim>().p_DamageAnimBranch();
            other.GetComponent<PlayerStatus>().Calculation_damage(Damage_power);
            FlagManager.is_hitPlayer = true;
        }
    }
}