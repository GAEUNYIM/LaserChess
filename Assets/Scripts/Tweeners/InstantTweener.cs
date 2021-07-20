using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantTweener : MonoBehaviour, IOobjectTweener
{
    public void MoveTo(Transform transform, Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
    public void RotationTo(Transform transform, Vector3 targetrotate)
    {
        transform.Rotate(targetrotate, Space.Self);
    }
}
