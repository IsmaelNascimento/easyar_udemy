using easyar;
using UnityEngine;
using VideoPlayer = UnityEngine.Video.VideoPlayer;

public class ShowVideoManager : MonoBehaviour
{
    [SerializeField] private ImageTargetController imageTargetController;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject loadingImage;
    [SerializeField] private GameObject showVideo;
    [SerializeField] private bool pauseOnLost;

    private void Start()
    {
        imageTargetController.OnTargetFound += OnTargetFound;
        imageTargetController.OnTargetLost += OnTargetLost;
        videoPlayer.started += VideoPlayer_started;
        BufferVideoAR.Instance.OnGetVideo += OnGetVideoViaBuffer;

        if (showVideo.activeInHierarchy)
        {
            loadingImage.SetActive(true);
        }
    }

    private void VideoPlayer_started(VideoPlayer source)
    {
        print("VideoPlayer_started");
        loadingImage.SetActive(false);
    }

    private void OnTargetFound(Target target)
    {
        print("OnTargetFound");

        if (pauseOnLost && !string.IsNullOrEmpty(videoPlayer.url))
        {
            videoPlayer.Play();
        }
        else if (string.IsNullOrEmpty(videoPlayer.url))
        {
            videoPlayer.Play();
        }
        else
        {
            BufferVideoAR.Instance.GetVideoForBuffer(videoPlayer.url);
        }
    }

    private void OnTargetLost(Target target)
    {
        print("OnTargetLost");

        if (pauseOnLost)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Stop();
            loadingImage.SetActive(true);
        }
    }

    private void OnGetVideoViaBuffer(bool error, string response)
    {
        if (error)
        {
            Debug.LogError("OnGetVideoViaBuffer error:: " + response);
        }
        else
        {
            videoPlayer.url = response;
            videoPlayer.Play();
        }
    }
}
