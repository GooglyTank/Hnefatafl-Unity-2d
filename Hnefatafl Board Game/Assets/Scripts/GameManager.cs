using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject ChipA; //Attacker Chip
    public GameObject ChipD; // Defender Chip
    public GameObject Barrier; // Barrier Tile
    public GameObject ChipK; // King Chip

    public TextMeshProUGUI winText;
    public Button restartButton;

    public GameObject mainCam;
    public int width = 9;
    public int height = 9;

    public string GridType = "9x9";

    public int AttackerChipsNum = 1;
    public int DefenderChipsNum = 1;
    
    public GameObject StackA;
    public GameObject StackD;

    public int speed = 1;

    public GameObject ChipKA;

    public string CurrentTurn = "Defender";
    Dictionary<int, string> startTileLocA;
    Dictionary<int, string> startTileLocD;

    public Transform cam;

    public Tile tilePrefab;

    public float elapsedTime;

    


    private void Start() {
        if (GridType == "9x9") {
            AttackerChipsNum = 16;
            DefenderChipsNum = 9;
            width = 9;
            height = 9;
            startTileLocD = new Dictionary<int, string>(AttackerChipsNum) {
                [0] = "Tile 2 4",
                [1] = "Tile 3 4",
                [2] = "Tile 4 2",
                [3] = "Tile 4 3",
                [4] = "Tile 4 5",
                [5] = "Tile 4 6",
                [6] = "Tile 5 4",
                [7] = "Tile 6 4",
            };
            startTileLocA = new Dictionary<int, string>(DefenderChipsNum) {
                [0] = "Tile 0 3",
                [1] = "Tile 0 4",
                [2] = "Tile 0 5",
                [3] = "Tile 1 4",
                [4] = "Tile 3 0",
                [5] = "Tile 3 8",
                [6] = "Tile 4 0",
                [7] = "Tile 4 1",
                [8] = "Tile 4 7",
                [9] = "Tile 4 8",
                [10] = "Tile 5 0",
                [11] = "Tile 5 8",
                [12] = "Tile 7 4",
                [13] = "Tile 8 3",
                [14] = "Tile 8 4",
                [15] = "Tile 8 5",
            };
        }
        if (GridType == "11x11") {
            AttackerChipsNum = 24;
            DefenderChipsNum = 13;
            width = 11;
            height = 11;
            startTileLocA = new Dictionary<int, string>(AttackerChipsNum) {
                
            };
            startTileLocD = new Dictionary<int, string>(DefenderChipsNum) {

            };
        }
        
        GenerateGrid();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / speed;
        //ChipKA.transform.position = Vector3.Lerp(ChipKA.transform.position, GameObject.Find("EmptyTile").transform.position, percentageComplete);
        if (AttackerChipsNum <= 0) {
            restartButton.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
            winText.text = "Defenders Win!";
        }
        if (DefenderChipsNum <= 0) {
            restartButton.gameObject.SetActive(true);
            winText.gameObject.SetActive(true);
            winText.text = "Attackers Win!";
        }
    }

    public void ResetGame() {
        SceneManager.LoadScene("Main");
    }
    public void EndGame() {
        Application.Quit();
    }
    void GenerateGrid() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.TileX = x;
                spawnedTile.TileY = y;

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        cam.transform.position = new Vector3((float)width/2 -0.5f, (float)height/2 -0.5f, -10);

        for (int i = 0; i < AttackerChipsNum; i++) {
            Instantiate(ChipA, StackA.Transform).name = $"ChipA {i}";
            GameObject.Find($"ChipA {i}").transform.position.y = GameObject.Find($"ChipA {i}").transform.position.y +1*i;
            
            
        }
        for (int i = 0; i < DefenderChipsNum; i++) {
            Instantiate(ChipD);
        }
    }



}


