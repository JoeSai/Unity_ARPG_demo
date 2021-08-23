using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public IRoleController ac;
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public IActorManagerInterface im;
    // Start is called before the first frame update
    void Awake()
    {
        ac = GetComponent<IRoleController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);
        im = Bind<InteractionManager>(sensor);

    }

    //Generics ����
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

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid)
    {
        if (sm.isCounterBackSuccess)
        {
            // do nothing
            // �Է������
            if (counterValid)
            {
                targetWc.wm.am.Stuuned();
            }
        }
        // �ܷ�ʧ��
        else if(sm.isCounterBackFailure)
        {
            if (attackValid)
            {
                HirOrDie(false);
            }
        }
        else if (sm.isImmortal)
        {
            
        }else if (sm.isDefense)
        {
            if (counterValid)
            {
                Blocked();
            }
            else
            {
                //���󱻹��� todo�� ��Ӧ�ú���
                HirOrDie(true);
            }
            // Attack should be blocked!
        }
        else
        {
            if (attackValid)
            {
                HirOrDie(true);
            }
        }
    }
    public void HirOrDie(bool doHitAnimation)
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
                if (doHitAnimation)
                {
                    Hit();
                }
                //��Ч
                //do some vfx , like splatter blood...

            }
            else
            {
                Die();
            }
        }
    }
    public void Stuuned()
    {
        ac.IssueTrigger("stunned");
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

    public void TestEcho()
    {
        print("Echo Echo");
    }

    public void LockUnlockActorController(bool value)
    {
        //ʱ����Ԥ����ʱ��Ҳ�����
        if (ac)
        {
            ac.pi.inputEnable = false;
            ac.SetBool("lock", value);
        }
    }

}
