using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Collider weaponColL;
    public Collider weaponColR;
    public ActorManager am;

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
    }

    public void WeaponEnable()
    {
        //print("WeaponEnable");
        weaponColR.enabled = true;
    }

    public void WeaponDisable()
    {
        //print("WeaponDisable");
        weaponColR.enabled = false;
    }
}
