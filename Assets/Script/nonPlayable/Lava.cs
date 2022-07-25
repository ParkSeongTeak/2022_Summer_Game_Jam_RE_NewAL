using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    GameObject lava;
    Vector3 lavaVec;

    float uptime;
    float upheight;
    // Start is called before the first frame update

    private void Awake()
    {

        lava = this.gameObject;
        lavaVec = lava.transform.position;
        uptime = 2f;

        upheight = 0.25f;

    }

    void Start()
    {
        //StartCoroutine("LavaUp");
    }


    public void StartLavaMove()
    {
        Debug.Log("Start");
        StartCoroutine("LavaUp");
        
    }


    public void StopLavaMove()
    {
        StopCoroutine("LavaUp");
    }
    IEnumerator LavaUp()
    {

        while (true)
        {
            lava.transform.position = lavaVec;
            lavaVec.y += upheight;
            yield return new WaitForSeconds(uptime);   
        }
        
    }

}
