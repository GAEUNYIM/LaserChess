
using UnityEngine;

public interface IOobjectTweener
{
    void MoveTo(Transform transform, Vector3 targetPosition);
    void RotationTo(Transform transform, Vector3 targetrotate);
}
