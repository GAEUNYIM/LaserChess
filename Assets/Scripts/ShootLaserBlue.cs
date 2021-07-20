using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserBlue : MonoBehaviour
{
    private bool shoot;
    public Material material;
    LaserBeamBlue beam;
    void Update()
    {
        if (shoot)
        {
            Destroy(GameObject.Find("Laser Beam Blue"));
            beam = new LaserBeamBlue(gameObject.transform.position, gameObject.transform.right, material);
        }
        else
        {
            Destroy(GameObject.Find("Laser Beam Blue"));
        }
    }

    public void setShoot(bool flag)
    {
        shoot = flag;
    }
}
