using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public GameObject objectToActivate;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer player)
    {
        gameObject.SetActive(false);
        objectToActivate.SetActive(true);
    }
}
