using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    Vector3 CameraPos;

    // Start is called before the first frame update

    private void Awake()
    {
        CameraPos = this.gameObject.transform.position;
       

    }

    void Start()
    {
        //Screen.SetResolution(1440, 2560, true);

        //Debug.Log("Hello _ World");
            
    }

    // Update is called once per frame
    void Update()
    {
        CameraPos.y = Player.transform.position.y + GameManager.Instance.CameraPlayerHeight;
        if (CameraPos.y < -741.79f) {
            CameraPos.y = -741.79f;
            this.gameObject.transform.position = CameraPos;
        }
        else
        {
            this.gameObject.transform.position = CameraPos;
        }

    }
}
