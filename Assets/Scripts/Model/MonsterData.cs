using UnityEngine;

public class MonsterData : BaseCharacter
{
    private CharacterAudio m_Audio;

    private void Awake()
    {
        m_Audio = this.gameObject.GetComponent<CharacterAudio>();
    }
    public override void TakeDamage(int player, int damage)
    {
        base.TakeDamage(player, damage);
        m_Audio.PlayHit();
    }
}
