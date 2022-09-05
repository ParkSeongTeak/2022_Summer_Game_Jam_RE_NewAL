using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Head");

        if (collision.transform.gameObject.layer == 6  && GameManager.Instance.time2 && collision.transform.parent.name == "Tetromino")       //¸Ó¸®¿¡ Bloack GameOver
        {
            Debug.Log("Head_Die");

            GameManager.Instance.GameOver("BlockHead");

        }
    }

    
}
