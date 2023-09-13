using UnityEngine;

public class MonsterData : BaseCharacter
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float AtkSpeed { get; set; }
    public float Speed { get; set; }
    public bool IsDead { get; set; }

    public void TakeDamage(int damage) { }
}
