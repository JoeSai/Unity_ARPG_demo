using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    private Animator ani;
    // Start is called before the first frame update
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTrigger(string triggerName)
    {
        ani.ResetTrigger(triggerName);
    }
}
