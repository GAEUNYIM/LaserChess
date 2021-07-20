using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rlaser : Piece
{
    private Vector2Int[] directions = new Vector2Int[] { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    // Empty
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        return availableMoves;
    }
}
