using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArthurController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.5f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;

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
        anim.SetFloat("forward", pi.Dmag);
        if (pi.Dmag > 0.1f)
        {
            model.transform.forward = pi.Dvec;
        }
        movingVec = pi.Dmag * model.transform.forward * walkSpeed;
    }

    private void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
        //rigid.velocity = movingVec;
        //rigid.velocity = new Vector3(movingVec.x , rigid.velocity.y , movingVec.z);
    }
}
