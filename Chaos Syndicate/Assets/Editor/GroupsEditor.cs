using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Groups))]
public class GroupsEditor : Editor {
    private ReorderableList groupList;

    private void OnEnable() {
        var groupsProperty = serializedObject.FindProperty("groups");

        groupList = new ReorderableList(serializedObject, groupsProperty, draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);

        groupList.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, "Groups");
        };

        groupList.elementHeightCallback = index => {
            var element = groupsProperty.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(element, true) + 8;
        };

        groupList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = groupsProperty.GetArrayElementAtIndex(index);
            rect.y += 4;

            // Draw foldout + all children (objects list), label: Group i
            EditorGUI.PropertyField(rect, element, new GUIContent($"Group {index}"), true);
        };
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        groupList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}