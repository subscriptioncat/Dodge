using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "MonsterPattern", menuName = "Monster/Pattern", order = 1)]
public class MonsterPattern : ScriptableObject
{
    // 패턴
    public List<PatternBundle> PatternBundles = new List<PatternBundle>();
    public MonsterPattern()
    {
        PatternBundles.Add(new PatternBundle());
    }

    /// <summary>
    /// 1개의 Pattern Bundle을 리턴한다.
    /// </summary>
    /// <returns></returns>
    public virtual Pattern[] GetPattern(int index)
    {
        return PatternBundles[index].Patterns.ToArray();
    }
}
public enum ePatternType
{
    None,
    Move,
    Fire,
    Look
}
[Serializable]
public class PatternBundle
{
    public List<Pattern> Patterns = new List<Pattern>();
    public PatternBundle()
    {
        Patterns.Add(new Pattern());
    }
}
[Serializable]
public class Pattern
{
    public Pattern()
    {
        Type = ePatternType.None;
        Direction = new Vector2();
        Power = 0;
        Duration = .0f;
        Condition = new EndCondition();
        TimeElpased = .0f;
        Count = 0;
    }
    public ePatternType Type;
    public Vector2 Direction;
    public int Power;
    public float Duration;
    public float TimeElpased;
    public int Count;
    public EndCondition Condition;
    public virtual bool IsEnd()
    {
        return Condition.IsEnd(TimeElpased, Count);
    }
    public virtual void Loop()
    {
        TimeElpased += Time.deltaTime;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonsterPattern))]
public class MonsterPatternEditor : Editor
{
    SerializedProperty m_PatternBundlesListProperty;

    List<string> m_EndConditionNameList;

    int[][] m_Condition = new int[][] { };
    bool[] m_FoldOut = new bool[] { };

    private void OnEnable()
    {
        m_PatternBundlesListProperty = serializedObject.FindProperty(nameof(MonsterPattern.PatternBundles));
        m_EndConditionNameList = new List<string>();
        for (int i = 0; i < (int)EndCondition.eEndType.Time+1; ++i)
            m_EndConditionNameList.Add(((EndCondition.eEndType)i).ToString());
        UpdateLength();
    }

    private void UpdateLength()
    {
        if (m_PatternBundlesListProperty != null)
        {
            m_Condition = new int[m_PatternBundlesListProperty.arraySize][];
            for (int i = 0; i < m_PatternBundlesListProperty.arraySize; ++i)
            {
                var patternBundle = m_PatternBundlesListProperty.GetArrayElementAtIndex(i);
                var patterns = patternBundle.FindPropertyRelative(nameof(PatternBundle.Patterns));
                int[] selectNums = new int[patterns.arraySize];
                for (int j = 0; j < patterns.arraySize; ++j)
                {
                    var pattern = patterns.GetArrayElementAtIndex(j);
                    //Debug.Log($"{pattern.displayName}");
                    var condition = pattern.FindPropertyRelative(nameof(Pattern.Condition));
                    //Debug.Log($"{condition.displayName}");
                    var type = condition.FindPropertyRelative(nameof(EndCondition.Type));
                    //Debug.Log($"{type.enumValueIndex}");
                    if (condition != null)
                    {
                        selectNums[j] = type.enumValueIndex;
                    }
                    else
                        selectNums[j] = -1;
                }
                m_Condition[i] = selectNums;
            }
            var tempFoldOut = new bool[m_PatternBundlesListProperty.arraySize];
            Array.Copy(m_FoldOut, tempFoldOut, m_FoldOut.Length > tempFoldOut.Length ? tempFoldOut.Length : m_FoldOut.Length);
            m_FoldOut = tempFoldOut;
        }
        //else
            //Debug.Log($"m_PatternBundlesListProperty is Null");
    }

    private bool CheckLength()
    {
        if (m_Condition.Length != m_PatternBundlesListProperty.arraySize)
            return true;
        else
        {
            for (int i = 0; i < m_PatternBundlesListProperty.arraySize; ++i)
            {
                var patterns = m_PatternBundlesListProperty.GetArrayElementAtIndex(i).FindPropertyRelative(nameof(PatternBundle.Patterns));
                if (m_Condition[i].Length != patterns.arraySize)
                    return true;
            }
            return false;
        }
    }

    private static class size
    {
        public static int typeSize = 60;
        public static int directionSize = 120;
        public static int powerSize = 60;
        public static int durationSize = 60;
        public static int conditionNameSize = 60;
        public static int conditionSize = 60;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (CheckLength())
            UpdateLength();

