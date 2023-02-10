using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundSE : MonoBehaviour
{
    /// <summary>
    /// オーディオソース
    /// </summary>
    [SerializeField] AudioSource m_AudioSource;

    /// <summary>
    /// オーディオクリップ
    /// </summary>
    [SerializeField] AudioClip m_AudioClip;


    //-----------------------------------------
    // スタート
    //-----------------------------------------
    private void Start()
    {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
    }


    /// <summary>
    /// 指定のサウンドの再生
    /// </summary>
    public void OnPlaySounds()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
    }
}
