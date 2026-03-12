//using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObject/SkillEffect/DashEffect")]
//public class DashEffect : SkillEffect
//{
//    public float dashForce = 20f;
//    public Vector3 direction = Vector3.forward;

//    public override void Execute(CharacterCtrl caster)
//    {
//        if (caster == null) return;

//        Rigidbody rb = caster.GetComponent<Rigidbody>();
//        if (rb == null) return;

//        Vector3 dashDir = caster.transform.TransformDirection(direction);
//        rb.AddForce(dashDir * dashForce, ForceMode.VelocityChange);
//    }
//}