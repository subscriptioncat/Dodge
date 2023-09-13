using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MonsterController : TopDownCharacterController
{
    [SerializeField] public MonsterPattern Pattern;
    private Pattern[] _currentPattern = null;
    private int _index = 0;
    private void PatternUpdate()
    {
        int endCount = 0;
        if (_currentPattern != null)
        {
            foreach (var pattern in _currentPattern)
            {
                if (pattern.IsEnd())
                {
                    ++endCount;
                    //Debug.Log($"[{_index}] {pattern.Type} End");
                    continue;
                }
                else
                {
                    pattern.Loop();
                }
                switch (pattern.Type)
                {
                    case ePatternType.None:
                        break;
                    case ePatternType.Move:
                        Move(pattern);
                        break;
                    case ePatternType.Fire:
                        Fire(pattern);
                        break;
                    case ePatternType.Look:
                        Look(pattern);
                        break;
                    case ePatternType.Dead:
                        Dead(pattern);
                        break;
                    default:
                        break;
                }

            }
        }
        if (_currentPattern == null || endCount >= _currentPattern.Length)
        {
            var item = Pattern.GetPattern(ref _index);
            _currentPattern = new Pattern[item.Length];
            for (int i = 0; i < item.Length; ++i)
            {
                _currentPattern[i] = new Pattern(item[i]);
                Debug.Log($"[{_index}] {item[i].Type} Start");
            }
        }
    }

    void Start()
    {
        //_currentPattern = Pattern.GetPattern(ref _index);
    }

    void FixedUpdate()
    {
        PatternUpdate();
    }

    /// <summary>
    /// 현재 위치에서 지정된 방향으로 이동
    /// </summary>
    private void Move(Pattern pattern)
    {
        if (pattern.IsEnd())
        {
            CallMoveEvent(Vector2.zero);
        }
        else if (pattern.IsNeedRun())
        {
            //var currentPos = this.gameObject.transform.position;
            //Vector2 targetPos = (Vector2)currentPos + pattern.Direction;
            Debug.Log($"[{_index}] {pattern.Type}");
            //CallMoveEvent(targetPos);
            CallMoveEvent(pattern.Direction);
        }
    }

    /// <summary>
    /// 현재 위치에서 Bullet을 지정된 방향으로 발사.
    /// </summary>
    private void Fire(Pattern pattern)
    {
        if (pattern.IsEnd())
        {
            //CallFireEvent(Vector2.zero);
        }
        else if (pattern.IsNeedRun())
        {
            Debug.Log($"[{_index}] {pattern.Type}");
            CallFireEvent(pattern.Direction);
        }
    }

    /// <summary>
    /// 현재 위치 기준에서 지정된 방향을 바라봄.
    /// </summary>
    private void Look(Pattern pattern)
    {
        if (pattern.IsEnd())
        {
            //CallLookEvent(Vector2.zero);
        }
        else if (pattern.IsNeedRun())
        {
            //var currentPos = this.gameObject.transform.position;
            //Vector2 targetPos = (Vector2)currentPos + pattern.Direction;
            Debug.Log($"[{_index}] {pattern.Type}");
            //CallLookEvent(targetPos);
            CallLookEvent(pattern.Direction);
        }
    }

    private void Dead(Pattern pattern)
    {
        if (pattern.IsEnd())
        {

        }
        else if (pattern.IsNeedRun())
        {
            Destroy(transform.gameObject);
        }
    }
}
