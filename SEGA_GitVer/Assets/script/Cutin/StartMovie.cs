using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMovie : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Start_Movie());
    }


    IEnumerator Start_Movie()
    {
        yield return new WaitForSeconds(3.0f);

        Debug.Log("a");

        // 動画を再生
        gameObject.GetComponent<VideoPlayer>().Play();
        yield break;
    }
}