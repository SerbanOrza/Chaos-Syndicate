using UnityEngine;
using UnityEditor;

public class BuildingWindow : EditorWindow
{
    private GameObject targetObject;

    [MenuItem("Tools/Building Window")]
    public static void ShowWindow()
    {
        GetWindow<BuildingWindow>("Mesh Physics Adder");
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Rigidbody & MeshCollider to Objects with Mesh", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);
        shield = (ObjectsShield)EditorGUILayout.ObjectField("Objects field", shield, typeof(ObjectsShield), true);
        if (EditorGUI.EndChangeCheck())
        {
            // Optionally do something when the object changes
        }

        GUI.enabled = targetObject != null;
        if (GUILayout.Button("Add Rigidbody + MeshCollider"))
        {
            AddPhysicsComponents(targetObject);
        }
        if (GUILayout.Button("Add in list"))
        {
            AddInList(targetObject);
        }
        GUI.enabled = true;
    }

    private void AddPhysicsComponents(GameObject root)
    {
        int count = 0;
        foreach (var meshFilter in root.GetComponentsInChildren<MeshFilter>(true))
        {
            var obj = meshFilter.gameObject;

            // Add MeshCollider if not present
            if (obj.GetComponent<MeshCollider>() == null)
            {
                obj.AddComponent<MeshCollider>();
            }

            // Add Rigidbody if not present
            if (obj.GetComponent<Rigidbody>() == null)
            {
                obj.AddComponent<Rigidbody>();
            }

            count++;
        }

        Debug.Log($"Added Rigidbody & MeshCollider to {count} object(s) with MeshFilter under '{root.name}'.");
    }

    public ObjectsShield shield;
    private void AddInList(GameObject root)
    {
        foreach (Transform child in root.transform)
            shield.objects.Add(child.gameObject.GetComponent<LevitatingObject>());
            // child.gameObject.GetComponent<LevitatingObject>().rb = child.gameObject.GetComponent<Rigidbody>();
        Debug.Log("ok");
    }
}