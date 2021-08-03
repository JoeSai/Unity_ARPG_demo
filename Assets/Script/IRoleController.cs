using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IRoleController : MonoBehaviour
{
    public GameObject model;
    public Animator anim;
    public IUserInput pi;
    public CameraController camcon;
    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }
}