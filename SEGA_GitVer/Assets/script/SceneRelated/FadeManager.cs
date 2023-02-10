using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    /// <summary>
    /// ローディング画面テキスト
    /// </summary>
    [SerializeField] private GameObject NowLoadingText;

    /// <summary>
    /// 透過率
    /// </summary>
    private float alpha = 0.0f;

    /// <summary>
    /// フェードにかかる時間
    /// </summary>
    private float fadeSpeed = 0.1f;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        if(!FlagManager.is_fadeInstance)
        {
            NowLoadingText.SetActive(true);
            DontDestroyOnLoad(this);
            FlagManager.is_fadeInstance = true;
        }
        else // 起動時以外は重複しないようにする
        {
            Destroy(this);
        }
    }


    //-----------------------------------------
    // アップデート
    //-----------------------------------------
    private void FixedUpdate()
    {
        if(FlagManager.is_fadeIn)
        {
            // アルファ値を下げて透過していく
            alpha -= Time.deltaTime / fadeSpeed;
            // 透明になったらフェードイン終了
            if(alpha <= 0.0f)
            {
                FlagManager.is_fadeIn = false;
                alpha = 0.0f;
                NowLoadingText.SetActive(false);
            }
            this.GetComponentInChildren<Image>().color
                = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        if(FlagManager.is_fadeOut)
        {
            // アルファ値をあげて色を上げる
            alpha += Time.deltaTime / fadeSpeed;
            // 真っ暗になったらフェード終了
            if(alpha >= 1.0f)
            {
                FlagManager.is_fadeOut = false;
                alpha = 1.0f;
                NowLoadingText.SetActive(true);
            }
            this.GetComponentInChildren<Image>().color
                 = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }


    /// <summary>
    /// fadeInのためにフラグを立てる関数
    /// </summary>
    public void fadeIn()
    {
        FlagManager.is_fadeIn = true;
        FlagManager.is_fadeOut = false;
    }


    /// <summary>
    /// fadeOutのためにフラグを立てる関数
    /// </summary>
    public void fadeOut()
    {
        FlagManager.is_fadeIn = false;
        FlagManager.is_fadeOut = true;
    }
 }