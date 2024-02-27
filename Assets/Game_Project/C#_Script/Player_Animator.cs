using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Direction_Input(float H_Input, float V_Input)
    {
        animator.SetFloat("H_Input", H_Input);
        animator.SetFloat("V_Input", V_Input);
    }
    public void WalkAnimation(bool value)
    {
        animator.SetBool("IsWalk", value);
    }
    public void RunAnimation(bool value)
    {
        animator.SetBool("IsRun", value);
    }
    public void CrouchAnimation(bool value)
    {
        animator.SetBool("IsCrouch", value);
    }
    public void ProneAnimation(bool value)
    {
        animator.SetBool("IsProne", value);
    }
}
