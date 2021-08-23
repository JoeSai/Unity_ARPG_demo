using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyTest2 : MonoBehaviour
{
    public StudyTest st;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(st.MyName = 1);
        StudyTest st = new StudyTest();
        StudyTest st2 = new StudyTest();
        Debug.Log(st.enemyCount);
        Debug.Log(StudyTest.enemyCount2);

        //Debug.Log(st.Add(1, "q"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
