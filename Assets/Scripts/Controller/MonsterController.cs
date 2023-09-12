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
        foreach (var pattern in _currentPattern)
        {
            if (pattern.IsEnd())
            {
                ++endCount;
                continue;
            }
            else
            {
                pattern.Loop();
                Debug.Log($"{_index} | {pattern.Type} | {pattern.Duration}");
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
                    default:
                        break;
                }
            }
        }
        if (_currentPattern == null || endCount >= _currentPattern.Length)
        {
            _currentPattern = Pattern.GetPattern(ref _index);
        }
    }

    void Start()
    {
        _currentPattern = Pattern.GetPattern(ref _index);
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
        var currentPos = this.gameObject.transform.position;
        Vector2 targetPos = (Vector2)currentPos + pattern.Direction;
        CallMoveEvent(targetPos);
    }

    /// <summary>
    /// 현재 위치에서 Bullet을 지정된 방향으로 발사.
    /// </summary>
    private void Fire(Pattern pattern)
    {
        if (pattern.IsNeedRun())
            CallFireEvent(pattern.Direction);
    }

    /// <summary>
    /// 현재 위치 기준에서 지정된 방향을 바라봄.
    /// </summary>
    private void Look(Pattern pattern)
    {
        var currentPos = this.gameObject.transform.position;
        Vector2 targetPos = (Vector2)currentPos + pattern.Direction;
        CallLookEvent(targetPos);
    }
}
