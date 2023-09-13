using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private static BGMPlayer Instance;
    private AudioSource m_AudioSource;
    public AudioClip[] AudioClips;
    [SerializeField] private int m_NowClipIndex;
    [SerializeField] private int m_NextClipIndex;
    [SerializeField] private bool m_StopFlag = false;
    [SerializeField] private bool m_PlayFlag = true;
    public bool TEST;
    public int StartBGMIndex;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        m_AudioSource.Stop();
        PlayBGM(StartBGMIndex);
    }

    private void FixedUpdate()
    {
        if (m_StopFlag)
            SlowlyStop();
        else if (m_PlayFlag && m_AudioSource.isPlaying)
            SlowlyPlay();
    }

    private void SlowlyStop()
    {
        if (m_AudioSource.volume <= (DataManager.Instance?.BGMVolume??0.5f) * 0.2f)
        {
            if (Instance.TEST)
                Debug.Log($"Slowly Stop BGM {m_NowClipIndex}");
            m_AudioSource.Stop();
            BGMPlayer.PlayBGM(m_NextClipIndex);
            m_PlayFlag = true;
            m_StopFlag = false;
        }
        else
        {
            m_AudioSource.volume -= 0.01f;
        }
    }

    private void SlowlyPlay()
    {
        if (m_AudioSource.volume > (DataManager.Instance?.BGMVolume??0.5f) * 0.9f)
        {
            m_AudioSource.volume = DataManager.Instance?.BGMVolume??0.5f;
            m_PlayFlag = false;
            if (Instance.TEST)
                Debug.Log($"Slowly Play BGM {m_NowClipIndex}");
        }
        else
        {
            m_AudioSource.volume += 0.01f;
        }
    }

    /// <summary>
    /// BGM 재생 시작
    /// </summary>
    /// <param name="index">0 : 밝은 BGM, 1 : 매드무비 BGM</param>
    public static void PlayBGM(int index)
    {
        if (Instance.m_AudioSource.isPlaying)
        {
            StopBGM();
            Instance.m_NextClipIndex = index;
            return;
        }
        else
        {
            Instance.m_PlayFlag = true;
        }

        if (index >= Instance.AudioClips.Length)
            index = 0;
        else if (index < 0)
            index = Instance.AudioClips.Length - 1;

        Instance.m_AudioSource.clip = Instance.AudioClips[index];
        Instance.m_NowClipIndex = index;
        Instance.m_AudioSource.volume = 0.0f;
        Instance.m_AudioSource.Play();
        if (Instance.TEST)
            Debug.Log($"Start Play BGM {index}");
    }
    public static void PauseBGM()
    {
        Instance.m_AudioSource.Pause();
    }
    public static void UnpauseBGM()
    {
        Instance.m_AudioSource.UnPause();
    }
    public static void StopBGM()
    {
        Instance.m_StopFlag = true;
        if (Instance.TEST)
            Debug.Log($"Stop Play BGM {Instance.m_NowClipIndex}");
    }
    public static void NextBGM()
    {
        BGMPlayer.PlayBGM(Instance.m_NowClipIndex + 1);
    }
    public static void PrevBGM()
    {
        BGMPlayer.PlayBGM(Instance.m_NowClipIndex - 1);
    }
}
