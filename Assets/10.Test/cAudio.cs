using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cAudio : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource seAudioSource;
    public Scrollbar bgm;
    public Scrollbar se;
    public Toggle bgmT;
    public Toggle seT;
    public void Bgm(float _value)
    {
        bgmAudioSource.volume = _value;
        if (_value > 0)
        {
            bgmT.isOn = true;
        }
    }

    public void Se(float _value)
    {
        seAudioSource.volume = _value;
        if (_value > 0)
        {
            seT.isOn = true;
        }
    }

    public void SetBgm(bool _isOn)
    {
        bgmAudioSource.volume = 0;
        bgm.value = 0;
    }

    public void SetSe(bool _isOn)
    {
        seAudioSource.volume = 0;
        se.value = 0;
    }
}
