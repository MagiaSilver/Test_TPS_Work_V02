using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Controller : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    private Vector3 direction;
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float ground_Y_Offset;
    [SerializeField]
    private LayerMask groundMask;
    private Vector3 SpherePos;

    [SerializeField]
    private float gravity = -9.8f;
    private Vector3 Velocity;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            characterController = GetComponent<CharacterController>();
        }
        else
        {
            gameObject.GetComponent <Player_Controller>().enabled = false;
        }
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
    }

    private void Movement()
    {
        Vector2 input = Vector3.zero;
        input = Input_Controller.instance.MoveAction.ReadValue<Vector2>(); ;
        direction = transform.forward * input.y +transform.right*input.x ;
        characterController.Move(direction* moveSpeed*Time.deltaTime);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SpherePos, characterController.radius - 0.05f);
    }
}
