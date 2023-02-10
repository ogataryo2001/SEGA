using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionButton_emphasis : MonoBehaviour
{
    /// <summary>
    /// �U���{�^���̍��W
    /// </summary>
    Vector3 attackButtonPos     = new Vector3(-300,  -100,   0);

    /// <summary>
    /// �h��{�^���̍��W
    /// </summary>
    Vector3 defenseButtonPos    = new Vector3(0,     -100,   0);

    /// <summary>
    /// �X�L���{�^���̍��W
    /// </summary>
    Vector3 skillButtonPos      = new Vector3(300,   -100,   0);

    /// <summary>
    /// �X�^�[�̉�]���x���x
    /// </summary>
    Vector3 starRotateSpeed     = new Vector3(0,     0,      1);

    /// <summary>
    /// �X�^�[�̊g��k�����x
    /// </summary>
    Vector3 starScaleSpeed      = new Vector3(0.01f,  0.01f,   0.01f);

    /// <summary>
    /// �X�^�[�̍ő�g��l
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