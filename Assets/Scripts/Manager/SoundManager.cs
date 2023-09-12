using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eSoundType
{
    Player,
    Enemy,
    Other,
    BGM
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


    public class SoundData
    {
        public AudioClip Clip;
        public Vector2 Position = Vector2.zero;

        public float PitchMin = 1.0f;
        public float PitchMax = 1.0f;

        public float Volume = 1.0f;
    }
}
