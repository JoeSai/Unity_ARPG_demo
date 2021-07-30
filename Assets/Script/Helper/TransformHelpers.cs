using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelpers
{
    //public static void hihi(this Transform trans)
    //{
    //    Debug.Log(trans);
    //    //Debug.Log(trans.name + "says" + say);
    //}

    public static Transform DeepFind(this Transform parent , string targetName)
    {      
        foreach (Transform child in parent)
        {
            //Debug.Log(child.name);
            if (child.name == targetName)
            {
                return child;
            }
            else if (DeepFind(child, targetName))
            {
                return DeepFind(child, targetName);
            }
        }
        return null;
    }
}
