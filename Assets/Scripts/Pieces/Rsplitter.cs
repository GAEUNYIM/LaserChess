using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rsplitter : Piece
{
    private Vector2Int[] directions = new Vector2Int[] { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };
    // Empty
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        float range = Board.BOARD_SIZE;
        foreach (var direction in directions)
        {
            Vector2Int nextCoords = occupiedSquare + direction;
            Piece piece = board.GetPieceOnSquare(nextCoords);
            if (!board.checkIfCoordinatedArdOnBoard(nextCoords))
                continue;
            if (piece == null)
                TryToAddMove(nextCoords);
        }
        return availableMoves;
    }
}
