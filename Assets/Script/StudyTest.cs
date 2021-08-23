using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyTest
{
    private int myName = 100;
    public int MyName {
        set { }
        get { return myName; }
    }
    public int MyProperty { get; set; }

    public int enemyCount = 0;
    public static int enemyCount2 = 0;

    public StudyTest()
    {
        enemyCount++;
        enemyCount2++;
    }

    public int Add(int num1 , int num2)
    {
        return num1 + num2;
    }

    public string Add(string num1 , string num2)
    {
        return num1 + num2;
    }

}
