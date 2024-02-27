using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Player_Controller : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float runSpeed = 5f;
    private Vector2 input = Vector3.zero;
    float X_Cal,Y_Cal;
    private Vector3 direction;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private CapsuleCollider capsuleCollider;

   [SerializeField]
    private float ground_Y_Offset;
    [SerializeField]
    private LayerMask groundMask;
    private Vector3 SpherePos;

    [SerializeField]
    private float gravity = -9.8f;
    private Vector3 Velocity;

    [SerializeField]
    private Transform LoookAt_Pos;
    private float CurentVeclocity;

    [SerializeField]
    private Player_Animator animator;
    private bool IsWalk,IsRun,IsCrouch,IsProne;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            characterController = GetComponent<CharacterController>();
            CameraManager.instance.SetLookAt(LoookAt_Pos);
        }
        else
        {
            gameObject.GetComponent <Player_Controller>().enabled = false;
        }
    }
    private void OnEnable()
    {
        Input_Controller.instance.CrouchAction.performed += CrouchActivate;
        Input_Controller.instance.ProneAction.performed += ProneActivate;
    }
    private void OnDisable()
    {
        Input_Controller.instance.CrouchAction.performed -= CrouchActivate;
        Input_Controller.instance.ProneAction.performed -= ProneActivate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input_Controller.instance.MoveAction.IsPressed())
        Movement();
        Gravity();
        X_Cal = Mathf.Lerp(X_Cal , input.normalized.x, 0.05f);
        Y_Cal = Mathf.Lerp(Y_Cal, input.normalized.y, 0.05f);
        animator.Direction_Input(X_Cal, Y_Cal);
        animator.WalkAnimation(IsWalk);
        animator.RunAnimation(IsRun);
        animator.CrouchAnimation(IsCrouch);
        animator.ProneAnimation(IsProne);
    }

    private void Movement()
    {  
        input = Input_Controller.instance.MoveAction.ReadValue<Vector2>(); ;
        direction = transform.forward * input.y + transform.right*input.x ;

        IsWalk = input!= Vector2.zero? true : false;

        float Speed;
        if (Input_Controller.instance.RunAction.IsPressed())
        {
            Speed = runSpeed;
            IsRun = true;
        }
        else
        {
            Speed = moveSpeed;
            IsRun = false;

        }

      
        characterController.Move(direction * Speed * Time.deltaTime);

        //float rotation = Mathf.Atan2(transform.eulerAngles.x, Mathf.Rad2Deg + LoookAt_Pos.transform.eulerAngles.y);
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, LoookAt_Pos.transform.eulerAngles.y, ref CurentVeclocity, 0.01f);

    }
    private bool IsGrounded()
    {
        SpherePos = new Vector3(transform.position.x, transform.position.y - ground_Y_Offset, transform.position.z);
        if (Physics.CheckSphere(SpherePos, characterController.radius - 0.05f, groundMask)) { return true; }
        return false;
    }
    private void Gravity()
    {
        if (!IsGrounded()) Velocity.y += gravity * Time.deltaTime;
        else if (Velocity.y < 0) Velocity.y = -2;

        characterController.Move(Velocity * Time.deltaTime);
    }

    private void CrouchActivate(InputAction.CallbackContext context)
    {
        Debug.LogError("CrouchActivate");
        if (context.performed && IsGrounded())
        {
            IsCrouch = !IsCrouch;
            IsProne = false;
            //playerAction.Crouch(isCrouch);
            //playerAction.Crawl(isCrawl);

            if (IsCrouch)
                Server_ChangeCollisionSize(this.gameObject, 0.4f, 0.86f, 1);
            else
                Server_ChangeCollisionSize(this.gameObject, 0.9f, 1.81f, 1);
        }
    }
    private void ProneActivate(InputAction.CallbackContext context)
    {
        Debug.LogError("CrouchActivate");
        if (context.performed && IsGrounded())
        {
            IsProne = !IsProne;
            IsCrouch = false;
            if (IsProne)Server_ChangeCollisionSize(this.gameObject, 0.2f, 1.4f, 2);
            else Server_ChangeCollisionSize(this.gameObject, 0.9f, 1.81f, 1);
        }
    }

    [ServerRpc]
    public void Server_ChangeCollisionSize(GameObject player, float y_center, float height, int direction)
    {
        Observers_ChangeCollisionSize(player, y_center, height, direction);
    }
    [ObserversRpc]
    public void Observers_ChangeCollisionSize(GameObject player, float y_center, float height, int direction)
    {
        player.GetComponent<Player_Controller>().capsuleCollider.height = height;
        player.GetComponent<Player_Controller>().capsuleCollider.center = new Vector3(0, y_center, 0);
        player.GetComponent<Player_Controller>().capsuleCollider.direction = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SpherePos, characterController.radius - 0.05f);
    }
}
