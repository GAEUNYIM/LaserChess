using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSelectorCreator : MonoBehaviour
{
    [SerializeField] private GameObject rotatorleftPrefab;
    [SerializeField] private GameObject rotatorrightPrefab;
    private List<GameObject> instantiatedRotators = new List<GameObject>();

    public void ShowRotation(Dictionary<Vector3, bool> eee)
    {
        ClearRotation();
        GameObject rotator = Instantiate(rotatorleftPrefab, new Vector3(-7, 0, 0), rotatorleftPrefab.transform.rotation);
        instantiatedRotators.Add(rotator);
        GameObject rotator1 = Instantiate(rotatorrightPrefab, new Vector3(7, 0, 0), rotatorleftPrefab.transform.rotation);
        instantiatedRotators.Add(rotator1);
    }

    public void ClearRotation()
    {
        foreach (var rotator in instantiatedRotators)
        {
            Destroy(rotator.gameObject);
        }
        instantiatedRotators.Clear();
    }
}
