using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color baseColor;
    public Color offsetColor;

    public SpriteRenderer _renderer;

    public GameObject ChipA;
    public GameObject ChipD;
    public GameObject Barrier;
    public GameObject ChipK;

    public static string CurrentTurn = "Defender";
    public static GameObject CurrentChipSelected;
    public string childName;

    public void Init(bool isOffset) {
        _renderer.color = isOffset ? offsetColor : baseColor;
    }

    void Start()
    {
        GenerateStartingChips9x9();
        

    }
        private void Update() {
            if (transform.childCount > 0) {
                    childName = transform.GetChild(0).name;
                }
        }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log(Tile.CurrentChipSelected);
            if (transform.childCount > 0) {
                if (childName.Contains("ChipD") && Tile.CurrentTurn == "Defender") {
                    Debug.Log("Changed to D");
                    Tile.CurrentChipSelected = transform.GetChild(0).gameObject;
                    Debug.Log(CurrentChipSelected.name);
                    Tile.CurrentTurn = "Defender";
                }
                if (childName.Contains("ChipA") && Tile.CurrentTurn == "Attacker") {
                    Debug.Log("Changed to A");
                    Tile.CurrentChipSelected = transform.GetChild(0).gameObject;
                    Debug.Log(CurrentChipSelected.name);   
                    Tile.CurrentTurn = "Attacker";
                }
                if (childName.Contains("ChipK") && Tile.CurrentTurn == "Attacker") {
                    Debug.Log("Changed to King");
                    Tile.CurrentChipSelected = transform.GetChild(0).gameObject;
                    Debug.Log(CurrentChipSelected.name);
                    Tile.CurrentTurn = "Attacker";
                }
            } else {
                if (CurrentChipSelected != null) {
                    Debug.Log("Moving piece");
                    Tile.CurrentChipSelected.transform.SetParent(gameObject.transform);
                    Tile.CurrentChipSelected.transform.position = gameObject.transform.position;
                    Debug.Log(Tile.CurrentChipSelected.transform.parent.name);
                    Tile.CurrentChipSelected = null;
                    if (Tile.CurrentTurn == "Attacker") {
                        Tile.CurrentTurn = "Defender";
                    } else {
                        Tile.CurrentTurn = "Attacker";
                    }
                    
                }
            }
        }
    }
    void GenerateStartingChips9x9 () {

        // IGNORE THIS ILL FIX IT LATER
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
