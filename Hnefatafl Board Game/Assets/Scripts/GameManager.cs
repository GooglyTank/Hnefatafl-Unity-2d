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
    public TextMeshProUGUI winText;
    public Button restartButton;

    public int width = 9;
    public int height = 9;

    public string GridType = "11x11";
    public int AttackerChipsNum = 1;
    public int DefenderChipsNum = 1;
    
    public GameObject StackA;
    public GameObject StackD;

    public int speed = 10;

    public string CurrentTurn;
    public string StartingTurn;
    Dictionary<int, string> startTileLocA;
    Dictionary<int, string> startTileLocD;

    public Transform cam;

    public Tile tilePrefab;

    int chipNumMoveA;
    int chipNumMoveD;

    int timesToPlayA;
    int timesToPlayD;

    public AudioSource clickSound;

    public bool AIEnabled;

    private void Start() {
        StartingTurn = CurrentTurn;
        AIEnabled = true;
        if (AIEnabled == true) {
            CurrentTurn = "Defender";
        } else {
            CurrentTurn = "Attacker";
        }
        if (GridType == "9x9") {
            AttackerChipsNum = 16;
            DefenderChipsNum = 9;
            timesToPlayA = AttackerChipsNum + 1;
            timesToPlayD = DefenderChipsNum;
            chipNumMoveA = -1;
            chipNumMoveD = -1;
            width = 9;
            height = 9;
            startTileLocD = new Dictionary<int, string>() {
                [0] = "Tile 2 4",
                [1] = "Tile 3 4",
                [2] = "Tile 4 2",
                [3] = "Tile 4 3",
                [4] = "Tile 4 5",
                [5] = "Tile 4 6",
                [6] = "Tile 5 4",
                [7] = "Tile 6 4",
            };
            startTileLocA = new Dictionary<int, string>() {
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
            UnityEngine.Camera.main.orthographicSize = 5.5f;
            AttackerChipsNum = 24;
            DefenderChipsNum = 13;
            timesToPlayA = AttackerChipsNum + 1;
            timesToPlayD = DefenderChipsNum;
            chipNumMoveA = -1;
            chipNumMoveD = -1;
            width = 11;
            height = 11;
            startTileLocA = new Dictionary<int, string>(AttackerChipsNum) {
                [0] = "Tile 0 3",
                [1] = "Tile 0 4",
                [2] = "Tile 0 5",
                [3] = "Tile 0 6",
                [4] = "Tile 0 7",
                [5] = "Tile 1 5",
                [6] = "Tile 3 10",
                [7] = "Tile 4 10",
                [8] = "Tile 5 10",
                [9] = "Tile 6 10",
                [10] = "Tile 7 10",
                [11] = "Tile 5 9",
                [12] = "Tile 10 7",
                [13] = "Tile 10 6",
                [14] = "Tile 10 5",
                [15] = "Tile 10 4",
                [16] = "Tile 10 3",
                [17] = "Tile 9 5",
                [18] = "Tile 3 0",
                [19] = "Tile 4 0",
                [20] = "Tile 5 0",
                [21] = "Tile 6 0",
                [22] = "Tile 7 0",
                [23] = "Tile 5 1",
            };
            startTileLocD = new Dictionary<int, string>(DefenderChipsNum) {
                [0] = "Tile 4 4",
                [1] = "Tile 4 5",
                [2] = "Tile 4 6",
                [3] = "Tile 5 6",
                [4] = "Tile 6 6",
                [5] = "Tile 6 5",
                [6] = "Tile 6 4",
                [7] = "Tile 5 4",
                [8] = "Tile 5 3",
                [9] = "Tile 3 5",
                [10] = "Tile 5 7",
                [11] = "Tile 7 5",
            };
        }
        
        GenerateGrid();
    }

    void Update()
    {
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
        CurrentTurn = Tile.CurrentTurn;
    }


    IEnumerator AnimateMoveA(int chipNumMove)
    {
        int timesToPlay = AttackerChipsNum;
        
        float journey = 0f;

            while (journey <= speed)
            {
                timesToPlay -= 1;
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / speed);
                GameObject.Find($"ChipA {chipNumMove}").transform.parent = GameObject.Find(startTileLocA[chipNumMove]).transform;
                GameObject.Find($"ChipA {chipNumMove}").transform.position = Vector3.Lerp(GameObject.Find($"ChipA {chipNumMove}").transform.position, GameObject.Find(startTileLocA[chipNumMove]).transform.position, percent);
                yield return null;
            }
        
    }
    IEnumerator AnimateMovePerA()
    {
        if (timesToPlayA == AttackerChipsNum+1) {
            timesToPlayA -= 1;
            yield return new WaitForSeconds(1f);
        }
        while (timesToPlayA > 0) {
        timesToPlayA -= 1;
        chipNumMoveA = chipNumMoveA +  1;
        StartCoroutine(AnimateMoveA(chipNumMoveA));
        clickSound.Play();
        yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator AnimateMoveD(int chipNumMove)
    {
        int timesToPlay = DefenderChipsNum;
        
        float journey = 0f;

            while (journey <= speed)
            {
                timesToPlay += 1;
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / speed);
                GameObject.Find($"ChipD {chipNumMove}").transform.parent = GameObject.Find(startTileLocD[chipNumMove]).transform;
                GameObject.Find($"ChipD {chipNumMove}").transform.position = Vector3.Lerp(GameObject.Find($"ChipD {chipNumMove}").transform.position, GameObject.Find(startTileLocD[chipNumMove]).transform.position, percent);
                yield return null;
            }
        
    }
    IEnumerator AnimateMovePerD()
    {
        if (timesToPlayD == DefenderChipsNum) {
            timesToPlayD -= 1;
            yield return new WaitForSeconds(1f);
        }
        while (timesToPlayD > 0) {
        timesToPlayD -= 1;
        chipNumMoveD =  chipNumMoveD + 1;
        StartCoroutine(AnimateMoveD(chipNumMoveD));
        clickSound.Play();
        yield return new WaitForSeconds(.25f);
        }
    }
    

    void MoveChipD()
    { 
        StartCoroutine(AnimateMovePerD());
    }
    void MoveChipA()
    { 
        StartCoroutine(AnimateMovePerA());
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
            }
        }

        cam.transform.position = new Vector3((float)width/2 -0.5f, (float)height/2 -0.5f, -10);

        for (int i = 0; i < AttackerChipsNum; i++) {
            int getINum = AttackerChipsNum - i - 1;
            Instantiate(ChipA, StackA.transform).name = $"ChipA {getINum}";
            GameObject CurrentChipA = GameObject.Find($"ChipA {getINum}");
            CurrentChipA.transform.position = new Vector3(CurrentChipA.transform.position.x , .35f*i, CurrentChipA.transform.position.z);
           if (i == AttackerChipsNum - 1) {
               MoveChipA();
           }
        }   

        for (int i = 0; i < DefenderChipsNum - 1; i++) {
            int getINum = DefenderChipsNum - i - 2;
            Instantiate(ChipD, StackD.transform).name = $"ChipD {getINum}";
            GameObject CurrentChipD = GameObject.Find($"ChipD {getINum}");
            CurrentChipD.transform.position = new Vector3(CurrentChipD.transform.position.x , .35f*i, CurrentChipD.transform.position.z);
           if (i == DefenderChipsNum - 2) {
               MoveChipD();
           }
        }
    }



}


