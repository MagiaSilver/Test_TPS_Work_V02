using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

public class Player_Controller : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    private Vector3 direction;
    [SerializeField]
    private CharacterController characterController;
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
        Movement();
    }

    private void Movement()
    {
        Vector2 input = Vector3.zero;
        input = Input_Controller.instance.MoveAction.ReadValue<Vector2>(); ;
        direction = transform.forward * input.y +transform.right*input.x ;
        characterController.Move(direction* moveSpeed*Time.deltaTime);
    }
}