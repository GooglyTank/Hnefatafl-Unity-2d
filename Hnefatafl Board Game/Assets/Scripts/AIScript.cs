using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public bool IsAITurn = true;
    public GameObject gameManagerObj;
    public GameManager gameManager;

    public bool AIEnabled;

    public string StartingTurn;
    public string CurrentTurn;
    void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        CurrentTurn = gameManager.CurrentTurn;
        StartingTurn = CurrentTurn;
        Debug.Log(StartingTurn);
        Debug.Log(CurrentTurn);
    }

    void LateUpdate()
    {
        CurrentTurn = gameManager.CurrentTurn;
        AIEnabled = gameManager.AIEnabled;
        if (StartingTurn != CurrentTurn && IsAITurn == true) {
            IsAITurn = false;
            Debug.Log("AIs Turn!");
            RunTurnAI();
        }       
    }

    void RunTurnAI() {
        if (AIEnabled == true) {
        CheckIfCanTakeChip();
        }
    }

    void CheckIfCanTakeChip() {
       GameObject[] ChipObjects = GameObject.FindGameObjectsWithTag("ChipA");
        foreach (GameObject Chip in ChipObjects) {
            GameObject ChipsTile = Chip.transform.parent.gameObject;
            Tile tileScript = ChipsTile.GetComponent<Tile>();
             for (int i = tileScript.TileY; i > 0; i--) {//UP
                if (GameObject.Find($"Tile {tileScript.TileX} {i}").gameObject.transform.childCount > 0) {
                    int tileChildCount = GameObject.Find($"Tile {tileScript.TileX} {i}").gameObject.transform.childCount;
                    if (tileChildCount == 1 && !GameObject.Find($"Tile {tileScript.TileX} {i}").gameObject.transform.GetChild(0).name.Contains("Barrier")) {
                        Debug.Log($"Chip {ChipsTile.name} found a chip on Tile {tileScript.TileX} {i}");
                    }
                    if (tileChildCount == 2) {
                    }
                }
            }
            for (int i = tileScript.TileY; i <= 9; i++) {//DOWN
                
            }
            for (int i = tileScript.TileX; i >= 0; i--) {//LEFT
                
            }
            for (int i = tileScript.TileX; i <= 9; i++) {//RIGHT
                
            } 
            
        }
    }

    void ChangeTurn() {

    }
}
