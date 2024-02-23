using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Controller : MonoBehaviour
{
    public static Input_Controller instance;
    // Start is called before the first frame update
    [Header("Mobile Control")]
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction MoveAction;
    private InputAction RunAction;
    private InputAction JumpAction;
    private InputAction CrouchAction;
    private InputAction CrawlAction;

    private void Awake()
    {
        if(instance == null)
        instance = this;
    }
    void Start()
    {
        MoveAction = playerInput.actions["Move"];
        RunAction = playerInput.actions["Run"];
        JumpAction = playerInput.actions["Jump"];
        CrouchAction = playerInput.actions["Crouch"];
        CrawlAction = playerInput.actions["Crawl"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
