using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{

    ///---------------------------------------------
    /// player関連
    ///---------------------------------------------


    /// <summary>
    /// プレイヤーの立つポジションに立ったか
    /// </summary>
    public static bool is_standingPos = false;

    /// <summary>
    /// プレイヤーが動き終わったか
    /// </summary>
    public static bool is_endOfAction = false;

    /// <summary>
    /// プレイヤー行動可能か
    /// </summary>
    public static bool is_playerAction = false;

    /// <summary>
    /// ギアバーストループは立っているか
    /// </summary>
    public static bool is_playerGearBurstLoop = false;

    /// <summary>
    /// プレイヤーは死んだか
    /// </summary>
    public static bool is_playerDeath = false;

    /// <summary>
    /// 初めてドットに触れたか
    /// </summary>
    public static bool is_firstTouchDot = false;

    /// <summary>
    /// 一通りの処理が終わったか
    /// </summary>
    public static bool is_processAsAWhole = false;

    /// <summary>
    /// 矢は生成されたか
    /// </summary>
    public static bool is_allowInstance = false;

    /// <summary>
    /// スキル準備されたか
    /// </summary>
    public static bool is_skillReady = false;


    ///---------------------------------------------
    /// enemy関連
    ///---------------------------------------------


    /// <summary>
    /// エネミーは死んだか
    /// </summary>
    public static bool is_enemyDead = false;

    /// <summary>
    /// プレイヤーに当たったか
    /// </summary>
    public static bool is_hitPlayer = false;

    /// <summary>
    /// スキルは成功したか
    /// </summary>
    public static bool is_skillJudgment = false;

    /// <summary>
    /// お邪魔スキルは発動しているか（敵
    /// </summary>
    public static bool is_enemyObstacle = false;


    ///---------------------------------------------
    /// 切り替え関連
    ///---------------------------------------------


    /// <summary>
    /// 演出が終わったか（trueで終了
    /// </summary>
    public static bool is_direction = false;

    /// <summary>
    /// ポーズの時間になったか
    /// </summary>
    public static bool is_pause = false;

    /// <summary>
    /// シーンの切り替え可能か
    /// </summary>
    public static bool is_changeScene = false;

    /// <summary>
    /// コンテニューするか
    /// </summary>
    public static bool is_continue = false;


    //-----------------------------------------
    // other
    //-----------------------------------------

    /// <summary>
    /// 最初のムービーが再生されたか
    /// </summary>
    public static bool is_startMovie = false;

    /// <summary>
    /// パターンは表示されているか
    /// </summary>
    public static bool is_pattern = false;

    /// <summary>
    /// ゲームシーンに切り替わって最初か
    /// </summary>
    public static bool is_firstGameScene = false;



    //-----------------------------------------
    // フェイド関連
    //-----------------------------------------

    /// <summary>
    /// フェイドをインスタンス化するか
    /// </summary>
    public static bool is_fadeInstance = false;

    /// <summary>
    /// フェードインするフラグ
    /// </summary>
    public static bool is_fadeIn = false;

    /// <summary>
    /// フェードアウトするフラグ
    /// </summary>
    public static bool is_fadeOut = false;



    /// <summary>
    /// 全フラグ初期化関数
    /// </summary>
    public void Initialization()
    {
        is_standingPos = false;
        is_playerAction = false;
        is_playerGearBurstLoop = false;
        is_playerDeath = false;
        is_endOfAction = false;
        is_processAsAWhole = false;
        is_firstTouchDot = false;
        is_allowInstance = false;
        is_skillReady = false;


        is_enemyDead = false;
        is_hitPlayer = false;
        is_skillJudgment = false;
        is_enemyObstacle = false;


        is_direction = false;
        is_pause = false;
        is_changeScene = false;
        is_continue = false;

        is_startMovie = false;
        is_pattern = false;
        is_firstGameScene = false;
    }
}
