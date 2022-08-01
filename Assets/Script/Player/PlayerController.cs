using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    GameObject Foot;
    [SerializeField]
    GameObject Head;
    [SerializeField]
    GameObject Hands;

    //debug�� public ���� �� �����Ұ�
    //public bool DebugGraund;
    //public Vector3 DebugDir = new Vector3();


    CharacterController Player; // ������ ĳ���� ��Ʈ�ѷ�
    public float Speed;  // �̵��ӵ�
    public float JumpPow;


    private float Gravity; // �߷�   
    private Vector3 MoveDir; // ĳ������ �����̴� ����.

    private bool isPlayerGrounded;  //  ���� ���� ��ư ���� ����
    private bool isJumpButtonPressing;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject.GetComponent<CharacterController>();

        // �⺻��
        Speed = 5.0f;
        Gravity = 10.0f;
        MoveDir = Vector3.zero;
        JumpPow = 8.0f;
        isJumpButtonPressing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player == null) return;

       
        MoveDir.x = 0;
        if (GameManager.Instance.time1)
        {
            if (GameManager.Instance.MoveSliderValue() >= 0.7f)
            {
                MoveDir.x = 1;
            }
            else if (GameManager.Instance.MoveSliderValue() <= 0.3f)
            {
                MoveDir.x = -1;

            }
            
        }
        // �ӵ��� ���ؼ� �����մϴ�.
        MoveDir.x *= Speed;

        if (isPlayerGrounded || Player.isGrounded)
        {
            MoveDir.y = 0;
            MoveDir.z = 0;

            if (isJumpButtonPressing == true)
            {
                isJumpButtonPressing = false;
                
            }
        }
        // ĳ���Ͱ� �ٴڿ� �پ� ���� �ʴٸ�
        else
        {
            // �߷��� ������ �޾� �Ʒ������� �ϰ��մϴ�.           
            MoveDir.y -= Gravity * Time.deltaTime;
        }
        Player.Move(MoveDir * Time.deltaTime);
    }

    public void IsJumpButtonPressing()
    {
        if (isPlayerGrounded)
        {
            StartCoroutine("JumpStart");
            MoveDir.y = JumpPow;
            isJumpButtonPressing = true;
            isPlayerGrounded = false;
        }
            
    }
    public void isGrounded()
    {
        isPlayerGrounded = true;
    }
    public void isNotGrounded()
    {
        isPlayerGrounded = false;
    }

    IEnumerator JumpStart()
    {
        Debug.Log("Jumpstart_sequence");
        Foot.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        Foot.SetActive(true);
        Debug.Log("JumpEnd");




    }
}
