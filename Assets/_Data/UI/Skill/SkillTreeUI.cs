using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : BaseMonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private SkillTreeManager skillTreeManager;

    [Header("Node UI")]
    [SerializeField] private SkillNodeUI nodePrefab;
    [SerializeField] private Transform nodeContainer;

    [Header("Line UI")]
    [SerializeField] private Image linePrefab;
    [SerializeField] private Transform lineContainer;

    [Header("Popup")]
    [SerializeField] private SkillNodePopupUI popup;

    private Dictionary<int, SkillNodeUI> nodeUIs = new();
    private List<Image> lines = new();
    private List<SkillConnection> connections = new();
    protected override void Start()
    {
        BuildTree();
    }

    protected override void OnDestroy()
    {
        foreach (var node in nodeUIs.Values)
        {
            node.OnNodeClick -= OnNodeClicked;
        }
    }

    void BuildTree()
    {
        foreach (var pair in skillTreeManager.Nodes)
        {
            CreateNode(pair.Value);
        }

        Canvas.ForceUpdateCanvases();

        DrawConnections();

        UpdateConnections();
    }
    void CreateNode(SkillNodeRuntime runtime)
    {
        SkillNodeUI nodeUI = Instantiate(nodePrefab, nodeContainer);

        nodeUI.Init(runtime);

        nodeUI.OnNodeClick += OnNodeClicked;

        runtime.OnLevelChanged += OnNodeLevelChanged;

        nodeUIs.Add(runtime.Data.id, nodeUI);
    }
    void OnNodeClicked(int nodeId)
    {
        if (!skillTreeManager.Nodes.TryGetValue(nodeId, out SkillNodeRuntime runtime))
            return;

        popup.Show(runtime);
    }

    void DrawConnections()
    {
        foreach (var node in skillTreeManager.Nodes.Values)
        {
            if (node.Data.prerequisites == null)
                continue;

            foreach (int parentId in node.Data.prerequisites)
            {
                if (skillTreeManager.Nodes.TryGetValue(parentId, out var parent))
                {
                    CreateConnection(parent, node);
                }
            }
        }
    }
    void CreateConnection(SkillNodeRuntime from, SkillNodeRuntime to)
    {
        SkillNodeUI fromUI = nodeUIs[from.Data.id];
        SkillNodeUI toUI = nodeUIs[to.Data.id];

        Image line = Instantiate(linePrefab, lineContainer);

        RectTransform fromRect = fromUI.GetComponent<RectTransform>();
        RectTransform toRect = toUI.GetComponent<RectTransform>();

        Vector3 start = fromRect.position;
        Vector3 end = toRect.position;

        Vector3 dir = end - start;
        float distance = dir.magnitude;

        RectTransform lineRect = line.rectTransform;

        lineRect.position = start;
        lineRect.sizeDelta = new Vector2(distance, 4);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);

        line.color = Color.gray;

        connections.Add(new SkillConnection
        {
            from = from,
            to = to,
            line = line
        });
    }
    void OnNodeLevelChanged(SkillNodeRuntime node)
    {
        UpdateConnections();
    }
    void UpdateConnections()
    {
        foreach (var conn in connections)
        {
            if (conn.from.IsUnlocked && conn.to.IsUnlocked)
            {
                conn.line.color = Color.yellow;
            }
            else if (conn.from.IsUnlocked)
            {
                conn.line.color = Color.white;
            }
            else
            {
                conn.line.color = Color.gray;
            }
        }
    }
}