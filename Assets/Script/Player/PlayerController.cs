using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //debug용 public 만든 후 삭제할것
    public bool DebugGraund;
    public Vector3 DebugDir = new Vector3();






    CharacterController Player; // 제어할 캐릭터 컨트롤러
    public float Speed;  // 이동속도
    public float JumpPow;


    private float Gravity; // 중력   
    private Vector3 MoveDir; // 캐릭터의 움직이는 방향.

    private bool isPlayerGrounded;  //  최종 점프 버튼 눌림 상태
    private bool isJumpButtonPressing;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject.GetComponent<CharacterController>();

        // 기본값
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
        // 속도를 곱해서 적용합니다.
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
        // 캐릭터가 바닥에 붙어 있지 않다면
        else
        {
            // 중력의 영향을 받아 아래쪽으로 하강합니다.           
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
