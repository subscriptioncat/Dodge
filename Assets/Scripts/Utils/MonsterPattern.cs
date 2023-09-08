using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "MonsterPattern", menuName = "Monster/Pattern", order = 1)]
public class MonsterPattern : ScriptableObject
{
    // ∆–≈œ
    public List<Pattern> Patterns;
    public MonsterEndCondition EndCondition;
    protected int count = 0;

    public virtual Pattern GetPattern()
    {
        if (count >= Patterns.Count) count = 0;
        return Patterns[count++];
    }
    public virtual bool GetEndCondition()
    {
        return EndCondition.IsEnd;
    }
}
[Serializable]
public struct Pattern
{
    public Vector2 Direction;
    public ePatternType Type;
    public float Time;
}

#if UNITY_EDITOR
[CustomEditor(typeof(MonsterPattern))]
public class MonsterPatternEditor : Editor
{
    SerializedProperty _PatternListProperty;
    SerializedProperty _EndConditionProperty;

    private void OnEnable()
    {
        _EndConditionProperty = serializedObject.FindProperty(nameof(MonsterPattern.EndCondition));
        _PatternListProperty = serializedObject.FindProperty(nameof(MonsterPattern.Patterns));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_EndConditionProperty);

        int toDelete = -1;
        for (int i = 0; i < _PatternListProperty.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Direction");
            GUILayout.Label("Type");
            GUILayout.Label("Time");
            GUILayout.Space(16);
            EditorGUILayout.EndHorizontal();

            var entryProp = _PatternListProperty.GetArrayElementAtIndex(i);

            var direcProp = entryProp.FindPropertyRelative("Direction");
            var typeProp = entryProp.FindPropertyRelative("Type");
            var timeProp = entryProp.FindPropertyRelative("Time");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(direcProp, GUIContent.none);
            EditorGUILayout.PropertyField(typeProp, GUIContent.none);
            EditorGUILayout.PropertyField(timeProp, GUIContent.none);
            if (GUILayout.Button("-", GUILayout.Width(16)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (toDelete != -1)
        {
            //Debug.Log($"Delete {toDelete}");
            _PatternListProperty.DeleteArrayElementAtIndex(toDelete);
        }

        if (GUILayout.Button("Add New Pattern", GUILayout.Width(200)))
        {
            //Debug.Log($"Add {_PatternListProperty.arraySize}");
            _PatternListProperty.InsertArrayElementAtIndex(_PatternListProperty.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif