using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    public Collider weaponColL;
    public Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    private void Start()
    {
        //print(transform.DeepFind("WeaponHandleR"));
        if (!whL)
        {
            whL = transform.DeepFind("WeaponHandleL").gameObject;
        }
        if (!whR)
        {
            whR = transform.DeepFind("WeaponHandleR").gameObject;
        }

        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR = whR.GetComponentInChildren<Collider>();
        //transform.hihi();
        //print(am.ac.GetType());
    }

    public void WeaponEnable()
    {
        if(am.ac.GetType() == typeof(PaladinController))
        {
            if (am.ac.CheckStateTag("attackL"))
            {
                weaponColL.enabled = true;
            }
            else if(am.ac.CheckStateTag("attackR"))
            {
                weaponColR.enabled = true;
            }
        }
        else if(am.ac.GetType() == typeof(KevinController))
        {
            weaponColR.enabled = true;
        }
        //¿˚”√CheckStateTag≈–∂œ◊Û”“ ÷
        //if(am.ac)
        //print("WeaponEnable");
   
    }

    public void WeaponDisable()
    {
        //print("WeaponDisable");
        weaponColL.enabled = false;
        weaponColR.enabled = false;
    }
}
