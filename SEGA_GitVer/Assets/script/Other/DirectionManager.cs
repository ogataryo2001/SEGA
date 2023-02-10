using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManager : MonoBehaviour
{
    
    [Header("キャラ"), SerializeField]
    GameObject character;

    [Header("カットインキャンバス"), SerializeField]
    GameObject CutinCanvas;

    [Header("映像に集中させるための背景表示"), SerializeField]
    GameObject BlackOutPanel;

    /// <summary>
    /// カメラ切り替えよう
    /// </summary>
    private CameraManager m_CameraManager;

    /// <summary>
    /// 敵の登場演出するため
    /// </summary>
    private CutinManager m_CutinManager;

    /// <summary>
    /// キャラクターの立ち位置(ｘ座標のみ
    /// </summary>
    private const float standingPositionX = -1.8f;

    /// <summary>
    /// 移動スピード
    /// </summary>
    private Vector3 moveSpeed = new Vector3(0.0f, 0.0f, 0.05f);


    private void Start()
    {
        m_CameraManager = gameObject.GetComponent<CameraManager>();
        m_CutinManager = CutinCanvas.GetComponent<CutinManager>();
    }


    private void FixedUpdate()
    {
        Control_character();
        m_CameraManager.Control_camera();

        if(FlagManager.is_startMovie)
        {
            m_CutinManager.EnemyCutin();
            FlagManager.is_startMovie = false;
            enabled = false;
        }
    }


    /// <summary>
    /// 演出でキャラを動かすよう
    /// </summary>
    private void Control_character()
    {
        if (character.transform.position.x < standingPositionX)
        {
            character.transform.Translate(moveSpeed);
        }
        else
        {
            FlagManager.is_standingPos = true;
        }
    }
}