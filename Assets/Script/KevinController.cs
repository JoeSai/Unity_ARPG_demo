using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KevinController : MonoBehaviour
{
    public GameObject model;
    public IUserInput pi;
    private Animator anim; 
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pi.attackR)
        {
            anim.SetTrigger("attack");
        }
    }

    /// <summary>
    /// Message
    /// </summary>


    void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }
}
