using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test
{
    public string testString;//= "TestString";
    public int testInt = 1;
    public Bounds value = new Bounds();
    //public List<int> testIntList = new List<int>() { 5, 10, 20, 30 };
    public List<string> erenmon = new List<string>() { "a", "b", "c", "d" };
    public List<object> objlist = new List<object>() { "a", 1 , 0.1f};
    public test()
    {

    }
    public test(string a)
    {
        testString = a;
    }

}
