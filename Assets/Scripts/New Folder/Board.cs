using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(SqueareSelectorCreator))]
[RequireComponent(typeof(RotationSelectorCreator))]
public class Board : MonoBehaviour
{
    public const int BOARD_SIZE = 8;

    [SerializeField] private Transform bottomLeftSquareTransform;
    [SerializeField] private float squareSize;

    private Piece[,] grid;
    private Piece selectedPiece;
    private ChessGameController chessController;
    private SqueareSelectorCreator squareSelector;
    private RotationSelectorCreator arrowSelector;

    private void Awake()
    {
        CreateGrid();
        squareSelector = GetComponent<SqueareSelectorCreator>();
        arrowSelector = GetComponent<RotationSelectorCreator>();
    }

    public void SetDependencies(ChessGameController chessController)
    {
        this.chessController = chessController;
    }

    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        if (checkIfCoordinatedArdOnBoard(coords))
            return grid[coords.x, coords.y];
        return null;
    }

    private void CreateGrid()
    {
        grid = new Piece[BOARD_SIZE, BOARD_SIZE];
    }

    public Vector3 CalculatePositionFromCoords(Vector2Int coords)
    {
        return bottomLeftSquareTransform.position + new Vector3(coords.x * squareSize, 0f, coords.y * squareSize);
    }

    private Vector2Int CalculateCoordsFromPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(transform.InverseTransformDirection(inputPosition).x / squareSize) + BOARD_SIZE / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformDirection(inputPosition).z / squareSize) + BOARD_SIZE / 2;
        return new Vector2Int(x, y);
    }

    public void OnGameRestarted()
    {
        selectedPiece = null;
        CreateGrid();
    }

    public void OnSquareSelected(Vector3 inputPosition)
    {
        //if (!chessController.IsGameInProgress())
        //    return;
        Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
        Piece piece = GetPieceOnsquare(coords);
        if (selectedPiece)
        {
            if (piece != null && selectedPiece == piece)
            {
                DeselectPiece();
            }            
                
            else if (selectedPiece.CanMoveTo(coords))
            {
                OnSelectedPieceMoved(coords, selectedPiece);
            }

            else if (piece != null && selectedPiece != piece && chessController.IsTeamTurnActive(piece.team))
            {
                SelectPiece(piece);
            }

        }
        else
        {
            if (piece != null && chessController.IsTeamTurnActive(piece.team))
            {
                SelectPiece(piece);
            }
                
        }
    }

    public void OnTextSelected(int tag)
    {
        if (tag == 1)
        {
            Debug.Log("left");
            selectedPiece.RotatePiece(new Vector3(0, -90, 0));
            DeselectPiece();
            EndTurn();
        }
        else{
            Debug.Log("Right");
            selectedPiece.RotatePiece(new Vector3(0, 90, 0));
            DeselectPiece();
            EndTurn();
        }
    }

    private void OnSelectedPieceMoved(Vector2Int coords, Piece piece)
    {
        //TryToTakeOppositePiece(coords);
        UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
        selectedPiece.MovePiece(coords);
        DeselectPiece();
        StartCoroutine(waiting());
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(1);
        EndTurn();
    }


    //private void TryToTakeOppositePiece(Vector2Int coords)
    //{
    //    Piece piece = GetPieceOnSquare(coords);
    //    if (piece != null && !selectedPiece.IsFromSameTeam(piece))
    //        TakePiece(piece);
    //}

    //private void TakePiece(Piece piece)
    //{
    //    if (piece)
    //    {
    //        grid[piece.occupiedSquare.x, piece.occupiedSquare.y] = null;
    //        chessController.OnPieceRemoved(piece);
    //    }
    //}

    public void SetPieceOnBoard(Vector2Int coords, Piece piece)
    {
        if (checkIfCoordinatedArdOnBoard(coords))
            grid[coords.x, coords.y] = piece;
    }

    private void EndTurn()
    {
        chessController.EndTurn();
    }

    public void UpdateBoardOnPieceMove(Vector2Int newCoords, Vector2Int oldCoords, Piece newPiece, Piece oldPiece)
    {
        grid[oldCoords.x, oldCoords.y] = oldPiece;
        grid[newCoords.x, newCoords.y] = newPiece;
    }

    private void SelectPiece(Piece piece)
    {
        //chessController.RemoveMovesEnablingAttackOnPieceOfType<Rking>(piece);
        selectedPiece = piece;
        List<Vector2Int> selection = selectedPiece.availableMoves;
        ShowSelectionSquares(selection);
        
    }


    private void ShowSelectionSquares(List<Vector2Int> selection)
    {
        Dictionary<Vector3, bool> squaresData = new Dictionary<Vector3, bool>();
        for(int i=0; i<selection.Count; i++)
        {
            Vector3 position = CalculatePositionFromCoords(selection[i]);
            bool isSquareFree = GetPieceOnSquare(selection[i]) == null;
            squaresData.Add(position, isSquareFree);
        }
        squareSelector.ShowSelection(squaresData);
        arrowSelector.ShowRotation(squaresData);
    }

    private void DeselectPiece()
    {
        selectedPiece = null;
        squareSelector.ClearSelection();
        arrowSelector.ClearRotation();
    }

    private Piece GetPieceOnsquare(Vector2Int coords)
    {
        if (checkIfCoordinatedArdOnBoard(coords))
            return grid[coords.x, coords.y];
        return null;
    }

    public bool checkIfCoordinatedArdOnBoard(Vector2Int coords)
    {
        if (coords.x < 0 || coords.y < 0 || coords.x >= BOARD_SIZE || coords.y >= BOARD_SIZE)
            return false;
        return true;
    }

    

    public bool HasPiece(Piece piece)
    {
        for(int i=0; i< BOARD_SIZE; i++)
        {
            for(int j=0; j < BOARD_SIZE; j++)
            {
                if (grid[i, j] == piece)
                    return true;
            }
        }
        return false;
    }
}
