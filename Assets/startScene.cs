using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool isLoading;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        videoPlayer.loopPointReached += finishgame;
    }
    void finishgame(VideoPlayer player)
    {
        //Endvideo();
        if (!isLoading)
        {
            Application.Quit();
        }
    }
}
