using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{
    public Cinemachine.AxisState axisState_X, axisState_Y;
    [SerializeField] private Transform camFollowPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        axisState_X.Update(Time.deltaTime);
        axisState_Y.Update(Time.deltaTime);
    }
    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(axisState_Y.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, axisState_X.Value, transform.eulerAngles.y);
    }
}
