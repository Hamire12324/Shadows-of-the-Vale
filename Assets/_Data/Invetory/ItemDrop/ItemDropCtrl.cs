using UnityEngine;

public class ItemDropCtrl : PoolObj
{
    [SerializeField] protected Rigidbody _rigi;
    [SerializeField] protected int itemCount = 1;
    [SerializeField] protected bool isAutoPickup = false;
    [SerializeField] protected ItemPickupTrigger itemPickup;
    [SerializeField] protected ItemProfileSO itemProfile;

    public Rigidbody Rigidbody => _rigi;
    public int ItemCount => itemCount;
    public bool IsAutoPickup => isAutoPickup;
    public ItemPickupTrigger ItemPickup => itemPickup;

    public ItemProfileSO ItemProfile => itemProfile;
    public string ItemName => itemProfile?.ItemName ?? string.Empty;
    public Sprite Icon => itemProfile?.Icon;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform visualRoot;
    [SerializeField] private float rotateSpeed = 120f;
    [SerializeField] private float floatHeight = 0.5f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float floatAmount = 0.1f;
    [SerializeField] private Vector3 startPos;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (visualRoot != null)
            startPos = visualRoot.localPosition;
    }
    protected override void Update()
    {
        base.Update();

        if (visualRoot == null) return;

        visualRoot.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        float newY = floatHeight + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        visualRoot.localPosition = new Vector3(0, newY, 0);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigibody();
        this.LoadItemPickup();
        this.LoadVisual();
    }
    protected virtual void LoadRigibody()
    {
        if (this._rigi != null) return;
        this._rigi = GetComponent<Rigidbody>();
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }
    protected virtual void LoadItemPickup()
    {
        if (this.itemPickup != null) return;
        this.itemPickup = GetComponentInChildren<ItemPickupTrigger>();
        Debug.Log(transform.name + ": LoadItemPickup", gameObject);
    }
    protected virtual void LoadVisual()
    {
        if (spriteRenderer != null) return;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError($"{name}: SpriteRenderer not found!", gameObject);
            return;
        }

        visualRoot = spriteRenderer.transform;
    }
    public void Init(ItemProfileSO profile, int count, bool autoPickup)
    {
        itemProfile = profile;
        itemCount = count;
        isAutoPickup = autoPickup;

        if (spriteRenderer != null && profile.Icon != null)
            spriteRenderer.sprite = profile.Icon;

        if (visualRoot != null)
        {
            visualRoot.localRotation = Quaternion.identity;
            visualRoot.localPosition = Vector3.zero;
        }

        gameObject.SetActive(true);
    }
}