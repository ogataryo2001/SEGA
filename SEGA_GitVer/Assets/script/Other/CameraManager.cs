using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("メインカメラ"), SerializeField]
    GameObject mainCamera;

    [Header("サブカメラ"), SerializeField]
    GameObject subCamera;

    /// <summary>
    /// 回転スピード
    /// </summary>
    private Vector3 rotateSpeed = new Vector3(0.0f, 0.15f, 0.0f);

    /// <summary>
    /// y軸の回転上限
    /// </summary>
    private const float rotateLimit = 0.31f;

    /// <summary>
    /// サブカメラを動かすよう
    /// </summary>
    public void Control_camera()
    {
        // プレイヤーが戦闘開始位置についたら
        if(FlagManager.is_standingPos)
        {
            if (rotateLimit >= subCamera.gameObject.transform.rotation.y)
            {
                subCamera.gameObject.transform.Rotate(rotateSpeed);
            }
            if (rotateLimit <= subCamera.gameObject.transform.rotation.y)
            {
                // 必要なくなったため非表示に
                subCamera.SetActive(false);

                // ゲームスタート前のムービーを流すためのフラグを立てる
                FlagManager.is_startMovie = true;
            }
        }
    }
}