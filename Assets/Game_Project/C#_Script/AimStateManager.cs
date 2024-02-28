using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimStateManager : MonoBehaviour
{
    public Cinemachine.AxisState axisState_X, axisState_Y;
    public float X_Axis, Y_Axis;
    [SerializeField] private Transform camFollowPos;
    [SerializeField] private float RotationSwnsitivity = 8f;
    [SerializeField] private float smoothTime = 3f;
    Vector3 tagetRotation;
    Vector3 CurrentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camFollowPos != null)
        {
            
            if (Input_Controller.instance.CameraAction.IsInProgress())
            {
                #if UNITY_ANDROID
                Y_Axis += Input_Controller.instance.CameraAction.ReadValue<Vector2>().x * RotationSwnsitivity;
                X_Axis -= Input_Controller.instance.CameraAction.ReadValue<Vector2>().y * RotationSwnsitivity;
                #endif
                /*Y_axis += playerInput.actions["MouseX"].ReadValue<float>() * RotationSwnsitivity;
                X_axis -= playerInput.actions["MouseY"].ReadValue<float>() * RotationSwnsitivity;*/
                //#else
                //          Y_axis += Input.GetAxis("Mouse X") * RotationSwnsitivity;
                //          X_axis -= Input.GetAxis("Mouse Y") * RotationSwnsitivity;
                //#endif
                X_Axis = Mathf.Clamp(X_Axis, 0, 20);
            }

            tagetRotation = Vector3.Lerp(tagetRotation, new Vector3(X_Axis, Y_Axis), smoothTime);
            camFollowPos.eulerAngles = tagetRotation;

           // transform.position = Target.position - (transform.forward * ZoomValue);
        }
        //X_Axis = Input_Controller.instance.CameraAction.ReadValue<Vector2>().y * RotationSwnsitivity;
        //Y_Axis = Input_Controller.instance.CameraAction.ReadValue<Vector2>().x * RotationSwnsitivity;
    }
    private void LateUpdate()
    {
       
        //camFollowPos.localEulerAngles = new Vector3(Y_Axis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, X_Axis, transform.eulerAngles.y);
    }
}
