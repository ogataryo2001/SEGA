using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingLines : MonoBehaviour
{
    /// <summary>
    /// ラインレンダラークラス
    /// </summary>
    private LineRenderer m_lineRenderer;

    /// <summary>
    /// 当たり判定クラス
    /// </summary>
    private CollisionDetection m_CollisionDetection;

    /// <summary>
    /// 道順の受け取りリスト
    /// </summary>
    private List<GameObject> route;

    /// <summary>
    /// 線のセット
    /// </summary>
    private Vector3[] lineSetPos = new Vector3[20];

    /// <summary>
    /// 線の通った点を数えるカウントのリセット
    /// </summary>
    private const int Reset_linePositionCount = 0;

    /// <summary>
    /// z座標の固定のための定数
    /// </summary>
    private const float posZ = 1.0f;

    //-----------------------------------------
    // スタート
    //-----------------------------------------
    void Start()
    {
        // 動的確保
        m_lineRenderer = GetComponent<LineRenderer>();
        m_CollisionDetection = gameObject.GetComponent<CollisionDetection>();

        // 線の太さの設定
        m_lineRenderer.startWidth = 0.15f;
        m_lineRenderer.endWidth = 0.15f;


        // 初期化
        route = new List<GameObject>();
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 線の描画
    /// </summary>
    public void Drawing_Line()
    {
        m_lineRenderer.positionCount = Reset_linePositionCount;
        route = m_CollisionDetection.Get_TransitPoint();
        // ラインレンダラーで描画するためにポジションだけを格納する
        for (int i = 0; i < route.Count; i++)
        {
            m_lineRenderer.positionCount++;
            var position = route[i].gameObject.transform.position;
            // Canvasに座標を合わせる
            position.z = posZ;
            lineSetPos[i] = position;
        }

        // 線を描くためのポジションのセット
        m_lineRenderer.SetPositions(lineSetPos);

        // 最後の点からマウスまでの直線の描画
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Canvasに座標を合わせる
        pos.z = posZ;
        m_lineRenderer.positionCount++;
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, pos);
    }

    /// <summary>
    /// リストクリア関数
    /// </summary>
    public void Clear_route()
    {
        route.Clear();
        m_lineRenderer.positionCount = Reset_linePositionCount;
    }
}
