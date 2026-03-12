//using UnityEngine;
//using UnityEditor;

//public class CreateSkillNodesEditor
//{
//    [MenuItem("Tools/Skill Tree/Create Skill Nodes")]
//    public static void CreateNodes()
//    {
//        SkillTreeData tree = Selection.activeObject as SkillTreeData;

//        if (tree == null)
//        {
//            Debug.LogError("Select a SkillTreeData asset first.");
//            return;
//        }

//        string folderPath = "Assets/_Data/Skill/Nodes/";

//        if (!AssetDatabase.IsValidFolder(folderPath))
//        {
//            AssetDatabase.CreateFolder("Assets/_Data/Skill", "Nodes");
//        }

//        for (int i = 0; i < 5; i++)
//        {
//            SkillNodeData node = ScriptableObject.CreateInstance<SkillNodeData>();

//            node.id = tree.nodes.Count;

//            string assetPath = folderPath + "SkillNode_" + node.id + ".asset";

//            AssetDatabase.CreateAsset(node, assetPath);

//            tree.nodes.Add(node);
//        }

//        EditorUtility.SetDirty(tree);
//        AssetDatabase.SaveAssets();

//        Debug.Log("Skill nodes created.");
//    }
//}