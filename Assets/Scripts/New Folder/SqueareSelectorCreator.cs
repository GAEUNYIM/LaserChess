using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SqueareSelectorCreator : MonoBehaviour
{
    [SerializeField] private GameObject selectorPrefab;
    private List<GameObject> instantiatedSelectors = new List<GameObject>();

    public void ShowSelection(Dictionary<Vector3, bool> squareData)
    {
        ClearSelection();
        foreach(var data in squareData)
        {
            GameObject selector = Instantiate(selectorPrefab, data.Key, Quaternion.identity);
            instantiatedSelectors.Add(selector);
        }
    }

    public void ClearSelection()
    {
        foreach(var selector in instantiatedSelectors)
        {
            Destroy(selector.gameObject);
        }
        instantiatedSelectors.Clear();
    }
}
