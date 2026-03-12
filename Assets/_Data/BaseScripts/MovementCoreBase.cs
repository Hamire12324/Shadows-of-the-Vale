using UnityEngine;

public abstract class MovementCoreBase<T> : BaseMonoBehaviour where T : CharacterCtrl
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float rotationSpeed = 10f;

    [Header("Gravity")]
    [SerializeField] protected float gravity = -9.81f;
    protected Vector3 velocity;

    protected Vector3 moveDirection = Vector3.zero;

    [SerializeField] protected T ctrl;

    protected override void Update()
    {
        this.HandleMovement();
        this.HandleGravity();
        this.HandleAnimation();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }
    protected virtual void LoadCtrl()
    {
        if (ctrl != null) return;
        ctrl = GetComponentInParent<T>();
        Debug.Log(transform.name + ": LoadEnemyCtrl", gameObject);
    }
    protected virtual void HandleMovement()
    {
        if (ctrl.StatusEffect.IsStunned) return;

        Vector3 move = Vector3.zero;

        if (moveDirection != Vector3.zero)
        {
            move = moveDirection.normalized * moveSpeed;

            Quaternion targetRot = Quaternion.LookRotation(moveDirection);
            transform.parent.rotation = Quaternion.Slerp(
                transform.parent.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        Vector3 finalMove =
            move * Time.deltaTime +
            new Vector3(0, velocity.y * Time.deltaTime, 0);

        ctrl.CharacterController.Move(finalMove);
    }

    protected virtual void HandleGravity()
    {
        if (ctrl.CharacterController.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

    protected virtual void HandleAnimation()
    {
        float animSpeed = moveDirection.magnitude * moveSpeed / moveSpeed;
        ctrl.Animator.SetFloat("Speed", animSpeed);
        ctrl.Animator.SetBool("IsGround", ctrl.CharacterController.isGrounded);
    }
}
