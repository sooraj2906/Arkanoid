using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public List<GameObject> brick;

    [Header("Grid Details")]
    public int columns;
    public int rows;

    [Header("Power Ups")]
    public GameObject slowBallPowerup;
    public GameObject piercingBallPowerup;

    [Header("Spawn Probability")]
    [Range(0, 100)]
    public float brickProbabilityPercentage;
    [Range(0, 100)]
    public float powerUpProbabilityPercentage;

    private List<GameObject> tiles = new List<GameObject>();

    void Start()
    {
        GameManager.pInstance.OnStart += InitTiles;
    }

    /// <summary>
    ///Creates a grid with the given width and height
    /// </summary>
    /// <param name="width">width of the grid</param>
    /// <param name="height">height of the grid</param>
    public void PlaceBricks(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Probability of spawning a brick in the given spot on the grid
                if (Random.value <= brickProbabilityPercentage / 100f)
                {
                    //Spawns any one of the 7 grids randomly
                    GameObject tile = Instantiate(brick[Random.Range(0, 7)], this.transform);
                    tiles.Add(tile);
                    tile.transform.localPosition = new Vector3(x * 1.5f, y * 0.75f, 0);
                    tile.GetComponent<BrickController>().hits = 0;
                    //10% chance of spawning a power up in this brick
                    if (Random.value <= powerUpProbabilityPercentage / 100f)
                    {
                        //Each powerup is given a probability to spawn
                        if (Random.value <= 0.5f)
                            tile.GetComponent<BrickController>().powerup = slowBallPowerup;
                        else
                            tile.GetComponent<BrickController>().powerup = piercingBallPowerup;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Initializes the tiles and places the bricks with reference to the grid
    /// </summary>
    private void InitTiles()
    {
        ClearTiles();
        print("init tiles");
        PlaceBricks(columns, rows);
    }
    /// <summary>
    /// Loops through the existing list of tiles and removes all the tiles from the scene
    /// </summary>
    private void ClearTiles()
    {
        foreach(GameObject obj in tiles)
        {
            Destroy(obj);
        }
        tiles.Clear();
    }

    private void OnDestroy()
    {
        if (GameManager.pInstance != null)
        {
            GameManager.pInstance.OnStart -= InitTiles;
        }
    }
}
