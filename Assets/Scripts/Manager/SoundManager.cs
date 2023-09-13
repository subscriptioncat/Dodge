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
    /// ����� ������� ���� ������ ��� Ŭ����
    /// </summary>
    public class SoundData
    {
        /// <summary>
        /// ����� ������� ���� ������ ��� Ŭ����
        /// </summary>
        /// <param name="audioClip">����� ����� ����</param>
        /// <param name="position">����Ǵ� world position</param>
        /// <param name="volume">�Ҹ� ũ��</param>
        public SoundData(AudioClip audioClip, Vector2 position, float volume = 1.0f)
        {
            Clip = audioClip;
            Position = position;
            Volume = volume;
        }
        /// <summary>
        /// ����� ������� ���� ������ ��� Ŭ����
        /// </summary>
        /// <param name="audioClip">����� ����� ����</param>
        /// <param name="position">����Ǵ� world position</param>
        /// <param name="pitchMin">���� �Ҹ� �������� ���� ����</param>
        /// <param name="pitchMax">���� �Ҹ� �������� ���� ����</param>
        /// <param name="volume">�Ҹ� ũ��</param>
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
