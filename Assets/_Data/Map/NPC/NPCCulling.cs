using UnityEngine;

public class NPCCulling : MonoBehaviour
{
    public float disableAnimDistance = 50f;
    public float despawnDistance = 100f;

    Transform cam;

    Animator[] animators;
    SkinnedMeshRenderer[] skinnedMeshes;

    void Awake()
    {
        cam = Camera.main.transform;

        animators = GetComponentsInChildren<Animator>(true);
        skinnedMeshes = GetComponentsInChildren<SkinnedMeshRenderer>(true);
    }

    void Update()
    {
        foreach (Transform npc in transform)
        {
            float dist = Vector3.Distance(npc.position, cam.position);

            // NPC rất xa → despawn
            if (dist > despawnDistance)
            {
                npc.gameObject.SetActive(false);
                continue;
            }

            // NPC xa → tắt animation + skinned mesh
            bool disable = dist > disableAnimDistance;

            Animator anim = npc.GetComponentInChildren<Animator>();
            if (anim) anim.enabled = !disable;

            SkinnedMeshRenderer skin = npc.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skin) skin.enabled = !disable;
        }
    }
}
