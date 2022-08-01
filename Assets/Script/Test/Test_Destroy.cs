using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Destroy : MonoBehaviour
{       
    
    GameObject[] Dummy = new GameObject[5];
    // S;tart is called before the first frame update
    void Start()
    {
        Debug.Log(Dummy.Length);
        for (int i = 4; i >= 0; i--)
        {
            Destroy(Dummy[i]);
        }
        Debug.Log("After_Destroy: " + Dummy.Length);
        Dummy = new GameObject[5];
        Debug.Log("New_Gobg: " + Dummy.Length);
    }   
    void Update()
    {


    }
}