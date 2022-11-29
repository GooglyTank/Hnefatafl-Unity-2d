using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public bool IsAITurn = true;
    public GameManager gameManager;
    
    public string StartingTurn;
    public string CurrentTurn;
    void Start()
    {
        gameManager = gameManager.GetComponent<GameManager>();
        CurrentTurn = gameManager.CurrentTurn;
        StartingTurn = CurrentTurn;
        Debug.Log(StartingTurn);
        Debug.Log(CurrentTurn);
    }

    void Update()
    {
        CurrentTurn = gameManager.CurrentTurn;
        if (StartingTurn != CurrentTurn && IsAITurn == true) {
            IsAITurn = false;
            AICheckIfCanTakePiece();
        }       
    }

    void AICheckIfCanTakePiece() {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach(GameObject i in allObjects) {
            int TileX = i.GetComponentInParent<Tile>().TileX;
            int TileY = i.GetComponentInParent<Tile>().TileY;
            if (i.name.Contains("ChipA") && CurrentTurn == "Attacker" || i.name.Contains("ChipD") && CurrentTurn == "Defender") {
                GameObject TileAbove = GameObject.Find($"Tile {TileX} {TileY + 1}");
                GameObject TileBelow = GameObject.Find($"Tile {TileX} {TileY - 1}");
                GameObject TileRight = GameObject.Find($"Tile {TileX + 1} {TileY}");
                GameObject TileLeft = GameObject.Find($"Tile {TileX - 1} {TileY}");

                GameObject TileAbove2 = GameObject.Find($"Tile {TileX} {TileY + 2}");
                GameObject TileBelow2 = GameObject.Find($"Tile {TileX} {TileY - 2}");
                GameObject TileRight2 = GameObject.Find($"Tile {TileX + 2} {TileY}");
                GameObject TileLeft2 = GameObject.Find($"Tile {TileX - 2} {TileY}");
            }
        }
    }

    void ChangeTurn() {

    }
}
