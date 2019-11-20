using UnityEngine;
using UnityEngine.Video;
using VideoPlayer = UnityEngine.Video.VideoPlayer;

public class ShowVideo : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private GameObject loadingImage;
    [SerializeField] private GameObject showVideo;
    [Header("CONFIG")]
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private string urlVideo;
    [SerializeField] private bool pauseOnLost;
    [SerializeField] private bool loopVideo;

    private VideoPlayer videoPlayer;
    private static double timePause;

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Awake()
    {
        videoPlayer = FindObjectOfType<VideoPlayer>();
    }

    private void Start()
    {
        videoPlayer.started += VideoPlayer_started;
        BufferVideoAR.Instance.OnGetVideo += OnGetVideoViaBuffer;

        if (showVideo.activeInHierarchy)
        {
            loadingImage.SetActive(true);
        }

        if (videoPlayer.source == VideoSource.Url)
        {
            BufferVideoAR.Instance.GetVideoForBuffer(urlVideo);
        }
    }

    private void OnEnable()
    {
        print("OnTargetFound");
        videoPlayer.source = videoClip != null ? VideoSource.VideoClip : VideoSource.Url;
        videoPlayer.isLooping = loopVideo;

        if(videoPlayer.source == VideoSource.Url)
        {
            BufferVideoAR.Instance.GetVideoForBuffer(urlVideo);
        }
        else
        {
            videoPlayer.clip = videoClip;
            videoPlayer.Play();
        }
    }
    private void OnDisable()
    {
        print("OnTargetLost");

        if (pauseOnLost)
        {
            timePause = videoPlayer.time;
            videoPlayer.Pause();
            print("Pause");
        }
        else
        {
            videoPlayer.Stop();
            loadingImage.SetActive(true);
            print("Stop");
        }
    }

    #endregion

    #region PRIVATE_METHODS

    private void VideoPlayer_started(VideoPlayer source)
    {
        loadingImage.SetActive(false);
    }

    private void OnGetVideoViaBuffer(bool error, string response)
    {
        if (error)
        {
            Debug.LogError("OnGetVideoViaBuffer error:: " + response);
        }
        else
        {
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = response;
            videoPlayer.Play();

            if (pauseOnLost)
            {
                videoPlayer.time = timePause;
            }
        }
    }

    #endregion
}