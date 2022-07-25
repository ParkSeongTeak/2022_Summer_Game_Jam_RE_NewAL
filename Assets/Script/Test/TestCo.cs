using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCo : MonoBehaviour
{
    public static TestCo Instance;
    public class pair
    {
        public int first;
        public int second;
        public pair(int f, int s)
        {
            first = f;
            second = s;
        }

        public pair()
        {
            first = 0;
            second = 0;
        }
    }

    public pair[] Arr = new pair[5];


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Arr " + Arr.Length);

        for(int i = 0; i < 5; i++)
        {
            Arr[i] = new pair();
        }

        for (int i=0; i<5; i++)
        {
        
            Arr[i].first = Random.Range(0, 10);
            Arr[i].second = Random.Range(0, 10);


            Debug.Log(Random.Range(0, 10));
        }
        
        //for (int i = 0; i < 5; i++)
        //    Debug.Log("TestCo firt"+ Arr[i].first+ "  Sec " + Arr[i].second);
    }

   


    // Update is called once per frame
    void Update()
    {
        
    }
    
}
