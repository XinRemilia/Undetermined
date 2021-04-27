using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSourceManager : MonoBehaviour
{
    public static cSourceManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;
    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 足音
    /// </summary>
    public void MoveAudio()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
}
