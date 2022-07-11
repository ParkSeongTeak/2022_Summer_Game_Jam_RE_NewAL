using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //debug�� public ���� �� �����Ұ�
    public bool DebugGraund;
    public Vector3 DebugDir = new Vector3();






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
        JumpPow = 5.0f;
        isJumpButtonPressing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player == null) return;
        DebugGraund = Player.isGrounded;
        if (GameManager.Instance.MoveSliderValue() >= 0.7f)
        {
            MoveDir.x = 1;
        }
        else if(GameManager.Instance.MoveSliderValue() <= 0.3f)
        {
            MoveDir.x = -1;

        }
        else
        {
            MoveDir.x = 0;
        }
        // �ӵ��� ���ؼ� �����մϴ�.
        MoveDir.x *= Speed;

        if (isPlayerGrounded)
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
        DebugDir = MoveDir;
        Player.Move(MoveDir * Time.deltaTime);
    }

    public void IsJumpButtonPressing()
    {
        if (isPlayerGrounded)
        {
            MoveDir.y = JumpPow;
            isJumpButtonPressing = true;
            isPlayerGrounded = false;
        }
            
    }
    public void isGrounded()
    {
        isPlayerGrounded = true;
    }

}
