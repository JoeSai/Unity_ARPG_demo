using UnityEngine;
using UnityEngine.Playables;

public class MoveObjPlayableBehaviour : PlayableBehaviour
{
    public GameObject go;
    public Vector3 pos;

    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);

        Debug.Log("OnGraphStart=======================");
    }

    public override void OnGraphStop(Playable playable)
    {
        base.OnGraphStop(playable);
        Debug.Log("OnGraphStop=======================");
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // ������߼��Ǹı���������꣬�����߼��Ϳ���������
        base.OnBehaviourPlay(playable, info);
        Debug.Log("OnBehaviourPlay=======================");
        if (null != go)
        {
            go.transform.position = pos;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        base.OnBehaviourPause(playable, info);
        Debug.Log("OnBehaviourPause=======================");
        if (null != go)
        {
            go.transform.position = Vector3.zero;
        }
    }

    public override void OnBehaviourDelay(Playable playable, FrameData info)
    {
        base.OnBehaviourDelay(playable, info);
        Debug.Log("OnBehaviourDelay=======================");
    }
}
