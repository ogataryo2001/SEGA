using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 今のシーン
    /// </summary>
    private string nextScene;

    /// <summary>
    /// シーンアウトのためのパネル取得
    /// </summary>
    public GameObject fadeCanvas;

    /// <summary>
    /// 停止時間 3
    /// </summary>
    private const float delayTime = 3.0f;

    /// <summary>
    /// フラグマネージャーを初期化するため
    /// </summary>
    private FlagManager m_FlagManager;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    void Awake()
    {
        m_FlagManager = GetComponent<FlagManager>();
        // フェイドパネルが生成されていないなら
        if(!FlagManager.is_fadeInstance)
        {
            Instantiate(fadeCanvas);
        }
        // 少し遅延をかけて関数呼び出し
        Invoke("findFadeObject", 0.01f);

        // 初期化
        nextScene = "TitleScene";
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void Update()
    {
        // シーンチェンジフラグが立っているなら
        if(FlagManager.is_changeScene)
        {
            Time.timeScale = 1;

            sceneChange(Get_sceneName());
        }
    }



    //-----------------------------------------
    // 各関数
    //-----------------------------------------


    /// <summary>
    /// オブジェクトの取得とフェードインの開始
    /// </summary>
    private void findFadeObject()
    {
        // Canvasを取得
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
        // fadeinフラグを立てる
        fadeCanvas.GetComponent<FadeManager>().fadeIn();
    }


    /// <summary>
    /// フェードアウトの開始とシーンの切り替え
    /// </summary>
    /// <param name="sceneName"></param>
    public void sceneChange(string sceneName)
    {
        // フェードアウトフラグを立てる
        fadeCanvas.GetComponent<FadeManager>().fadeOut();

        // フラグを下げる
        m_FlagManager.Initialization();
        

        // コルーチンのスタート
        StartCoroutine(DelayMethod_LoadScene(delayTime, sceneName));
    }


    /// <summary>
    /// 次シーンの取得関数
    /// </summary>
    /// <returns>nextScene</returns>
    private string Get_sceneName()
    {
        return nextScene;
    }


    /// <summary>
    /// 次のシーンへのセット関数
    /// </summary>
    /// <param name="SceneName">次に移動したいシーン名</param>
    public void Set_sceneName(string SceneName)
    {
        nextScene = SceneName;
    }


    /// <summary>
    /// コルーチンで処理を待機してから実行
    /// </summary>
    /// <param name="delay">待機秒数</param>
    /// <param name="sceneName">移りたいシーン</param>
    /// <returns></returns>
    IEnumerator DelayMethod_LoadScene(float delay, string sceneName)
    {

        yield return new WaitForSeconds(delay);

        // シーンのロード
        SceneManager.LoadSceneAsync(sceneName);

        yield break;
    }
}