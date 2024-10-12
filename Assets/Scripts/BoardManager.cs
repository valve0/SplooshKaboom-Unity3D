using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MonoBehaviour, IDisposable
{
    public GameObject Tile;
    public Material[] Materials;

    private GameObject[,] board;

    public GameObject[,] Board
    {
        get { return board; }
        private set { board = value; }
    }


    public GameObject Bombs;

    public GameObject squidIcons;

    private const int numTiles = 64;
    private const int numCols = 8;
    private const int numRows = 8;
    private List<GameObject> squidEventObjects;

    private float distanceBetweenTiles = 1F;

    private List<Squid> listOfSquids;

    public GameObject[,] GenerateBoard()
    {

        listOfSquids = new List<Squid>();
        squidEventObjects = new List<GameObject>();

        //Fill array of twoDSquares with new twoDSquares, setting them to default start value
        board = new GameObject[numRows, numCols];

        int ID = 0;

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                board[i, j] = CreateChildPrefab(Tile, gameObject,
                    transform.position.x,
                    transform.position.y - distanceBetweenTiles * i,
                    transform.position.z - distanceBetweenTiles * j);

                board[i, j].GetComponent<TileManager>().Bombs = Bombs;
                board[i, j].GetComponent<TileManager>().ID = ID;
                
                ID++;
            }
        }

        PlaceSquid(2);
        PlaceSquid(3);
        PlaceSquid(4);

        return board;

    }

    private void SquidKilled(object sender, EventArgs e)
    {
        SquidKilledEvent.Invoke(this, new EventArgs());
    }

    public event EventHandler SquidKilledEvent = delegate { };


    /// <summary>
    /// This method works out where to put each squid. It loops through
    /// the length of each squid putting the selected location for each part
    private void PlaceSquid(int length)
    {
        int orientation;
        int row;
        int col;

        while (true)
        {
            //generate new orientation (0 = up down, 1 = left right)
            System.Random Random = new System.Random();
            orientation = Random.Next(2);

            row = Random.Next(numRows); //Starting row
            col = Random.Next(numCols); //Starting col

            if (Fits(length, orientation, row, col))
                break;
        }

        //Create the squid
        Squid squid = new Squid(length);
        listOfSquids.Add(squid);
        squid.SquidKilled += SquidKilled;

        //Create refernce to squid object in relevant squares
        if (orientation == 0)
        {
            for (int i = 0; i < length; i++)
            {
                var tile = board[row, col + i];
                tile.GetComponent<TileManager>().Squid = squid;
            }  
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                var tile = board[row + i, col];
                tile.GetComponent<TileManager>().Squid = squid;
            }
        }
    }

    private bool Fits(int length, int orientation, int row, int col)
    {

        for (int i = 0; i < length; i++) //Loop through the length of the squid
        {
            if (orientation == 0)
            {
                if (col + i >= numCols) //Is is out of bounds? 
                    return false;


                if (board[row, col + i].GetComponent<TileManager>().Squid != null)          
                    return false;
            }
            else
            {

                if (row + i >= numRows)
                    return false;

                if (board[row + i, col].GetComponent<TileManager>().Squid != null)
                    return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject CreateChildPrefab(GameObject prefab, GameObject parent, float x, float y, float z)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), parent.transform.rotation);
        myPrefab.transform.parent = parent.transform;
        return myPrefab;
    }

    #region IDisposal

    private bool disposedValue;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {

                foreach (Squid squid in listOfSquids)
                {
                    squid.SquidKilled -= SquidKilled;
                }

            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }


    #endregion
}
