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
}
