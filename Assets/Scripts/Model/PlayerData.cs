using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour, BaseCharacter
{
    public string Name { get; set; }
    public string Type { get; set; } // boss, enemy, player
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float AtkSpeed { get; set; }
    public float Speed { get; set; }
    public int Score { get; set; }
    public bool IsDead { get; set; }
    [SerializeField]
    private Sprite image;
    public Sprite Image { get { return image; } }

    public PlayerData() { }
    public PlayerData(string _name, string _type, int _hp, int _atk, float _atkSpeed, float _speed, Sprite _image) {
        Name = _name;
        Type = _type;
        Hp = _hp;
        Atk = _atk;
        AtkSpeed = _atkSpeed;
        Speed = _speed;
        Score = 0;
        IsDead = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _image;
    }

    void Start()
    {
        if (Name == null)
            Name = "Player";
        if (Type == null)
            Type = "player";
        if (Hp <= 0)
            Hp = 100;
        if (Atk <= 0)
            Atk = 10;
        if (AtkSpeed <= 0)
            AtkSpeed = 2;
        if (Speed <= 0)
            Speed = 5;
        if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == null)
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = image;
    }

    public void TakeDamage(int damage) {
        Hp -= damage;
        if (Hp <= 0) {
            IsDead = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //기체 폭팔 또는 사라지는 애니메이션 실행  -> 나중에 만들어야됨
    }
    //public void Shooting(); <= TopDownShooting에 구현완료
}
