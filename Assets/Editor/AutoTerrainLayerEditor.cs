using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AutoTerrainLayer))]
public class AutoTerrainLayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoTerrainLayer script = (AutoTerrainLayer)target;
        if (GUILayout.Button("Apply Terrain Layers"))
        {
            script.Apply();
        }
    }
}