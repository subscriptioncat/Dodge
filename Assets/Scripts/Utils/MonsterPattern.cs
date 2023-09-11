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
        PatternBundles.First().Patterns.Add(new Pattern()
        {
            Type = ePatternType.None,
            Direction = new Vector2(),
            Duration = 10.0f,
            Power = 0,
            Condition = null
        });
    }

    /// <summary>
    /// 다음 None 혹은 Move이 나오기 전까지의 패턴 배열을 리턴한다.
    /// </summary>
    /// <returns></returns>
    public virtual Pattern[] GetPattern(int index)
    {
        LinkedList<Pattern> ret = new LinkedList<Pattern>();
        return ret.ToArray();
    }

    /// <summary>
    /// EndCondition의 IsEnd를 리턴한다.
    /// </summary>
    /// <returns></returns>
    public virtual bool GetEndCondition(int index)
    {
        return false;
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
        Duration = 0;
        Condition = null;
        m_TimeElpased = 0;
    }
    public ePatternType Type { get; set; }
    public Vector2 Direction { get; set; }
    public int Power { get; set; }
    public float Duration { get; set; }
    public MonsterEndCondition Condition { get; set; }
    private float m_TimeElpased;
    public virtual bool IsEnd()
    {
        return Condition.IsEnd(m_TimeElpased);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonsterPattern))]
public class MonsterPatternEditor : Editor
{
    SerializedProperty m_PatternBundlesListProperty;
    SerializedProperty m_EndConditionProperty;

    List<string> m_EndConditionList;

    int[][] m_Select;
    bool[] m_FoldOut;

    private void OnEnable()
    {
        m_PatternBundlesListProperty = serializedObject.FindProperty(nameof(MonsterPattern.PatternBundles));
        var lookup = typeof(MonsterEndCondition);
        m_EndConditionList = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && x.IsAbstract && x.IsSubclassOf(lookup))
            .Select(type => type.Name)
            .ToList();

        if (m_PatternBundlesListProperty != null)
        {
            m_Select = new int[m_PatternBundlesListProperty.arraySize][];
            for (int i = 0; i < m_PatternBundlesListProperty.arraySize; ++i)
            {
                var patternBundle = m_PatternBundlesListProperty.GetArrayElementAtIndex(i);
                var patterns = patternBundle.FindPropertyRelative(nameof(PatternBundle.Patterns));
                int[] selectNums = new int[patterns.arraySize];
                for (int j = 0; j < patterns.arraySize; ++j)
                {
                    var entry = patterns.GetArrayElementAtIndex(j);
                    var condition = entry.FindPropertyRelative(nameof(Pattern.Condition));
                    if (condition != null)
                        selectNums[j] = m_EndConditionList.IndexOf(condition.type);
                    else
                        selectNums[j] = -1;
                }
                m_Select[i] = selectNums;
            }
            m_FoldOut = new bool[m_PatternBundlesListProperty.arraySize];
        }
    }

    private void UpdateLength()
    {
        if (m_PatternBundlesListProperty != null)
        {
            m_Select = new int[m_PatternBundlesListProperty.arraySize][];
            for (int i = 0; i < m_PatternBundlesListProperty.arraySize; ++i)
            {
                var patternBundle = m_PatternBundlesListProperty.GetArrayElementAtIndex(i);
                var patterns = patternBundle.FindPropertyRelative(nameof(PatternBundle.Patterns));
                int[] selectNums = new int[patterns.arraySize];
                for (int j = 0; j < patterns.arraySize; ++j)
                {
                    var entry = patterns.GetArrayElementAtIndex(j);
                    var condition = entry.FindPropertyRelative(nameof(Pattern.Condition));
                    if (condition != null)
                        selectNums[j] = m_EndConditionList.IndexOf(condition.type);
                    else
                        selectNums[j] = -1;
                }
                m_Select[i] = selectNums;
            }
            m_FoldOut = new bool[m_PatternBundlesListProperty.arraySize];
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (m_FoldOut.Length != m_PatternBundlesListProperty.arraySize)
            UpdateLength();

        int toDelete = -1;
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(nameof(Pattern.Type));
        GUILayout.Label(nameof(Pattern.Direction));
        GUILayout.Label(nameof(Pattern.Power));
        GUILayout.Label(nameof(Pattern.Duration));
        GUILayout.Label(nameof(Pattern.Condition));
        GUILayout.Space(16);
        EditorGUILayout.EndHorizontal();
        if (m_PatternBundlesListProperty != null)
        {
            for (int i = 0; i < m_PatternBundlesListProperty.arraySize; ++i)
            {
                m_FoldOut[i] = EditorGUILayout.BeginFoldoutHeaderGroup(m_FoldOut[i], $"{i} Pattern");
                if (m_FoldOut[i])
                {
                    var patternBundle = m_PatternBundlesListProperty.GetArrayElementAtIndex(i);
                    var patterns = patternBundle.FindPropertyRelative(nameof(PatternBundle.Patterns));

                    for (int j = 0; j < patterns.arraySize; ++j)
                    {
                        var entryProp = patterns.GetArrayElementAtIndex(j);

                        var typeProp = entryProp.FindPropertyRelative(nameof(Pattern.Type));
                        var direcProp = entryProp.FindPropertyRelative(nameof(Pattern.Direction));
                        var powerProp = entryProp.FindPropertyRelative(nameof(Pattern.Power));
                        var durationProp = entryProp.FindPropertyRelative(nameof(Pattern.Duration));

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.TextField($"{j}");
                        EditorGUILayout.PropertyField(typeProp, GUIContent.none);
                        EditorGUILayout.PropertyField(direcProp, GUIContent.none);
                        EditorGUILayout.PropertyField(powerProp, GUIContent.none);
                        EditorGUILayout.PropertyField(durationProp, GUIContent.none);
                        int isNew = EditorGUILayout.Popup("EndCondition", m_Select[i][j], m_EndConditionList.ToArray());
                        if (isNew != m_Select[i][j])
                        {
                            if (m_Select[i][j] != -1)
                            {
                                var item = entryProp.FindPropertyRelative(nameof(Pattern.Condition)).objectReferenceValue;
                                DestroyImmediate(item, true);
                            }
                            m_Select[i][j] = isNew;
                            var newInstance = ScriptableObject.CreateInstance(m_EndConditionList[isNew]);

                            AssetDatabase.AddObjectToAsset(newInstance, target);
                            m_EndConditionProperty.objectReferenceValue = newInstance;
                        }
                    }


                    if (GUILayout.Button("-", GUILayout.Width(16)))
                    {
                        toDelete = i;
                    }
                    EditorGUILayout.EndHorizontal();

                    if (toDelete != -1)
                    {
                        patterns.DeleteArrayElementAtIndex(toDelete);
                    }

                    if (GUILayout.Button("Add Pattern", GUILayout.Width(200)))
                    {
                        patterns.InsertArrayElementAtIndex(patterns.arraySize);
                    }
                }
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