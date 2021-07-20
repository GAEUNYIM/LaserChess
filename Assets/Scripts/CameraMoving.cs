using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void TurntoBlue()
    {
        anim.Play("TurningCameraToBlue");
    }
    public void TurntoRed()
    {
        anim.Play("TurningCameraToRed");
    }
    
}
