using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    [SerializeField]
    bool LeftHand;


    GameObject Player;
    PlayerController playerController;
    bool RightHand;

    PlayerController.State Before;

    private void Awake()
    {
        RightHand = !(LeftHand);

    }

    private void Start()
    {
        Player = transform.parent.gameObject;
        playerController = Player.GetComponent<PlayerController>();
        Before = playerController.nowState;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ladder")
        {
            if (LeftHand)
            {
                Debug.Log("LeftHand");
                Player.GetComponent<PlayerController>().nowState = PlayerController.State.Leftvertical;
                

            }
            else if(RightHand)
            {
                Debug.Log("RightHand");
                Player.GetComponent<PlayerController>().nowState = PlayerController.State.Rightvertical;
            }
        }
    }
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ladder" )
        {
            if (LeftHand && !(Player.GetComponent<PlayerController>().nowState == PlayerController.State.Rightvertical))
            {
                Player.GetComponent<PlayerController>().nowState = PlayerController.State.Horizon;
            }
            else if (!LeftHand && !(Player.GetComponent<PlayerController>().nowState == PlayerController.State.Leftvertical))
            {
                Player.GetComponent<PlayerController>().nowState = PlayerController.State.Horizon;
            }
        }
    }


}
