using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject Prefab;
    public Material[] Materials;

    private GameObject[] bombs;
    private Queue<GameObject> queueOfBombs;

    private const int maxNumCols = 3;
    private const int maxNumRows = 8;

    private readonly float distanceBetweenBombs = 1F;

    public static int BombsUsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        queueOfBombs = new Queue<GameObject>();
    }


    //call this method with any prefab index and any material index
    void SetMaterial(GameObject bomb, string bombState)
    {

        int materialIndex = 0;

        if (bombState == "start")
        {
            materialIndex = 0;
        }
        else if (bombState == "used")
        {
            materialIndex = 1;
        }
        else
        {
            Debug.LogError("Invalid state", this);
        }

        bomb.GetComponent<Renderer>().material = Materials[materialIndex];
    }

    public void UseBomb()
    {
        if (queueOfBombs.Count == 0)
            return;

        SetMaterial(queueOfBombs.Dequeue(), "used");
    }

    private GameObject CreateChildPrefab(GameObject prefab, GameObject parent, float x, float y, float z)
    {
        var myPrefab = Instantiate(prefab, new Vector3(x, y, z), parent.transform.rotation);
        myPrefab.transform.parent = parent.transform;
        return myPrefab;
    }

    public void GenerateBombs(int numberOfBombs)
    {

        bombs = new GameObject[numberOfBombs];

        int bombIndex = 0;

        //Top right value is 0,0 going down column by column
        //Follow way in which bombs are "used"- easier to queue like this

        for (int i = 0; i < maxNumCols; i++)
        {
            for (int j = 0; j < maxNumRows; j++)
            {
                // create a new skyscraper from prefab selection at right edge of screen
                bombs[bombIndex] = CreateChildPrefab(Prefab, gameObject,
                    transform.position.x,
                    transform.position.y - distanceBetweenBombs * j,
                    transform.position.z + distanceBetweenBombs * i);

                SetMaterial(bombs[bombIndex], "start");

                queueOfBombs.Enqueue(bombs[bombIndex]);

                bombIndex++;

                if (bombIndex == numberOfBombs)
                    return;

            }
        }
    }
}
