using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    /// <summary>
    /// メインのキャンバス
    /// </summary>
    [SerializeField] GameObject mainCanvas;

    /// <summary>
    /// コンディション切り替えボタンがあるキャンバス
    /// </summary>
    [SerializeField] GameObject shiftCanvas;

    /// <summary>
    /// ポーズ専用パネル
    /// </summary>
    [SerializeField] GameObject pausePanel;

    /// <summary>
    /// ポーズ専用のボタン
    /// </summary>
    [SerializeField] GameObject pauseButton;

    /// <summary>
    /// 再開用のボタン
    /// </summary>
    [SerializeField] GameObject resumeButton;

    /// <summary>
    /// タイトルへの遷移ボタン
    /// </summary>
    [SerializeField] GameObject titleTransitionButton;

    /// <summary>
    /// ゲーム終了ボタン
    /// </summary>
    [SerializeField] GameObject gameEndButton;

    /// <summary>
    /// 停止時間
    /// </summary>
    private const float stopTime = 0.0f;

    /// <summary>
    /// ｹﾞｰﾑ進行時間
    /// </summary>
    private const float timeSpeed = 1.0f;



    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        titleTransitionButton.SetActive(false);
        gameEndButton.SetActive(false);
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        // ポーズ可能になったら一時停止
        if (FlagManager.is_pause)
        {
            mainCanvas.SetActive(false);
            shiftCanvas.SetActive(false);
            // 停止ボタンの非アクティブ化
            pauseButton.SetActive(false);
            // フィルター用のパネルのアクティブ化
            pausePanel.SetActive(true);
            // ポーズ解除ボタンのアクティブ化
            resumeButton.SetActive(true);
            // タイトルへの遷移ボタンのアクティブ化
            titleTransitionButton.SetActive(true);
            // ゲーム終了ボタンのアクティブ化
            gameEndButton.SetActive(true);

            // 描画物の削除
            FlagManager.is_firstTouchDot = false;
            mainCanvas.GetComponent<CollisionDetection>().Clear_positionOfPassage();
            mainCanvas.GetComponent<CorrectRouteDetermination>().Clear_transitPointStorage();
            mainCanvas.GetComponent<DrawingLines>().Clear_route();


            // 一時停止へ
            Time.timeScale = stopTime;
        }
        else if (!FlagManager.is_firstGameScene)
        {
            // 初期化
            mainCanvas.SetActive(true);
            shiftCanvas.SetActive(true);
            pauseButton.SetActive(true);
            pausePanel.SetActive(false);
            resumeButton.SetActive(false);
            titleTransitionButton.SetActive(false);
            gameEndButton.SetActive(false);
            Time.timeScale = timeSpeed;
        }
    }
}