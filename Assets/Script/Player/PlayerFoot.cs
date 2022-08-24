using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{

    GameObject Player;
    Vector3 _destPos;


    bool Ground = true;
    float RayLenght = 0.25f; 

    // Start is called before the first frame update
    void Start()
    {
        Player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray();
        ray.origin = this.transform.position;
        ray.direction = - this.transform.up;
        Debug.DrawRay(this.transform.position, ray.direction * RayLenght, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RayLenght, LayerMask.GetMask("Block")))
        {
         
            Player.GetComponent<PlayerController>().isGrounded();
            if(hit.transform.gameObject.tag == "Needle")
            {
                //Debug.Log("Needle");
                GameManager.Instance.GameOver("Falling");
            }
        }
        else
        {
            //Debug.Log("OFFGround");

            Player.GetComponent<PlayerController>().isNotGrounded();
        }
        
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        string tag = collision.transform.tag;

        switch (tag)
        {
            case "Lava" :
                GameManager.Instance.GameOver("Lava");
                break;
            case "Block":
                Player.GetComponent<PlayerController>().isGrounded();
                Ground = true;
                break;

            case "Needle":
                break;
           
            default:
             
                break;
        }
       
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        string tag = collision.transform.tag;

        switch (tag)
        {
            
            case "Block":
                Player.GetComponent<PlayerController>().isGrounded();
                Ground = false;

                break;

            default:
                
                break;
        }


    }


}
