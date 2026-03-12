//using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObject/SkillEffect/AttackMoveEffect")]
//public class AttackMoveEffect : SkillEffect
//{
//    public float distance = 3f;
//    public float speed = 8f;
//    public bool moveToTarget = true;

//    public override void Execute(CharacterCtrl caster, SkillInstance skill)
//    {
//        var player = caster as PlayerCtrl;
//        if (player == null) return;

//        var motion = player.PlayerAttackMotion;
//        if (motion == null) return;

//        if (moveToTarget)
//        {
//            var enemy = player.PlayerTargetFinder?.FindNearest();

//            if (enemy != null)
//                motion.AttackMoveToTarget(enemy, distance, speed);
//            else
//                motion.AttackMoveForward(distance, speed);
//        }
//        else
//        {
//            motion.AttackMoveForward(distance, speed);
//        }
//    }
//}