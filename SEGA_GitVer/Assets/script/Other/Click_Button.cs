using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click_Button : MonoBehaviour
{
    /// <summary>
    /// シーンコントローラーを取得するため
    /// </summary>
    [SerializeField] GameObject mainCamera;

    /// <summary>
    /// シーンを渡すため
    /// </summary>
    private SceneController m_SceneController;

    private ConditionManager m_ShiftMode;

    /// <summary>
    /// 各シーン名の代入
    /// </summary>
    private string[] scenes = 
        new string[] { "TitleScene", "GameScene", "GameClearScene", "GameOverScene" };


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_SceneController = mainCamera.GetComponent<SceneController>();
        m_ShiftMode = GetComponent<ConditionManager>();
    }

    /// <summary>
    /// ポーズボタンを押したときに呼ばれる
    /// </summary>
    public void OnClick_PauseButton()
    {
        FlagManager.is_pause = true;
    }

    /// <summary>
    /// ポーズ終了ボタンを押したときに
    /// </summary>
    public void OnClick_ResumeButton()
    {
        FlagManager.is_pause = false;
    }

    /// <summary>
    /// シーン切り替えのボタンが押されたとき
    /// </summary>
    public void OnClick_SceneButton()
    {
        FlagManager.is_changeScene = true;
    }

    /// <summary>
    /// タイトルへの遷移
    /// </summary>
    public void OnClick_TitleSceneTransition()
    {
        FlagManager.is_changeScene = true;
        FlagManager.is_pause = false;
        m_SceneController.Set_sceneName(scenes[0]);
    }

    /// <summary>
    /// ゲームシーンへの遷移
    /// </summary>
    public void OnClick_GameSceneTransition()
    {
        FlagManager.is_changeScene = true;
        m_SceneController.Set_sceneName(scenes[1]);
    }

    /// <summary>
    /// ゲームクリアシーンへの遷移
    /// </summary>
    public void OnClick_GameClearSceneTransition()
    {
        FlagManager.is_changeScene = true;
        m_SceneController.Set_sceneName(scenes[2]);
    }

    /// <summary>
    /// ゲームオーバーシーンへの遷移
    /// </summary>
    public void OnClick_GameOverSceneTransition()
    {
        FlagManager.is_changeScene = true;
        m_SceneController.Set_sceneName(scenes[3]);
    }

    /// <summary>
    /// ゲーム終了ボタン
    /// </summary>
    public void OnClick_GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


    /// <summary>
    /// アタックモードへ遷移
    /// </summary>
    public void OnClick_AttackMode()
    {
        ConditionManager.playerMode = Condition.attack;
    }

    /// <summary>
    /// ディフェンスモードへ遷移
    /// </summary>
    public void OnClick_DefenseMode()
    {
        ConditionManager.playerMode = Condition.defense;
    }

    /// <summary>
    /// スキルモードへ遷移
    /// </summary>
    public void OnClick_SkillMode()
    {
        ConditionManager.playerMode = Condition.skill;
    }
}