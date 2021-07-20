using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(PieceCreator))]
[RequireComponent(typeof(PlayerInfoCreator))]
public class ChessGameController : MonoBehaviour
{
    //private enum GameState { Init, Play, Finished}
    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;
    [SerializeField] private ChessUIManager uIManager;

    private PieceCreator pieceCreator;
    private ChessPlayer redPlayer;
    private ChessPlayer bluePlayer;
    private ChessPlayer activePlayer;
    private PlayerInfoCreator playerSelector;
    private Animation cameraControll;
    //private GameState state;

    private void Awake()
    {
        SetDependencies();
        CreatePlayers();
    }

    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
        cameraControll = GetComponent<Animation>();
        playerSelector = GetComponent<PlayerInfoCreator>();
    }

    private void CreatePlayers()
    {
        redPlayer = new ChessPlayer(TeamColor.Red, board);
        bluePlayer = new ChessPlayer(TeamColor.Blue, board);
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        //SetGameState(GameState.Init);
        uIManager.HideUI();
        board.SetDependencies(this);
        CreatePiecesFromLayout(startingBoardLayout);
        activePlayer = bluePlayer;
        GenerateAllPossiblePlayerMoves(activePlayer);
        playerSelector.ShowPlayer(bluePlayer);
        //SetGameState(GameState.Play);
    }

    public void RestartGame()
    {
        DestroyPieces();
        board.OnGameRestarted();
        redPlayer.OnGameRestarted();
        bluePlayer.OnGameRestarted();
        StartNewGame();
    }

    private void DestroyPieces()
    {
        foreach(var p in redPlayer.activePieces)
        {
            if (p)
            {
                Destroy(p.gameObject);
            }
        }
        foreach (var p in bluePlayer.activePieces)
        {
            if (p)
            {
                Destroy(p.gameObject);
            }
        }
        //redPlayer.activePieces.ForEach(p => Destroy(p.gameObject));
        //bluePlayer.activePieces.ForEach(p => Destroy(p.gameObject));
    }

    //private void SetGameState(GameState state)
    //{
    //    this.state = state;
    //}

    //public bool IsGameInProgress()
    //{
    //    return state == GameState.Play;
    //}
    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for(int i=0; i<layout.GetPiecesCount(); i++)
        {
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);
            Angle angle = layout.GetSquareAngleAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords, team, type, angle);
        }
    }

    public bool IsTeamTurnActive(TeamColor team)
    {
        return activePlayer.team == team;
    }

    private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type type, Angle angle)
    {
        Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
        newPiece.SetData(squareCoords, team, board);
        if(angle==Angle.Right)
            newPiece.RotatePiece(new Vector3(0, 90, 0));
        else if(angle==Angle.Down)
            newPiece.RotatePiece(new Vector3(0, 180, 0));
        else if (angle == Angle.Left)
            newPiece.RotatePiece(new Vector3(0, -90, 0));

        board.SetPieceOnBoard(squareCoords, newPiece);

        ChessPlayer currentPlayer = team == TeamColor.Red ? redPlayer : bluePlayer;
        currentPlayer.AddPiece(newPiece);
    }

    private void GenerateAllPossiblePlayerMoves(ChessPlayer player)
    {
        player.GenerateAllPossibleMoves();
    }

    public void EndTurn()
    {
        GenerateAllPossiblePlayerMoves(activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
        //if (CheckIfGameIsFinished())
        Shoot(activePlayer);
        ChangeActiveTeam();
        //EndGame();
        //else
        //{
        //    ChangeActiveTeam();
        //}
    }

    //private bool CheckIfGameIsFinished()
    //{
    //    Piece[] kingAttckingPieces = activePlayer.GetPiecesAttackingOppositePieceOfType<Rking>();
    //    if(kingAttckingPieces.Length > 0)
    //    {
    //        ChessPlayer oppositePlayer = GetOpponentToPlayer(activePlayer);
    //        Piece attackedKing = oppositePlayer.GetPiecesOfType<Rking>().FirstOrDefault();
    //        oppositePlayer.RemovesEnablingAttackOnPiece<Rking>(activePlayer, attackedKing);

    //        int availableKingMoves = attackedKing.availableMoves.Count;
    //        if(availableKingMoves == 0)
    //        {
    //            bool canCoverKing = oppositePlayer.CanHidePieceFromAttack<Rking>(activePlayer);
    //            if (!canCoverKing)
    //                return true;
    //        }
    //    }
    //    return false;
    //}

    //public void OnPieceRemoved(Piece piece)
    //{
    //    ChessPlayer pieceOwner = (piece.team == TeamColor.Red) ? redPlayer : bluePlayer;
    //    pieceOwner.RemovePiece(piece);
    //    Destroy(piece.gameObject);
    //}

    public void EndGame(String player)
    {
        Debug.Log("EndGame");
        GenerateAllPossiblePlayerMoves(activePlayer); 
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
        uIManager.OnGameFinished(player);
    }

    private void ChangeActiveTeam()
    {
        if (activePlayer.team == redPlayer.team) {
            activePlayer = bluePlayer;
            playerSelector.ClearPlayer();
            playerSelector.ShowPlayer(bluePlayer);

        }
        else
        {
            activePlayer = redPlayer;
            playerSelector.ClearPlayer();
            playerSelector.ShowPlayer(redPlayer);
        }
    }

    public void Shoot(ChessPlayer activePlayer)
    {
        if (activePlayer == redPlayer)
        {
            Debug.Log("Shoot Red Laser");
            GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(true);

            StartCoroutine(TurnOff());
        }
        else
        {
            GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(true);
            Debug.Log("Shoot Blue Laser");
            
            StartCoroutine(TurnOffBlue());
        }
    }

    

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Turn Off");
        GameObject.Find("Rlaser(Clone)").GetComponent<ShootLaser>().setShoot(false);
        //GameObject.Find("Cube").GetComponent<CameraMoving>().TurntoRed();
    }

    IEnumerator TurnOffBlue()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Turn Off");
        GameObject.Find("Blaser(Clone)").GetComponent<ShootLaserBlue>().setShoot(false);
        //GameObject.Find("Cube").GetComponent<CameraMoving>().TurntoBlue();
    }

    private ChessPlayer GetOpponentToPlayer(ChessPlayer player)
    {
        return player.team == redPlayer.team ? bluePlayer : redPlayer;
    }

    //public void RemoveMovesEnablingAttackOnPieceOfType<T>(Piece piece) where T:Piece
    //{
    //    activePlayer.RemovesEnablingAttackOnPiece<T>(GetOpponentToPlayer(activePlayer), piece);
    //}
}


