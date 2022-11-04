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

    public string CurrentTurn = "Defender";
    public GameObject CurrentChipSelected;
    public string childName;

    public void Init(bool isOffset) {
        _renderer.color = isOffset ? offsetColor : baseColor;
    }

    void Start()
    {
        GenerateStartingChips9x9();
        if (transform.childCount > 0) {
            childName = transform.GetChild(0).name;
        }

    }


    void OnMouseOver() {
        //Debug.Log(transform.name);
        if (Input.GetMouseButtonDown(0)) {
            if (transform.childCount > 0) {
                if (childName.Contains("ChipD") && CurrentTurn == "Defender") {
                    CurrentChipSelected = transform.GetChild(0);
                }
                if (childName.Contains("ChipA") && CurrentTurn == "Attacker") {
                    CurrentChipSelected = transform.GetChild(0);
                }
                if (childName.Contains("ChipK") && CurrentTurn == "Attacker") {
                    CurrentChipSelected = transform.GetChild(0);
                }
            } else {
                if (CurrentChipSelected != null) {
                    CurrentChipSelected.Parent = transform;
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
