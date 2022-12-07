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
                for (int xTile = 0; xTile > TileX - 8; xTile++) {
                    
                }
            }
        }
    }

    void ChangeTurn() {

    }
}
