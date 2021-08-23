using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : IActorManagerInterface
{
    public PlayableDirector pd;

    [Header("=== Timeline assets ===")]
    public TimelineAsset frontStab;

    [Header("=== Assets Settings ===")]
    public ActorManager attacker;
    public ActorManager victim;

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        pd.playableAsset = Instantiate(frontStab);

        foreach (var track in pd.playableAsset.outputs)
        {
            if(track.streamName == "Attacker Script")
            {
                pd.SetGenericBinding(track.sourceObject, attacker);
            }
            else if (track.streamName == "Victim Script")
            {
                pd.SetGenericBinding(track.sourceObject, victim);
            }
            else if (track.streamName == "Attacker Animation")
            {
                pd.SetGenericBinding(track.sourceObject, attacker.ac.anim);
            }
            else if (track.streamName == "Victim Animation")
            {
                pd.SetGenericBinding(track.sourceObject, victim.ac.anim);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            pd.Play();
        }
    }
}
