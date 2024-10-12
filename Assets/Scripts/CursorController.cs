using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public GameObject GameController;

    private const float cursorMovement = 1F;

    private const float boardSize = 7F;

    private static float maxY;
    private static float maxZ;
    private AudioSource audioSource;
    private const int maxRowIndex = 7;
    private const int maxColsIndex = 7;

    private float minY;
    private float minZ;

    private int rowIndex;
    private int columnIndex;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        maxY = transform.position.y;
        maxZ = transform.position.z;

        minY = maxY - boardSize;
        minZ = maxZ - boardSize;
    }

    // Update is called once per frame
    void Update()
    {
        var positionY = transform.position.y;
        var positionZ = transform.position.z;

        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            rowIndex = rowIndex <= 0 ? rowIndex = 0 : --rowIndex;
            audioSource.Play();
            positionY += cursorMovement;
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            rowIndex = rowIndex >= maxRowIndex ? rowIndex = maxRowIndex : ++rowIndex;
            audioSource.Play();
            positionY -= cursorMovement;
        }
        else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            columnIndex = columnIndex <= 0 ? columnIndex = 0 : --columnIndex;
            positionZ += cursorMovement;
            audioSource.Play();
        }
        else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            columnIndex = columnIndex >= maxColsIndex ? columnIndex = maxColsIndex : ++columnIndex;
            audioSource.Play();

            positionZ -= cursorMovement;
        }
        else if (Input.GetKeyDown("enter")|| Input.GetKeyDown("return"))
        {
            GameController.GetComponent<GameController>().SelectTile(rowIndex, columnIndex);
        }

        //Set limits on position
        if (positionY < minY)
        {
            positionY = minY;
        }
        if (positionY > maxY)
        {
            positionY = maxY;
        }

        if (positionZ < minZ)
        {
            positionZ = minZ;
        }
        if (positionZ > maxZ)
        {
            positionZ = maxZ;
        }

        transform.position = new Vector3(transform.position.x, positionY, positionZ);
    }
}
