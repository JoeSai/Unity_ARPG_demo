using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 1.0f;
    public float jumpBackVelocity = 3.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;
    private Vector3 thrustVec;
    //private Vector3 rollVec;
    private bool lockMovingVec = false;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = pi.run ? 0.01f : 0.03f;
        float targetRunMulit = ((pi.run) ? 2.0f : 1.0f);
        anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), pi.Dmag * targetRunMulit, t));

        if (rigid.velocity.magnitude > 5.0f)
        {
            anim.SetTrigger("roll");
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }
        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.1f);
            model.transform.forward = targetForward;
        }
        if (!lockMovingVec)
        {
            movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run && anim.GetFloat("forward") > 0.9f) ? runMultiplier : 1.0f); ;
        }
    }

    private void FixedUpdate()
    {
        //rigid.position += movingVec * Time.fixedDeltaTime;
        //rigid.velocity = movingVec;
        rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    /// <summary>
    /// Messgae
    /// </summary>
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        lockMovingVec = true;
        pi.inputEnable = false;
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


}
