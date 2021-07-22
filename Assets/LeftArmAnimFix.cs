using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    public Vector3 a;
    public Vector3 b;
    private void Awake()
    {
        anim = GetComponent<Animator>();        
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (!anim.GetBool("defense"))
        {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowerArm.localEulerAngles += a;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
        }

        else
        {
            Transform leftUpperArm = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            leftUpperArm.localEulerAngles += b;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftUpperArm, Quaternion.Euler(leftUpperArm.localEulerAngles));
        }
    }
}
