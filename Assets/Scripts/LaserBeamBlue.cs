using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBeamBlue
{

    Vector3 pos, dir;

    GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();

    public LaserBeamBlue(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam Blue";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.2f;
        this.laser.endWidth = 0.2f;
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.yellow;
        this.laser.tag = "BlueBeam";

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        //Debug.Log("CastRay");
        laserIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }
    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
            //StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        Debug.Log("waiter()");
        yield return new WaitForSeconds(2);
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
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        }

        if (attacked == "heartRTriKnight")
        {
            Rtriknight rtriknight = hitInfo.transform.GetComponentInParent<Rtriknight>();
            rtriknight.Die();
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        }

        if (attacked == "heartRRecKnight")
        {
            Rrectknight rrectknight = hitInfo.transform.GetComponentInParent<Rrectknight>();
            rrectknight.Die();
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        }

        // If Blue pieces are attacked,

        if (attacked == "heartBKing")
        {
            Bking bking = hitInfo.transform.GetComponentInParent<Bking>();
            bking.Die();
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        }

        if (attacked == "heartBTriKnight")
        {
            Btriknight btrinight = hitInfo.transform.GetComponentInParent<Btriknight>();
            btrinight.Die();
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        }

        if (attacked == "heartBRecKnight")
        {
            Brectknight brectknight = hitInfo.transform.GetComponentInParent<Brectknight>();
            brectknight.Die();
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        }

        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }
}

