using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBeam
{

    Vector3 pos, dir;

    GameObject laserObj1;
    LineRenderer laser1;
    List<Vector3> laserIndices1 = new List<Vector3>();


    Material material;
    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser1 = new LineRenderer();
        this.laserObj1 = new GameObject();
        this.laserObj1.name = "Laser Beam Red";
        this.pos = pos;
        this.dir = dir;

        this.laser1 = this.laserObj1.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser1.startWidth = 0.2f;
        this.laser1.endWidth = 0.2f;
        this.laser1.material = material;
        this.laser1.startColor = Color.red;
        this.laser1.endColor = Color.yellow;

        CastRay(pos, dir, laser1);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices1.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices1.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser1.positionCount = laserIndices1.Count;

        foreach (Vector3 idx in laserIndices1)
        {
            laser1.SetPosition(count, idx);
            count++;

        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        String attacked = hitInfo.transform.name;

        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        if (hitInfo.collider.gameObject.tag == "Splitter")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, laser);

            pos = hitInfo.point;
            dir = direction;
            CastRay(pos, dir, laser);
        }

        // If Red pieces are attacked,

        if (attacked == "heartRKing")
        {
            Rking rking = hitInfo.transform.GetComponentInParent<Rking>();
            rking.Die();
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        }

        if (attacked == "heartRTriKnight")
        {
            Rtriknight rtriknight = hitInfo.transform.GetComponentInParent<Rtriknight>();
            rtriknight.Die();
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        }

        if (attacked == "heartRRecKnight")
        {
            Rrectknight rrectknight = hitInfo.transform.GetComponentInParent<Rrectknight>();
            rrectknight.Die();
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        }

        // If Blue pieces are attacked,

        if (attacked == "heartBKing")
        {
            Bking bking = hitInfo.transform.GetComponentInParent<Bking>();
            bking.Die();
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        }

        if (attacked == "heartBTriKnight")
        {
            Btriknight btrinight = hitInfo.transform.GetComponentInParent<Btriknight>();
            btrinight.Die();
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        }

        if (attacked == "heartBRecKnight")
        {
            Brectknight brectknight = hitInfo.transform.GetComponentInParent<Brectknight>();
            brectknight.Die();
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        }

        else
        {
            laserIndices1.Add(hitInfo.point);
            UpdateLaser();
        }
    }

   
}
        
    
