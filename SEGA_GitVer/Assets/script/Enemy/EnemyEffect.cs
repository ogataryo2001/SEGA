using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    /// <summary>
    /// エフェクトpower06
    /// </summary>
   [SerializeField] GameObject effect_power06;

    /// <summary>
    /// エフェクトline06
    /// </summary>
    [SerializeField] GameObject effect_line06;


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// line06の再生
    /// </summary>
    private void Invoke_line06()
    {
        effect_line06.GetComponent<ParticleSystem>().Play();
    }

    /// <summary>
    /// line06の停止
    /// </summary>
    private void Release_line06()
    {
        effect_line06.GetComponent<ParticleSystem>().Stop();
    }


    /// <summary>
    /// power06の再生
    /// </summary>
    private void Invoke_power06()
    {
        FlagManager.is_hitPlayer = false;
        effect_power06.GetComponent<ParticleSystem>().Play();
    }
}
