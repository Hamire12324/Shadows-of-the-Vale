using UnityEngine;

public class SemiCircleLayout : BaseMonoBehaviour
{
    public RectTransform[] skills;
    public float radius = 250f;

    protected override void LoadComponents()
    {
        this.LoadSkill();

        DrawCircle(0, 5, radius);
        DrawCircle(5, 9, radius * 1.6f);
        DrawCircle(14, 9, radius * 2.2f);
    }

    void DrawCircle(int startIndex, int count, float r)
    {
        for (int i = 0; i < count; i++)
        {
            int index = startIndex + i;

            if (index >= skills.Length) return;

            float angle = Mathf.Lerp(0, 180, (float)i / (count - 1));
            float rad = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * r;
            float y = Mathf.Sin(rad) * r;

            skills[index].anchoredPosition = new Vector2(x, y);
        }
    }

    protected virtual void LoadSkill()
    {
        if (skills != null && skills.Length > 0) return;

        SkillNodeUI[] skillNodes = GetComponentsInChildren<SkillNodeUI>();
        skills = new RectTransform[skillNodes.Length];

        for (int i = 0; i < skillNodes.Length; i++)
        {
            skills[i] = skillNodes[i].GetComponent<RectTransform>();
        }
    }
}