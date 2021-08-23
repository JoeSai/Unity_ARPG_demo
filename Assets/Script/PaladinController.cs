using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : IRoleController
{

    //public GameObject model;
    public float walkSpeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;
    public float jumpBackVelocity = 3.0f;
    public string attackLayerName = "Attack Layer";
    public float weightVelocity;
    public float weightVelocity2;

    [Space(10)]
    [Header("===== Friction Setting =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZeo;

    private Rigidbody rigid;
    private Vector3 movingVec;
    private Vector3 thrustVec;
    private bool canAttack;
    private CapsuleCollider col;
    //private float lerpTarget;
    private Vector3 deltaPos;
    public bool leftIsShield = true;

    //private Vector3 rollVec;
    private bool lockMovingVec = false;
    private bool trackDirection = false;


    // Start is called before the first frame update
    void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = pi.run ? 0.01f : 0.03f;
        float targetRunMulit = ((pi.run) ? 2.0f : 1.0f);

        if (!camcon.lockState)
        {
            anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), pi.Dmag * targetRunMulit, t));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDvec.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDvec.x * ((pi.run) ? 2.0f : 1.0f));
            //anim
        }

        //if (pi.roll)
        if (rigid.velocity.magnitude > 5.0f)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }
        if (pi.attackR && (CheckState("ground") || CheckStateTag("attack")) && canAttack)
        {
            anim.SetBool("R0L1", false);
            anim.SetTrigger("attack");
        }

        if (pi.attackL && !leftIsShield && (CheckState("ground") || CheckStateTag("attack")) && canAttack)
        {
            anim.SetBool("R0L1", true);
            anim.SetTrigger("attack");
        }

        //¶Ü·´
        if(pi.counterBack && leftIsShield && CheckState("ground"))
        {
            anim.SetTrigger("counterBack");
        }

        if (pi.defense && leftIsShield && (CheckState("ground") || CheckState("blocked")))
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 1);
            anim.SetBool("defense", true);
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense Layer"), 0);
            anim.SetBool("defense", false);
        }


        if (pi.lockon)
        {
            camcon.ToggleLock();
        }

        if (!camcon.lockState)
        {
            if (pi.Dmag > 0.1f)
            {
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.1f);
                model.transform.forward = targetForward;
            }
            if (!lockMovingVec)
            {
                movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run && anim.GetFloat("forward") > 0.9f) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            if (!trackDirection)
            {
                transform.forward = transform.forward;
            }
            else
            {
                transform.forward = movingVec.normalized;
            }
            if (!lockMovingVec)
            {
                movingVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }

        if (pi.action)
        {
            InvokeAction();
        }
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos * 1.2f; ;
        //rigid.position += movingVec * Time.fixedDeltaTime;
        //rigid.velocity = movingVec;
        rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
        //if(anim.GetLayerWeight(anim.GetLayerIndex(attackLayerName)) <= 0.1f)
        //{
        //    anim.SetBool("canAttack", true);
        //}
        //else
        //{
        //    print(anim.GetLayerWeight(anim.GetLayerIndex(attackLayerName)));
        //    anim.SetBool("canAttack", false);
        //}
    }

    /// <summary>
    /// Messgae
    /// </summary>
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        lockMovingVec = true;
        pi.inputEnable = false;
        trackDirection = true;
        //Debug.Log("On JumpEnter");
    }


    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        lockMovingVec = false;
        pi.inputEnable = true;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    public void OnGroundExit()
    {
        col.material = frictionZeo;
    }

    public void OnFallEnter()
    {
        lockMovingVec = true;
        pi.inputEnable = false;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        lockMovingVec = true;
        pi.inputEnable = false;
        trackDirection = true;
    }

    public void OnJumpBackEnter()
    {
        lockMovingVec = true;
        pi.inputEnable = false;
    }

    public void OnJumpBackUpdate()
    {
        thrustVec = model.transform.forward * -jumpBackVelocity;
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnable = false;
    }


    public void OnAttack1hAUpdate() {
        //thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //float currentWeight = Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex(attackLayerName)), lerpTarget, 0.2f);
        //float currentWeight = Mathf.SmoothDamp(anim.GetLayerWeight(anim.GetLayerIndex(attackLayerName)), 1, ref weightVelocity2, 0.2f);
        //anim.SetLayerWeight(anim.GetLayerIndex(attackLayerName), currentWeight);
    }

    public void OnHitEnter()
    {
        pi.inputEnable = false;
        movingVec = Vector3.zero;
    }

    public void OnBlockedEnter()
    {
        pi.inputEnable = false;
        thrustVec = model.transform.forward * -jumpBackVelocity;
    }

    public void OnBlockedUpdate()
    {
        // ·ÀÓù»÷ÍË³åÁ¿
        thrustVec = model.transform.forward * -1.5f;
    }

    public void OnDieEnter()
    {
        pi.inputEnable = false;
        movingVec = Vector3.zero;
    }

    public void OnStunnedEnter()
    {
        pi.inputEnable = false;
        movingVec = Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        pi.inputEnable = false;
        movingVec = Vector3.zero;
    }

    public void OnUpdateRM(Vector3 _deltaPos)
    {
        //if (CheckState("attack1hC", attackLayerName)) { 
        //    deltaPos += (deltaPos + _deltaPos) / 2;
        //}
        if (CheckState("attack1hD") || CheckState("attack1hE") || CheckState("attack1hF"))
        {
            deltaPos += (deltaPos + _deltaPos) / 2;
        }
    }


}
