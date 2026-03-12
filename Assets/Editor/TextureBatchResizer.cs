using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureBatchResizer : EditorWindow
{
    DefaultAsset folder;
    new int maxSize = 1024;

    [MenuItem("Tools/Texture Batch Resizer")]
    static void Init()
    {
        TextureBatchResizer window = (TextureBatchResizer)GetWindow(typeof(TextureBatchResizer));
        window.titleContent = new GUIContent("Texture Resizer");
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Resize Textures In Folder", EditorStyles.boldLabel);

        folder = (DefaultAsset)EditorGUILayout.ObjectField("Folder", folder, typeof(DefaultAsset), false);
        maxSize = EditorGUILayout.IntPopup("Max Size", maxSize,
            new string[] { "4096", "2048", "1024", "512" },
            new int[] { 4096, 2048, 1024, 512 });

        if (GUILayout.Button("Resize Textures"))
        {
            ResizeTextures();
        }
    }

    void ResizeTextures()
    {
        if (folder == null) return;

        string folderPath = AssetDatabase.GetAssetPath(folder);
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".tga"))
            {
                TextureImporter importer = AssetImporter.GetAtPath(file) as TextureImporter;
                if (importer != null)
                {
                    importer.maxTextureSize = maxSize;
                    importer.SaveAndReimport();
                }
            }
        }

        Debug.Log("Finished resizing textures!");
    }
}