        int toDelete = -1;
        if (m_PatternBundlesListProperty != null)
        {
            for (int i = 0; i < m_PatternBundlesListProperty.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                m_FoldOut[i] = EditorGUILayout.BeginFoldoutHeaderGroup(m_FoldOut[i], $"{i} Pattern Bundle");
                if (m_FoldOut[i])
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(nameof(Pattern.Type), GUILayout.MinWidth(size.typeSize));
                    GUILayout.Label(nameof(Pattern.Direction), GUILayout.MinWidth(size.directionSize));
                    GUILayout.Label(nameof(Pattern.Power), GUILayout.MinWidth(size.powerSize));
                    GUILayout.Label(nameof(Pattern.Duration), GUILayout.MinWidth(size.durationSize));
                    GUILayout.Label(nameof(Pattern.Condition), GUILayout.MinWidth(size.conditionNameSize));
                    GUILayout.Label("Limit", GUILayout.MinWidth(size.conditionSize));
                    GUILayout.Space(16);
                    EditorGUILayout.EndHorizontal();

                    var patternBundle = m_PatternBundlesListProperty.GetArrayElementAtIndex(i);
                    var patterns = patternBundle.FindPropertyRelative(nameof(PatternBundle.Patterns));

                    for (int j = 0; j < patterns.arraySize; ++j)
                    {
                        var entryProp = patterns.GetArrayElementAtIndex(j);
                        //Debug.Log($"{entryProp.displayName}");

                        var typeProp = entryProp.FindPropertyRelative(nameof(Pattern.Type));
                        var direcProp = entryProp.FindPropertyRelative(nameof(Pattern.Direction));
                        var powerProp = entryProp.FindPropertyRelative(nameof(Pattern.Power));
                        var durationProp = entryProp.FindPropertyRelative(nameof(Pattern.Duration));

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(typeProp, GUIContent.none, GUILayout.MinWidth(size.typeSize));
                        EditorGUILayout.PropertyField(direcProp, GUIContent.none, GUILayout.MinWidth(size.directionSize));
                        EditorGUILayout.PropertyField(powerProp, GUIContent.none, GUILayout.MinWidth(size.powerSize));
                        EditorGUILayout.PropertyField(durationProp, GUIContent.none, GUILayout.MinWidth(size.durationSize));

                        int isNew = EditorGUILayout.Popup(GUIContent.none, m_Condition[i][j], m_EndConditionNameList.ToArray(), GUILayout.MinWidth(size.conditionNameSize));
                        // 표시
                        if (m_Condition[i][j] != -1)
                        {
                            if (isNew != m_Condition[i][j])
                            {
                                m_Condition[i][j] = isNew;
                            }
                            var endConditionProp = entryProp.FindPropertyRelative(nameof(Pattern.Condition));
                            var type = endConditionProp.FindPropertyRelative(nameof(EndCondition.Type));
                            type.enumValueIndex = isNew;
                            if (type.enumValueIndex == (int)EndCondition.eEndType.Count)
                            {
                                var item = endConditionProp.FindPropertyRelative(nameof(EndCondition.intPoint));
                                var input = EditorGUILayout.IntField(item.intValue, GUILayout.MinWidth(size.conditionSize));
                                item.intValue = input;
                            }
                            else if (type.enumValueIndex == (int)EndCondition.eEndType.Time)
                            {
                                var item = endConditionProp.FindPropertyRelative(nameof(EndCondition.floatPoint));
                                var input = EditorGUILayout.FloatField(item.floatValue, GUILayout.MinWidth(size.conditionSize));
                                item.floatValue = input;
                            }
                        }
                        
                        if (GUILayout.Button("-", GUILayout.Width(32)))
                        {
                            toDelete = j;
                        }
                        EditorGUILayout.EndHorizontal();

                        if (toDelete != -1)
                        {
                            patterns.DeleteArrayElementAtIndex(toDelete);
                            //Debug.Log(toDelete);
                            toDelete = -1;
                        }
                    }
                    if (GUILayout.Button("Add Pattern", GUILayout.Width(200)))
                    {
                        patterns.InsertArrayElementAtIndex(patterns.arraySize);
                    }
                }
                EditorGUILayout.EndVertical();
                if (GUILayout.Button("-", GUILayout.Width(32)))
                {
                    toDelete = i;
                }
                if (toDelete != -1)
                {
                    m_PatternBundlesListProperty.DeleteArrayElementAtIndex(toDelete);
                    toDelete = -1;
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            if (GUILayout.Button("Add Pattern Bundle", GUILayout.Width(200)))
            {
                m_PatternBundlesListProperty.InsertArrayElementAtIndex(m_PatternBundlesListProperty.arraySize);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif