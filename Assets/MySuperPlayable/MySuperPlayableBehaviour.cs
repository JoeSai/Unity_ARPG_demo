using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public GameObject myCamera;
    public float myFloat;
    PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        pd = (PlayableDirector)playable.GetGraph().GetResolver();
        foreach (var track in pd.playableAsset.outputs)
        {
            if(track.streamName == "Attacker Script")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(true);
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Script")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(false);
            }
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.Log("Behaviour play");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Debug.Log("Behaviour pause");
    }
}
