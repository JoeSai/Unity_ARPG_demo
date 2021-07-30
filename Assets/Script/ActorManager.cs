using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public PaladinController ac;
    public BattleManager bm;
    public WeaponManager wm;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<PaladinController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;
        bm = sensor.GetComponent<BattleManager>();
        if (!bm)
        {
            bm = sensor.AddComponent<BattleManager>();
        }
        bm.am = this;

        wm = model.GetComponent<WeaponManager>();
        if (!wm)
        {
            wm = model.AddComponent<WeaponManager>();
        }
        wm.am = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DoDamage()
    {
        ac.IssueTrigger("hit");
    }
}
