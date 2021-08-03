using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface
{
    public float HP = 15.0f;
    public float HPMax = 15.0f;

    [Header("1st order states flags")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isJab;
    public bool isRoll;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;

    [Header("2nd order states flags")]
    public bool isAllowDefense;
    public bool isImmortal;


    //public void Test()
    //{
    //    print("sm test : HP is " + HP);
    //}

    void Start()
    {
        HP = HPMax;
    }

    private void Update()
    {
        if(am.ac.GetType() == typeof(PaladinController))
        {
            isGround = am.ac.CheckState("ground");
            isJump = am.ac.CheckState("jump");
            isFall = am.ac.CheckState("fall");
            isRoll = am.ac.CheckState("roll");
            isJab = am.ac.CheckState("jumpBackward");
            isAttack = am.ac.CheckStateTag("attack");
            isHit = am.ac.CheckState("hit");
            isDie = am.ac.CheckState("die");
            isBlocked = am.ac.CheckState("blocked");
            //isDefense = am.ac.CheckState("defense1h", "Defense Layer");
            //isAllowDe

            isAllowDefense = isGround || isBlocked;
            isDefense = isAllowDefense && am.ac.CheckState("defense1h", "Defense Layer");
            isImmortal = isRoll || isJab; 
        }
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
    }
}
