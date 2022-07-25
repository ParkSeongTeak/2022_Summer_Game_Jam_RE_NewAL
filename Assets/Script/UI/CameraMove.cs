using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    Vector3 CameraPos;
    
    // Start is called before the first frame update
    void Start()
    {
        CameraPos = this.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        CameraPos.y = Player.transform.position.y + GameManager.Instance.CameraPlayerHeight;
        this.gameObject.transform.position = CameraPos;
    }
}
