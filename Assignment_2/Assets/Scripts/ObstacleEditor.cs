using UnityEngine;
using UnityEditor;

public class ObstacleEditor : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Tools/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditor>("Obstacle Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacle Grid (10x10)", EditorStyles.boldLabel);

        // Assign the Scriptable Object
        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data:", obstacleData, typeof(ObstacleData), false);

        if (obstacleData == null)
        {
            EditorGUILayout.HelpBox("Assign an ObstacleData Scriptable Object!", MessageType.Warning);
            return;
        }

        EditorGUI.BeginChangeCheck(); // Track changes

        for (int x = 0; x < 10; x++)
        {
            GUILayout.BeginHorizontal();
            for (int z = 0; z < 10; z++)
            {
                ////bool isBlocked = obstacleData.obstacleGrid[x, z];
                //bool newValue = GUILayout.Toggle(isBlocked, "", GUILayout.Width(25), GUILayout.Height(25));

                //if (newValue != isBlocked)
                //{
                //   // obstacleData.obstacleGrid[x, z] = newValue;
                //}
            }
            GUILayout.EndHorizontal();
        }

        if (EditorGUI.EndChangeCheck()) // If any changes were made
        {
            EditorUtility.SetDirty(obstacleData); // Mark as dirty (so Unity saves changes)
            AssetDatabase.SaveAssets(); // Save changes immediately
        }
    }
}
