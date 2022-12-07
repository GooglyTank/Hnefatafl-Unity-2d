using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject Barrier; // Barrier Tile
    public GameObject ChipK; // King Chip

    public int TileX; // Tiles x pos
    public int TileY; // Tiles y pos

    public GameObject EmptyTile;

    public GameObject TileAbove;
    public GameObject TileBelow;
    public GameObject TileRight;
    public GameObject TileLeft;

    public GameObject TileAbove2;
    public GameObject TileBelow2;
    public GameObject TileRight2;
    public GameObject TileLeft2;

    
    public static string CurrentTurn = "Attacker";

    public static int AIChipCount;
    public static string AITeam;

    public string StartingTurn = CurrentTurn; //The current team thats current turn
    public static GameObject CurrentChipSelected; // Current chip selected
    public GameObject childName; // Name of child chip if there is one
    public bool hasBarrier; // Does this game object have a barrier?

    public GameObject gameManager;



    void Start()
    {
        EmptyTile = GameObject.Find("EmptyTile");
        TileAbove = GameObject.Find($"Tile {TileX} {TileY + 1}");
        TileBelow = GameObject.Find($"Tile {TileX} {TileY - 1}");
        TileRight = GameObject.Find($"Tile {TileX + 1} {TileY}");
        TileLeft = GameObject.Find($"Tile {TileX - 1} {TileY}");

        TileAbove2 = GameObject.Find($"Tile {TileX} {TileY + 2}");
        TileBelow2 = GameObject.Find($"Tile {TileX} {TileY - 2}");
        TileRight2 = GameObject.Find($"Tile {TileX + 2} {TileY}");
        TileLeft2 = GameObject.Find($"Tile {TileX - 2} {TileY}");
        gameManager = GameObject.Find("GameManager");

        if (TileAbove == null) {
            Debug.Log("Setting to null");
            TileAbove = EmptyTile;
        }
        if (TileBelow == null) {
            Debug.Log("Setting to null");
            TileBelow = EmptyTile;
        }
        if (TileLeft == null) {
            Debug.Log("Setting to null");
            TileLeft = EmptyTile;
        }
        if (TileRight == null) {
            Debug.Log("Setting to null");
            TileRight = EmptyTile;
        }
        if (TileAbove2 == null) {
            Debug.Log("Setting to null");
            TileAbove2 = EmptyTile;
        }
        if (TileBelow2 == null) {
            Debug.Log("Setting to null");
            TileBelow2 = EmptyTile;
        }
        if (TileLeft2 == null) {
            Debug.Log("Setting to null");
            TileLeft2 = EmptyTile;
        }
        if (TileRight2 == null) {
            Debug.Log("Setting to null");
            TileRight2 = EmptyTile;
        }

        if (gameManager.GetComponent<GameManager>().GridType == "11x11") {
            GenerateStartingChips11x11(); //Generates the starting chips
        }
        if (gameManager.GetComponent<GameManager>().GridType == "9x9") {
            GenerateStartingChips9x9(); //Generates the starting chips
        }
    }
        private void Update() {
           
           if (transform.childCount > 0) { //Checks for any children
               if (transform.GetChild(0).name.Contains("Barrier")) { // Checks if the 1st child is a barrier
                   hasBarrier = true; //Sets barrier to true if it is
                   childName = transform.GetChild(0).gameObject; //Set game object to barrier so theres isnt any annoying errors
                   if (transform.childCount == 2) { // Checks if there is 2 game objects
                       childName = transform.GetChild(1).gameObject; //Sets the child name to the second object which has to the chip
                   }
                 }
                 if (!transform.GetChild(0).name.Contains("Barrier")) { //Checks if there isnt a barrier
                childName = transform.GetChild(0).gameObject; //Sets the child name to the chip
                }
           }  
             
        }

    void OnMouseOver() { //Checks when the mouse is above this tile
        if (Input.GetMouseButtonDown(0)) { //Checks when a player clicks while being over this tile
            CheckTurnMove(); //Runs the CheckTurnMove function
        }
    }

    void CheckTurnMove () {
            if (transform.childCount > 0) { //Checks if any children on this tile
                if (childName.name.Contains("ChipD") && Tile.CurrentTurn == "Defender") { //If the chip is a Defender and if its the defenders turn
                    Tile.CurrentChipSelected = childName; //Sets global selected chip to this one
                }
                if (childName.name.Contains("ChipA") && Tile.CurrentTurn == "Attacker") { //If the chip is a Attacker and if its the attackers turn
                    Tile.CurrentChipSelected = childName; //Sets global selected chip to this one  
                }
                if (childName.name.Contains("ChipK") && Tile.CurrentTurn == "Defender") { //If the chip is a King and if its the attackers turn
                    Tile.CurrentChipSelected = childName; //Sets global selected chip to this one
                }
                if (hasBarrier == true && Tile.CurrentChipSelected != null && Tile.CurrentChipSelected.name.Contains("ChipK") && !childName.name.Contains("ChipK")) { //Lets the King chip move on barriers and not others
                    Tile.CurrentChipSelected.transform.SetParent(gameObject.transform);
                    Tile.CurrentChipSelected.transform.position = gameObject.transform.position;
                    gameManager.GetComponent<GameManager>().AttackerChipsNum = 0;
                }
            } else {
                if (Tile.CurrentChipSelected != null) { //If there are no children and there is a current selected chip when the mouse is clicked on a tile
                    CheckValidMove(); //Runs the CheckValidMove function
                }
            }
    }

    void CheckValidMove () {
         if (Tile.CurrentChipSelected != null && TileX == Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileX) { //Checks if the tiles x is equal to the Current selected chips parents x
             int BiggestNum = Mathf.Max(TileY, Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileY); //Finds which y is larger
             int SmallestNum = Mathf.Min(TileY, Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileY); //Finds which y is smaller
             bool CanMove = true; //Sets can move to true automatically 
             for(int i = SmallestNum; i < BiggestNum; i++) //Loops through every tile in between where the chip started and being moved to
                {
                    if (GameObject.Find($"Tile {TileX} {i}").gameObject.transform.childCount > 0 && GameObject.Find($"Tile {TileX} {i}").gameObject.transform.name != $"Tile {Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileX} {Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileY}") { //If any tiles in between has a child set can move to false 
                            CanMove = false;
                    } 
                }
                if (CanMove == true) { //Checks if can move is true
                        Tile.CurrentChipSelected.transform.SetParent(gameObject.transform); // Sets chips parent to the tile its moving to
                        Tile.CurrentChipSelected.transform.position = gameObject.transform.position; //Sets chips pos to the one its moving to
                        Debug.Log(Tile.CurrentChipSelected.transform.parent.name);
                        Tile.CurrentChipSelected = null; //Removes the selected chip so another can be selected

                        CheckIfTakenPiece(); //Runs the CheckIfTakePiece function
                }
         }
        if (Tile.CurrentChipSelected != null && TileY == Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileY) { // Same thing as the one above but if the tile y is equal instead of tile x
             int BiggestNum = Mathf.Max(TileX, Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileX);
             int SmallestNum = Mathf.Min(TileX, Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileX);
             bool CanMove = true;
             for(int i = SmallestNum; i < BiggestNum; i++)
                {
                    if (GameObject.Find($"Tile {i} {TileY}").gameObject.transform.childCount > 0 && GameObject.Find($"Tile {i} {TileY}").gameObject.transform.name != $"Tile {Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileX} {Tile.CurrentChipSelected.transform.parent.GetComponent<Tile>().TileY}") {
                            CanMove = false;
                    }
                }   
                if (CanMove == true) {
                        Tile.CurrentChipSelected.transform.SetParent(gameObject.transform);
                        Tile.CurrentChipSelected.transform.position = gameObject.transform.position;
                        Debug.Log(Tile.CurrentChipSelected.transform.parent.name);
                        Tile.CurrentChipSelected = null;

                        CheckIfTakenPiece();
                }
        }
    }

    void CheckIfTakenPiece() {
       
        if (TileAbove.transform.childCount > 0) {
            Debug.Log("Is Child Above");
                if (TileAbove.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    Debug.Log("Is Child Above Enemy");
                    if (TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipA") || TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = gameManager.GetComponent<GameManager>().DefenderChipsNum - 1;
                    }
                }
                if (TileAbove.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    Debug.Log("Is Child Above Enemy");
                    if (TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipD") || TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipK") || TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("Barrier") ) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileAbove.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Attacker") { 
                    Debug.Log("Is Child Above Enemy");
                    if (TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipA") || TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = 0;
                    }
                }
        }
        if (TileBelow.transform.childCount > 0) {
            if (TileBelow.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    if (TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipA") || TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileBelow.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = gameManager.GetComponent<GameManager>().DefenderChipsNum - 1;
                    }
                }
                if (TileBelow.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    if (TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipD") || TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipK") || TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileBelow.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileBelow.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Attacker") { 
                    if (TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipA") || TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileBelow.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = 0;
                    }
                }
        }
        if (TileRight.transform.childCount > 0) {
            if (TileRight.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    if (TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipA") || TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileRight.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = gameManager.GetComponent<GameManager>().DefenderChipsNum - 1;
                    }
                }
                if (TileRight.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    if (TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipD") || TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipK") || TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileRight.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileRight.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Attacker") { 
                    if (TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipA") || TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileRight.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = 0;
                    }
                }
        }
        if (TileLeft.transform.childCount > 0) {
            if (TileLeft.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    if (TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipA") || TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileLeft.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = gameManager.GetComponent<GameManager>().DefenderChipsNum - 1;
                    }
                }
                if (TileLeft.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    if (TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipD") || TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipK") || TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileLeft.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileLeft.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Attacker") { 
                    if (TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipA") || TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileLeft.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = 0;
                    }
                }
        }
        if (Tile.CurrentTurn == "Attacker") {
            Tile.CurrentTurn = "Defender";
        } else {
            Tile.CurrentTurn = "Attacker";
        }
    }

    

    void GenerateStartingChips9x9 () {
        //Generate all Barriers and King. 
        if (gameObject.name == "Tile 0 0") {
            Instantiate(Barrier, gameObject.transform);
        }
        if (gameObject.name == "Tile 0 8") {
            Instantiate(Barrier, gameObject.transform);
        }
        if (gameObject.name == "Tile 4 4") {
            Instantiate(Barrier, gameObject.transform);
            Instantiate(ChipK, gameObject.transform);
            
        }
        if (gameObject.name == "Tile 8 0") {
            Instantiate(Barrier, gameObject.transform);
        }
        if (gameObject.name == "Tile 8 8") {
            Instantiate(Barrier, gameObject.transform);
        }
    }
    void GenerateStartingChips11x11 () {
        //Generate all Barriers and King. 
        if (gameObject.name == "Tile 0 0") {
            Instantiate(Barrier, gameObject.transform);
        }
        if (gameObject.name == "Tile 0 10") {
            Instantiate(Barrier, gameObject.transform);
        }
        if (gameObject.name == "Tile 5 5") {
            Instantiate(Barrier, gameObject.transform);
            Instantiate(ChipK, gameObject.transform);
            
        }
        if (gameObject.name == "Tile 10 0") {
            Instantiate(Barrier, gameObject.transform);
        }
        if (gameObject.name == "Tile 10 10") {
            Instantiate(Barrier, gameObject.transform);
        }
    }
}


