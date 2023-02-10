using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各状態
/// </summary>
public enum Condition
{
    idle,
    attack,
    defense,
    guard,
    skill,
};


public class ConditionManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの状態保存変数
    /// </summary>
    public static Condition playerMode = Condition.attack;

    /// <summary>
    /// 敵の状態保存偏す
    /// </summary>
    public static Condition enemyMode = Condition.idle;

}