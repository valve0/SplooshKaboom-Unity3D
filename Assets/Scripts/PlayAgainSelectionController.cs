using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayAgainSelectionController : MonoBehaviour
{
    public GameObject LevelLoader;
    public GameObject YesArrow;
    public GameObject NoArrow;

    private AudioSource audioSource;
    private bool isYesSelected = true;
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelLoader = LevelLoader.GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && !isYesSelected)
        {
            audioSource.Play();
            YesArrow.SetActive(true);
            NoArrow.SetActive(false);
            isYesSelected = true;
        }
        else if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && isYesSelected)
        {
            audioSource.Play();
            YesArrow.SetActive(false);
            NoArrow.SetActive(true);
            isYesSelected = false;
        }
        else if (Input.GetKeyDown("enter") || Input.GetKeyDown("return"))
        {
            if (!isYesSelected)
            {
                Application.Quit();
            }
            else
            {
                levelLoader.ReplayGame();
            }
        }
    }
}
