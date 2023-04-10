using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    [SerializeField] private string url;

    private VideoPlayer _videoplayer;
    // Start is called before the first frame update
    void Start()
    {
        _videoplayer = GetComponent<VideoPlayer>();
        _videoplayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "NewIntro.mp4");
        _videoplayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
