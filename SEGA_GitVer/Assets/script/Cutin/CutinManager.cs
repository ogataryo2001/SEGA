using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutinManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤーのカットインパネル
    /// </summary>
    [SerializeField] GameObject PlayerCutinPanel;

    /// <summary>
    /// 敵のカットインパネル
    /// </summary>
    [SerializeField] GameObject EnemyCutinPanel;

    /// <summary>
    /// カットイン用のバックグラウンドパネル
    /// </summary>
    [SerializeField] GameObject CutinBackGround;

    /// <summary>
    /// 映像管理用
    /// </summary>
    private Dictionary<string, VideoPlayer> m_VideoPlayer;

    /// <summary>
    /// CutinPanelのアニメーション管理用
    /// </summary>
    private Dictionary<string, Animator> m_Animator;

    /// <summary>
    /// 動画時間
    /// </summary>
    private double movieLength;

    /// <summary>
    /// 終了処理待ち時間
    /// </summary>
    private const float endWaitTime = 2.0f;

    /// <summary>
    /// 時間経過速度（デフォルト
    /// </summary>
    private const float timeSpeed = 1.0f;

    /// <summary>
    /// 時間経過速度(zero
    /// </summary>
    private const float timeSpeed_zero = 0.0f;

    /// <summary>
    /// プレイヤーならture、敵ならfalse
    /// </summary>
    private bool is_which;

    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        // キャッシュ
        m_VideoPlayer = new Dictionary<string, VideoPlayer>
        {
            {"Player", PlayerCutinPanel.GetComponentInChildren<VideoPlayer>() },
            {"Enemy", EnemyCutinPanel.GetComponentInChildren<VideoPlayer>() }
        };

        m_Animator = new Dictionary<string, Animator>
        {
            {"Player", PlayerCutinPanel.GetComponentInChildren<Animator>()},
            {"Enemy", EnemyCutinPanel.GetComponentInChildren<Animator>() }
        };
    }


    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// プレイヤー用のカットイン
    /// </summary>
    public void PlayerCutin()
    {
        is_which = true;
        Time.timeScale = timeSpeed_zero;

        PlayerCutinPanel.SetActive(true);
        CutinBackGround.SetActive(true);
        m_Animator["Player"].Play("PlayerCutinStart");
        m_Animator["Player"].SetBool("EndCutin", false);
        m_VideoPlayer["Player"].Play();
        movieLength = m_VideoPlayer["Player"].length;
        StartCoroutine(EndCutin((float)movieLength));    
    }

    /// <summary>
    /// 敵用のカットイン
    /// </summary>
    public void EnemyCutin()
    {
        is_which = false;
        Time.timeScale = timeSpeed_zero;

        EnemyCutinPanel.SetActive(true);
        CutinBackGround.SetActive(true);
        m_Animator["Enemy"].Play("CutinPanel");
        m_Animator["Enemy"].SetBool("EndCutin", false);
        m_VideoPlayer["Enemy"].Play();
        movieLength = m_VideoPlayer["Enemy"].length;
        StartCoroutine(EndCutin((float)movieLength));
    }


    /// <summary>
    /// 終了カットイン再生用
    /// </summary>
    /// <param name="waitTime">動画の時間</param>
    /// <returns>動画時間分の待機</returns>
    IEnumerator EndCutin(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if(is_which)
        {
            m_Animator["Player"].SetBool("EndCutin", true);
        }
        else
        {
            m_Animator["Enemy"].SetBool("EndCutin", true);
        }

        yield return End_all(endWaitTime);
    }

    /// <summary>
    /// カットイン関連の終了処理
    /// </summary>
    /// <param name="waitTime">２秒</param>
    /// <returns>２秒の待機時間</returns>
    IEnumerator End_all(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Time.timeScale = timeSpeed;
        FlagManager.is_direction = true;

        PlayerCutinPanel.SetActive(false);
        EnemyCutinPanel.SetActive(false);
        CutinBackGround.SetActive(false);

        yield break;
    }
}