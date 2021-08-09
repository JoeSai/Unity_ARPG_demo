using UnityEngine;
using UnityEngine.Playables;

public class MoveObjPlayableAsset : PlayableAsset
{
    public GameObject go;
    public Vector3 pos;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var bhv = new MoveObjPlayableBehaviour();
        bhv.go = go;
        bhv.pos = pos;
        return ScriptPlayable<MoveObjPlayableBehaviour>.Create(graph, bhv);
    }
}
