using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board Vars")]
    public int Width;
    public int Height;
    public GameObject TilePrefab;

    private BackgroundTile[,] allTiles;     //Holds the tiles and positions for the board

    // Start is called before the first frame update
    void Start()
    {
        //set the array to the proper dimensions
        allTiles = new BackgroundTile[Width, Height];

        //Set up the board
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Vector2 tempPos = new Vector2(i, j);
                GameObject backgroundtile = Instantiate(TilePrefab,tempPos, Quaternion.identity);
                backgroundtile.transform.parent = transform;
                backgroundtile.name = "( " + i + ", " + j + " )";
            }
        }
    }

 

}
