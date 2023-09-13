using UnityEngine;

public class MonsterData : BaseCharacter 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BulletController>().isPlayer)
        {
            Destroy(gameObject);
        }
    }

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
