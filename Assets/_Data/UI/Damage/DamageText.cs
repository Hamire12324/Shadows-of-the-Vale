using UnityEngine;
using UnityEngine.UI;

public class DamageText : TxtAbstract
{
    [SerializeField] float floatSpeed = 40f;
    [SerializeField] float lifeTime = 1.2f;
    [SerializeField] float drift = 30f;

    private float timer;
    private Vector3 moveDir;

    private CanvasGroup canvasGroup;
    protected override void OnEnable()
    {
        base.OnEnable();

        RectTransform rect = transform as RectTransform;
        rect.anchoredPosition = Vector2.zero;

        transform.localScale = Vector3.one;

        timer = 0f;

        if (canvasGroup != null) canvasGroup.alpha = 1f;

        moveDir = new Vector3(Random.Range(-0.5f, 0.5f), 1f, 0f).normalized;
    }
    protected override void Update()
    {
        Vector3 move = moveDir * floatSpeed * Time.deltaTime;

        move.x += Mathf.Sin(Time.time * 10f) * drift * Time.deltaTime;

        transform.Translate(move);

        timer += Time.deltaTime;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1 - (timer / lifeTime);
        }

        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvasGroup();
    }
    protected virtual void LoadCanvasGroup()
    {
        if (canvasGroup != null) return;
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
