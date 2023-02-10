using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowEvent : MonoBehaviour
{
    /// <summary>
    /// キャラクター
    /// </summary>
    [SerializeField] GameObject Character;

    /// <summary>
    /// 矢の着弾
    /// </summary>
    [SerializeField] ParticleSystem allowHit;

    /// <summary>
    /// プレイヤーのステータス取得用
    /// </summary>
    private PlayerStatus m_PlayerStatus;

    /// <summary>
    /// リセット用のスタートポジション
    /// </summary>
    private Vector3 startPosition;

    /// <summary>
    /// スタート時の回転率（初期化用
    /// </summary>
    private Quaternion startRotation;

    /// <summary>
    /// 矢の速度を早くするため（デルタタイムで計算しているから
    /// </summary>
    private const float allowTwiceSpeed = 7.0f;

    /// <summary>
    /// x方向への最大移動位置
    /// </summary>
    private const float positionXLimit = 3.0f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_PlayerStatus = Character.GetComponent<PlayerStatus>();

        // 初期座標の取得,初期化に使用
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        // x軸方向へ飛ばす
        gameObject.transform.position =
            new Vector3(gameObject.transform.position.x + Time.deltaTime * allowTwiceSpeed,
            gameObject.transform.position.y ,
            gameObject.transform.position.z);

        if(gameObject.transform.position.x > positionXLimit)
        {
            gameObject.SetActive(false);
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;
            FlagManager.is_allowInstance = false;
            enabled = false;
        }
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// 敵に当たったらダメージ計算
    /// </summary>
    /// <param name="collision">当たり判定</param>
    private void OnCollisionEnter(Collision collision)
    {

        collision.gameObject.
            GetComponent<EnemyStatus>().
            Calculation_damage(m_PlayerStatus.p_nomalAttackDamage());
        allowHit.Play();

        // 矢を削除
        if (FlagManager.is_allowInstance)
        {
            gameObject.SetActive(false);
            gameObject.transform.position = startPosition;
            gameObject.transform.rotation = startRotation;
            FlagManager.is_allowInstance = false;
            enabled = false;
        }
    }
}