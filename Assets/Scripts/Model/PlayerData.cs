using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerData : MonoBehaviour, BaseCharacter {
    public string Name { get; set; }
    public string Type { get; set; } // boss, enemy, player
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float Speed { get; set; }
    public int Score { get; set; }
    public bool IsDead { get; set; }
    [SerializeField]
    private Sprite image;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    //BulletData bullet;
    private bool isUnHitTime;
    private bool isHit;

    private void Awake() {
        Name = "Pilot";
        Type = "player";
        Hp = 100;
        Atk = 10;
        Speed = 5;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = image;

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        //bullet = GetComponent<BulletData>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // ��, �� �Ѿ� �±װ� �ٸ��� �������ּ���. ***
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet") {
            // �����ð�
            if (isUnHitTime)
                return;
            // �Ѿ� ���������� �������� ��Ʈ ����
            if (isHit)
                return;

            isHit = true;
            // enemydata Ŭ���� ��������� ���ݷ� �־��ּ���. ***
            //TakeDamage(collision.gameObject.GetComponent<EnemyData>().Atk);
            Invoke("OffDamage", 3f);
            isHit = false;

        } else if (collision.gameObject.tag == "Item") {
            GameObject item = collision.gameObject;
            switch (item.GetComponent<Item>().type) {
                case "bulletTime":
                    Debug.Log("bulletTime");
                    //bullet.Delay /= 2;
                    break;
                case "timeSlow":
                    StartCoroutine(EnemySlow());
                    break;
                case "superPower":
                    Debug.Log("superPower");
                    isUnHitTime = true;
                    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                    Invoke("OffDamage", 5f);
                    isUnHitTime = false;
                    break;
            }
            Destroy(item);
        }
    }

    public void TakeDamage(int damage) {
        Hp -= damage;
        StartCoroutine(DamageEffect());

        if (Hp <= 0) {
            IsDead = true;
            OnDestroy();
        }
    }

    IEnumerator DamageEffect() {
        for (int i = 0; i < 3; i++) {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator EnemySlow() {
        for (int i = 0; i < 15; i++) {
            // �� �±� �ٸ��� �������ּ��� ***
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // �� �Ѿ� �±� �ٸ��� �������ּ��� ***
            GameObject[] bullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
            for (int index = 0; index < enemies.Length; index++) {
                // �� �ӵ� ����
                //enemies[index].GetComponent<EnemyData>().Speed = 0.3f;
            }
            for (int index = 0; index < bullet.Length; index++) {
                // �� źȯ �ӵ� ����
                bullet[index].GetComponent<BulletData>().Speed = 0.6f;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OffDamage() {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void OnDestroy() {
        Destroy(gameObject);
        //��ü ���� �Ǵ� ������� �ִϸ��̼� ����  -> ���߿� �����ߵ�
    }
    //public void Shooting(); <= TopDownShooting�� �����Ϸ�
}
