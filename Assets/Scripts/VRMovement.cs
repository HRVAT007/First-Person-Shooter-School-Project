using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean sprint;
    void Start()
    {
        
    }
    
    private void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        
        if(sprint.state)
        {
            speed = 5;
            transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
        }
        
        if (!sprint.state)
        {
            speed = 1;
            transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
        }

    }
}
