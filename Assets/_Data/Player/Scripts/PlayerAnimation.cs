using UnityEngine;

public class PlayerAnimation : PlayerAbstract
{
    public void Move()
    {
        float animSpeed = playerCtrl.PlayerMovement.GetAnimatorSpeed();
        playerCtrl.Animator.SetFloat("Speed", animSpeed, 0.1f, Time.deltaTime);
    }
    public void Jump()
    {
        playerCtrl.Animator.SetTrigger("Jump");
    }
    public void GroundAttack(int comboIndex)
    {
        playerCtrl.Animator.SetInteger("ComboIndex", comboIndex);
    }
    public void JumpAttack()
    {
        playerCtrl.Animator.SetTrigger("JumpAttack");
    }

    public void SetGround(bool isGrounded)
    {
        playerCtrl.Animator.SetBool("IsGround", isGrounded);
    }
    public bool IsPlaying(string stateName)
    {
        return playerCtrl.Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    public void ForcePlayDash()
    {
        Animator animator = playerCtrl.Animator;

        animator.ResetTrigger("Attack");

        animator.CrossFade("Dash", 0f);
    }
}
