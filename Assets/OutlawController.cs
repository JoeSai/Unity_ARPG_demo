using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.5f;
    public float runMultiplier = 2.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;
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
        float targetRunMulit = ((pi.run) ? 2.0f : 1.0f);
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(1.0f, targetRunMulit,0.5f));
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
            movingVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f); ;
        }
    }

    private void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
        //rigid.velocity = movingVec;
        //rigid.velocity = new Vector3(movingVec.x , rigid.velocity.y , movingVec.z);
    }


    public void OnJumpEnter()
    {
        //lockMovingVec = true;
        //pi.inputEnable = false;
        //Debug.Log("On JumpEnter");
    }

    public void OnJumpExit()
    {
        //lockMovingVec = false;
        //pi.inputEnable = true;
        //Debug.Log("On JumpExit");
    }
}
