using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManagerInterface
{
    private CapsuleCollider interCol;
    public List<EventCasterManager> overlapEcastms= new List<EventCasterManager>();

    private void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (!overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Add(ecastm);
            }
            //print(ecastm.eventName);
            //print(ecastm.active);
        }

    }

    private void OnTriggerExit(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Remove(ecastm);
            }
            //print(ecastm.eventName);
            //print(ecastm.active);
        }
    }

}
