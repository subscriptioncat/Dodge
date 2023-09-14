using UnityEngine;

public class MonsterData : BaseCharacter 
{
    public GameObject superPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BulletController>().isPlayer)
        {
            int ran = Random.Range(0, 10);
            if (ran < 9)
                return;
            else if (ran < 10)
                Instantiate(superPower, transform.position, superPower.transform.rotation);
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
