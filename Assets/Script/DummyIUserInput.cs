using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            attackR = true;
            yield return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
