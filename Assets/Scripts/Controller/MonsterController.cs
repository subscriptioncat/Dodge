using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MonsterController : TopDownCharacterController
{
    public List<MonsterPattern> _pattern = new List<MonsterPattern>();
    public void Pattern()
    {
        CallMoveEvnet(_pattern[0].Move());
    }

    public void Fire()
    {
        GameObject bulletModel = DataManager.Instance.Bullet;
        var bulletFired = GameObject.Instantiate(bulletModel, this.transform, true);
    }
    
    public void Aim()
    {

    }
    
    public void Look()
    {

    }
    
    [CreateAssetMenu(fileName = "MonsterPattern", menuName = "Monster/Pattern", order = 0)]
    public class MonsterPattern : ScriptableObject
    {
        public List<Vector2> Positions = new List<Vector2>();
        private int count = 0;
        public virtual Vector2 Move()
        {
            if (count >= Positions.Count) count = 0;
            return Positions[count++];
        }
        public virtual bool Condition()
        {
            return false;
        }
    }
}
