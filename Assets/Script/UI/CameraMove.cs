using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject HardCodingFormat;

    Vector3 CameraPos;

    // Start is called before the first frame update

    private void Awake()
    {
        CameraPos = this.gameObject.transform.position;

        int i_width = Screen.width;
        int i_height = Screen.height;
        Debug.Log(i_width + "" + i_height);
        HardCodingFormat.transform.localScale = new Vector3 (i_width/1440f, i_height/2960f);
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
        if (CameraPos.y < -741.849f) {
            CameraPos.y = -741.849f;
            this.gameObject.transform.position = CameraPos;
        }
        else
        {
            this.gameObject.transform.position = CameraPos;
        }

    }
}
