using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color baseColor; //Even Color of Tile
    public Color offsetColor; //Odd color of Tile

    public SpriteRenderer _renderer; //Sprite render for changing color

    public GameObject ChipA; //Attacker Chip
    public GameObject ChipD; // Defender Chip
    public GameObject Barrier; // Barrier Tile
    public GameObject ChipK; // King Chip

    public int TileX; // Tiles x pos
    public int TileY; // Tiles y pos

    public GameObject TileAbove;
    public GameObject TileBelow;
    public GameObject TileRight;
    public GameObject TileLeft;

    public GameObject TileAbove2;
    public GameObject TileBelow2;
    public GameObject TileRight2;
    public GameObject TileLeft2;
    public static string CurrentTurn = "Defender"; //The current team thats current turn
    public static GameObject CurrentChipSelected; // Current chip selected
    public GameObject childName; // Name of child chip if there is one
    public bool hasBarrier; // Does this game object have a barrier?

    public GameObject gameManager;

    public void Init(bool isOffset) {
        _renderer.color = isOffset ? offsetColor : baseColor; // Adds a color to every odd and even tile.
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        GenerateStartingChips9x9(); //Generates the starting chips
        TileAbove = GameObject.Find($"Tile {TileX} {TileY + 1}");
        TileBelow = GameObject.Find($"Tile {TileX} {TileY - 1}");
        TileRight = GameObject.Find($"Tile {TileX + 1} {TileY}");
        TileLeft = GameObject.Find($"Tile {TileX - 1} {TileY}");

        TileAbove2 = GameObject.Find($"Tile {TileX} {TileY + 2}");
        TileBelow2 = GameObject.Find($"Tile {TileX} {TileY - 2}");
        TileRight2 = GameObject.Find($"Tile {TileX + 2} {TileY}");
        TileLeft2 = GameObject.Find($"Tile {TileX - 2} {TileY}");
    }
        private void Update() {
           if (transform.childCount > 0) { //Checks for any children
               if (transform.GetChild(0).name.Contains("Barrier")) { // Checks if the 1st child is a barrier
                   hasBarrier = true; //Sets barrier to true if it is
                   childName = transform.GetChild(0).gameObject; //Set game object to barrier so theres inst any annoying errors
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
                    //Tile.CurrentTurn = "Attacker";
                }
                if (childName.name.Contains("ChipK") && Tile.CurrentTurn == "Defender") { //If the chip is a King and if its the attackers turn
                    Tile.CurrentChipSelected = childName; //Sets global selected chip to this one
                }
                if (hasBarrier == true && Tile.CurrentChipSelected != null && Tile.CurrentChipSelected.name.Contains("ChipK") && !childName.name.Contains("ChipK")) { //Lets the King chip move on barriers and not others
                    Tile.CurrentChipSelected.transform.SetParent(gameObject.transform);
                    Tile.CurrentChipSelected.transform.position = gameObject.transform.position;
                }
            } else {
                if (Tile.CurrentChipSelected != null) { //If there is no children and a current selected chip when the mouse is clicked on a tile
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
                Debug.Log("Moving Pice");
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
                        Debug.Log("Moving piece");
                        Tile.CurrentChipSelected.transform.SetParent(gameObject.transform);
                        Tile.CurrentChipSelected.transform.position = gameObject.transform.position;
                        Debug.Log(Tile.CurrentChipSelected.transform.parent.name);
                        Tile.CurrentChipSelected = null;

                        CheckIfTakenPiece();
                }
        }
    }

    void CheckIfTakenPiece() {
        if (TileAbove != null && TileAbove.transform.childCount > 0) {
            Debug.Log("Is Child Above");
                if (TileAbove.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    Debug.Log("Is Child Above Enemy");
                    if (TileAbove2 != null && TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipA") || TileAbove2.transform.GetChild(0).name.Contains("ChipK") ) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileAbove.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    Debug.Log("Is Child Above Enemy");
                    if (TileAbove2 != null && TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileAbove.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Defender") { 
                    Debug.Log("Is Child Above Enemy");
                    if (TileAbove2 != null && TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().DefenderChipsNum = 0;
                    }
                }
                if (TileAbove.transform.GetChild(0).name.Contains("ChipD") || TileAbove.transform.GetChild(0).name.Contains("ChipA") || TileAbove.transform.GetChild(0).name.Contains("ChipK") ) {
                    if (TileAbove2.transform.childCount > 0 && TileAbove2.transform.GetChild(0).name.Contains("Barrier")) { 
                        Destroy(TileAbove.transform.GetChild(0).gameObject);
                        if (TileAbove.transform.GetChild(0).name.Contains("ChipK")) {
                            gameManager.GetComponent<GameManager>().DefenderChipsNum = 0;
                        }
                        if (TileAbove.transform.GetChild(0).name.Contains("ChipA")) {
                            gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                        }
                        if (TileAbove.transform.GetChild(0).name.Contains("ChipD")) {
                            gameManager.GetComponent<GameManager>().DefenderChipsNum = gameManager.GetComponent<GameManager>().DefenderChipsNum - 1;
                        }
                    }
                }
        }
        if (TileBelow != null && TileBelow.gameObject.transform.childCount > 0) {
            if (TileBelow.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    if (TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipA") || TileBelow2.transform.GetChild(0).name.Contains("ChipK") ) { 
                        Destroy(TileBelow.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileBelow.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    if (TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileBelow.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileBelow.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Defender") { 
                    if (TileBelow2.transform.childCount > 0 && TileBelow2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileBelow.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = 0;
                    }
                }
        }
        if (TileRight != null && TileRight.gameObject.transform.childCount > 0) {
            if (TileRight.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    if (TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipA") || TileRight2.transform.GetChild(0).name.Contains("ChipK") ) { 
                        Destroy(TileRight.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileRight.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    if (TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileRight.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileRight.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Defender") { 
                    if (TileRight2.transform.childCount > 0 && TileRight2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileRight.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = 0;
                    }
                }
        }
        if (TileLeft != null && TileLeft.gameObject.transform.childCount > 0) {
            if (TileLeft.transform.GetChild(0).name.Contains("ChipD") && Tile.CurrentTurn == "Attacker") { 
                    if (TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipA") || TileLeft2.transform.GetChild(0).name.Contains("ChipK") ) { 
                        Destroy(TileLeft.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileLeft.transform.GetChild(0).name.Contains("ChipA") && Tile.CurrentTurn == "Defender") { 
                    if (TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileLeft.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = gameManager.GetComponent<GameManager>().AttackerChipsNum - 1;
                    }
                }
                if (TileLeft.transform.GetChild(0).name.Contains("ChipK") && Tile.CurrentTurn == "Defender") { 
                    if (TileLeft2.transform.childCount > 0 && TileLeft2.transform.GetChild(0).name.Contains("ChipD")) { 
                        Destroy(TileLeft.transform.GetChild(0).gameObject);
                        gameManager.GetComponent<GameManager>().AttackerChipsNum = 0;
                    }
                }
        }
              if (Tile.CurrentTurn == "Attacker") {
                    Tile.CurrentTurn = "Defender";
                } 
            if (Tile.CurrentTurn == "Defender") {
                Tile.CurrentTurn = "Attacker";
            }
    }
    void GenerateStartingChips9x9 () {

        // A ton of if statements to place chips on the starting tiles could be more efficient. 
        if (gameObject.name == "Tile 0 0") {
            Instantiate(Barrier, gameObject.transform);
        }
                if (gameObject.name == "Tile 0 3") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 0 4") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 0 5") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 0 8") {
            Instantiate(Barrier, gameObject.transform);
        }
                if (gameObject.name == "Tile 1 4") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 2 4") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 3 0") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 3 4") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 3 8") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 0") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 1") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 2") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 3") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 4") {
            Instantiate(Barrier, gameObject.transform);
            Instantiate(ChipK, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 5") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 6") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 7") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 4 8") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 5 0") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 5 4") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 5 8") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 6 4") {
            Instantiate(ChipA, gameObject.transform);
        }
                if (gameObject.name == "Tile 7 4") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 8 0") {
            Instantiate(Barrier, gameObject.transform);
        }
                if (gameObject.name == "Tile 8 3") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 8 4") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 8 5") {
            Instantiate(ChipD, gameObject.transform);
        }
                if (gameObject.name == "Tile 8 8") {
            Instantiate(Barrier, gameObject.transform);
        }
    }
}

