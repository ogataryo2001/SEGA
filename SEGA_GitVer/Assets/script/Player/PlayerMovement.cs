using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// 当たり判定の管理クラス
    /// </summary>
    private CollisionDetection m_CollisionDetection;

    /// <summary>
    /// 線の描画クラス
    /// </summary>
    private DrawingLines m_DrawingLines;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Awake()
    {
        // 動的確保
        m_CollisionDetection = gameObject.GetComponent < CollisionDetection>();
        m_DrawingLines = gameObject.GetComponent<DrawingLines>();
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        Move();
        Draw();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// プレイヤーの動き
    /// </summary>
    private void Move()
    {
        // 押している間かつ一通りの処理が終わっているもしくは始まっていないなら
        if (Input.GetMouseButton(0) && !FlagManager.is_processAsAWhole)
        {
            m_CollisionDetection.Drawing_Decision();
        }
        // マウスボタンが話されたときかつ、最初に点をタッチしていれば
        else if(Input.GetMouseButtonUp(0) && FlagManager.is_firstTouchDot)
        {
            FlagManager.is_endOfAction = true;
            FlagManager.is_processAsAWhole = true;
        }
    }

    /// <summary>
    /// プレイヤーの動き関する描画
    /// </summary>
    private void Draw()
    {
        if (!FlagManager.is_processAsAWhole)
        {
            m_DrawingLines.Drawing_Line();
        }
    }
}