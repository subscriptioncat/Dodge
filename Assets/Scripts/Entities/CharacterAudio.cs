using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public AudioClip FireClip;
    public AudioClip HitClip;
    public bool TEST;
    private float m_Time;
    private TopDownCharacterController m_Controller;

    private void Awake()
    {
        m_Controller = GetComponent<TopDownCharacterController>();
    }
    private void Start()
    {
        if (TEST)
            BGMPlayer.PlayBGM(0);
        if (m_Controller != null)
        {
            m_Controller.OnFireEvent += PlayFire;
        }
    }
    private void Update()
    {
        if (TEST)
            Test();
    }

    public void PlayFire(Vector2 NotUse)
    {
        PlayFire();
    }
    public void PlayFire()
    {
        SoundManager.PlayAudio(eSoundType.Player, new SoundManager.SoundData(FireClip, (Vector2)transform.position, 0.9f, 1.1f));
    }
    public void PlayHit()
    {
        SoundManager.PlayAudio(eSoundType.Player, new SoundManager.SoundData(HitClip, (Vector2)transform.position, 0.8f, 1.2f));
    }
    private void Test()
    {
        m_Time += Time.deltaTime;
        if (m_Time > 0.1f)
        {
            PlayFire();
            //PlayHit();
            m_Time = 0.0f;
        }
    }
}
