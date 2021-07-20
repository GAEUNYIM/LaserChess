using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    private bool shoot;
    public Material material;
    LaserBeam beam1;

    void FixedUpdate()
    {
        if (shoot)
        {
            Destroy(GameObject.Find("Laser Beam Red"));
            beam1 = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
        }
        else
        {
            Destroy(GameObject.Find("Laser Beam Red"));
        }
    }

    public void setShoot(bool flag)
    {
        shoot = flag;
    }
}
