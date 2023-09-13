using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eSoundType
{
    Player,
    Enemy,
    Other,
}

public class SoundManager : MonoBehaviour
{
    static SoundManager Instance;
    public AudioListener Listener;
    public Transform ListenerTarget;
    public AudioSource[] AudioMixer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Listener.transform.position = ListenerTarget.transform.position;
    }

    public static void PlayAudio(eSoundType type, SoundData data)
    {
        AudioSource s = GetMixer(type);

        s.clip = data.Clip;
        s.volume = data.Volume;
        s.gameObject.transform.position = data.Position;
        s.pitch = Random.Range(data.PitchMin, data.PitchMax);

        s.Play();
    }

    public static AudioSource GetMixer(eSoundType type)
    {
        return Instance.AudioMixer[(int)type];
    }

    /// <summary>
    /// 재생할 오디오에 대한 정보를 담는 클래스
    /// </summary>
    public class SoundData
    {
        /// <summary>
        /// 재생할 오디오에 대한 정보를 담는 클래스
        /// </summary>
        /// <param name="audioClip">재생할 오디오 파일</param>
        /// <param name="position">재생되는 world position</param>
        /// <param name="volume">소리 크기</param>
        public SoundData(AudioClip audioClip, Vector2 position, float volume = 1.0f)
        {
            Clip = audioClip;
            Position = position;
            Volume = volume;
        }
        /// <summary>
        /// 재생할 오디오에 대한 정보를 담는 클래스
        /// </summary>
        /// <param name="audioClip">재생할 오디오 파일</param>
        /// <param name="position">재생되는 world position</param>
        /// <param name="pitchMin">랜덤 소리 높낮이의 낮은 제한</param>
        /// <param name="pitchMax">랜덤 소리 높낮이의 높은 제한</param>
        /// <param name="volume">소리 크기</param>
        public SoundData(AudioClip audioClip, Vector2 position, float pitchMin, float pitchMax, float volume = 1.0f)
        {
            Clip = audioClip;
            Position = position;
            PitchMin = pitchMin;
            PitchMax = pitchMax;
            Volume = volume;
        }
        public AudioClip Clip;
        public Vector2 Position = Vector2.zero;

        public float PitchMin = 1.0f;
        public float PitchMax = 1.0f;

        public float Volume = 1.0f;
    }
}
