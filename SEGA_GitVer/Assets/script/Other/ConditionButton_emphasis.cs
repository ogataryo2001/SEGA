using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionButton_emphasis : MonoBehaviour
{
    /// <summary>
    /// 攻撃ボタンの座標
    /// </summary>
    Vector3 attackButtonPos     = new Vector3(-300,  -100,   0);

    /// <summary>
    /// 防御ボタンの座標
    /// </summary>
    Vector3 defenseButtonPos    = new Vector3(0,     -100,   0);

    /// <summary>
    /// スキルボタンの座標
    /// </summary>
    Vector3 skillButtonPos      = new Vector3(300,   -100,   0);

    /// <summary>
    /// スターの回転速度速度
    /// </summary>
    Vector3 starRotateSpeed     = new Vector3(0,     0,      1);

    /// <summary>
    /// スターの拡大縮小速度
    /// </summary>
    Vector3 starScaleSpeed      = new Vector3(0.01f,  0.01f,   0.01f);

    /// <summary>
    /// スターの最大拡大値
    /// </summary>
    Vector3 starScaleMax        = new Vector3(1.2f,  1.2f,   1.2f);

    private bool is_change = false;

    private void Update()
    {
        switch (ConditionManager.playerMode)
        {
            case Condition.attack:
                gameObject.transform.localPosition = attackButtonPos;
                break;
            case Condition.defense:
                gameObject.transform.localPosition = defenseButtonPos;
                break;
            case Condition.skill:
                gameObject.transform.localPosition = skillButtonPos;
                break;
        }

        gameObject.transform.Rotate(starRotateSpeed);
        if (gameObject.transform.localScale.x <= starScaleMax.x && !is_change)
        {
            gameObject.transform.localScale += starScaleSpeed;
        }
        else
        {
            is_change = true;
        }

        if(gameObject.transform.localScale.x >= Vector3.zero.x && is_change)
        {
            gameObject.transform.localScale -= starScaleSpeed;

        }
        else
        {
            is_change = false;
        }
    }
}