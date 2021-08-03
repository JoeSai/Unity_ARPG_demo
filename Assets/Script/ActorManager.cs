using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public IRoleController ac;
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<IRoleController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);

    }

    //Generics ·ºÐÍ
    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        tempInstance = go.GetComponent<T>();
        if (!tempInstance)
        {
            tempInstance = go.AddComponent<T>();
        }
        tempInstance.am = this;
        return tempInstance;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void TryDoDamage()
    {

        if (sm.isImmortal)
        {
            
        }else if (sm.isDefense)
        {
            // Attack should be blocked!
            Blocked();
        }
        else
        {
            if (sm.HP <= 0)
            {
                // Already dead.
            }
            else
            {
                sm.AddHP(-5);
                if (sm.HP > 0)
                {
                    Hit();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnable = false;
        if(ac.camcon && ac.camcon.lockState)
        {
            ac.camcon.ToggleLock();
            ac.camcon.enabled = false;
        }
    }
}
