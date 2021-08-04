using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    public Collider weaponColL;
    public Collider weaponColR;

    public GameObject whL;
    public GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

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


        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);
        //transform.hihi();
        //print(am.ac.GetType());
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if(tempWc == null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;
        return tempWc;
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

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
}
