using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width = 9;
    public int height = 9;

    public int AttackerChipsNum = 16;
    public int DefenderChipsNum = 9;

    public string CurrentTurn = "Defender";

    public Transform cam;

    public Tile tilePrefab;


    private void Start() {
        GenerateGrid();
    }

    void Update()
    {
        if (AttackerChipsNum <= 0) {
            Debug.Log("Game over Defenders Win!");
        }
        if (DefenderChipsNum <= 0) {
            Debug.Log("Game over Attackers Win!");
        }
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
    }

}
