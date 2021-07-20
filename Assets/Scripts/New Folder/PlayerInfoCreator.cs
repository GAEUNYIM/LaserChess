using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoCreator : MonoBehaviour
{
    [SerializeField] private GameObject redPlayerPrefab;
    [SerializeField] private GameObject bluePlayerPrefab;
    private List<GameObject> instantiatedPlayerPrefab = new List<GameObject>();

    public void ShowPlayer(ChessPlayer player)
    {
        ClearPlayer();
        if(player.team == TeamColor.Red)
        {
            GameObject redPrefab = Instantiate(redPlayerPrefab, new Vector3(-7, 0, 4), redPlayerPrefab.transform.rotation);
            instantiatedPlayerPrefab.Add(redPrefab);
        }
        else if (player.team == TeamColor.Blue)
        {
            GameObject bluePrefab = Instantiate(bluePlayerPrefab, new Vector3(7, 0, -4), bluePlayerPrefab.transform.rotation);
            instantiatedPlayerPrefab.Add(bluePrefab);
        }

    }

    public void ClearPlayer()
    {
        foreach (var playerPrefab in instantiatedPlayerPrefab)
        {
            Destroy(playerPrefab.gameObject);
        }
        instantiatedPlayerPrefab.Clear();
    }
}
