using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public AudioClip HitAudio;
    public AudioClip MissAudio;
    public AudioClip SquidKilledAudio;

    public AudioClip GameWinAudio;
    public AudioClip GameLoseAudio;

    public GameObject GameWinPrefab;
    public GameObject GameLosePrefab;
    public GameObject Cursor;
    public GameObject Squids;
    public GameObject BombManager;
    public GameObject BoardManager;
    private GameObject[,] board;

    public static int High_Score = 20;

    public static int BombsUsed;

    private const int TotalNumberOfSquids = 3;
    [SerializeField]
    private int TotalNumberOfBombs = 24;

    private AudioSource audioSource;

    private int squidsKilled;


    // Start is called before the first frame update
    void Start()
    {
        BombsUsed = 0;

        audioSource = GetComponent<AudioSource>();
        squidsKilled = 0;

        GenerateBoard();
    }

    public void GenerateBoard()
    {
        board = BoardManager.GetComponent<BoardManager>().GenerateBoard();
        BoardManager.GetComponent<BoardManager>().SquidKilledEvent += SquidKilled;

        Squids.GetComponent<SquidManager>().GenerateSquids(TotalNumberOfSquids);
        BombManager.GetComponent<BombManager>().GenerateBombs(TotalNumberOfBombs);

        //Debug.Log("Generated successfully");
    }

    private void SquidKilled(object sender, EventArgs e)
    {
        Squids.GetComponent<SquidManager>().KillSquid();

        audioSource.PlayOneShot(SquidKilledAudio, 0.7F);

        squidsKilled++;
        //Debug.Log(squidsKilled);

        if (squidsKilled == TotalNumberOfSquids)
        {
            //Debug.Log("WIN");
            GameWin();
        }
    }

    private void GameWin()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GameWinAudio, 0.7F);
        Cursor.SetActive(false);
        if (BombsUsed < High_Score)
            High_Score = BombsUsed;
        Instantiate(GameWinPrefab, new Vector3(464.8f,10,532.86f),
            Quaternion.Euler(-90f,0f,90f));
    }

    private void GameOver()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GameLoseAudio, 0.9F);
        Cursor.SetActive(false);
        Instantiate(GameLosePrefab, new Vector3(464.8f, 10, 532.86f),
            Quaternion.Euler(-90f, 0f, 90f));
    }

    public void SelectTile(int rowIndex, int ColIndex)
    {

        var tile = board[rowIndex, ColIndex].GetComponent<TileManager>();

        if (!tile.Selectable)
            return;

        try
        {
            if (tile.AttackTile())
            {
                audioSource.PlayOneShot(HitAudio, 0.7F);

            }
            else
            {
                audioSource.PlayOneShot(MissAudio, 0.7F);
            }

            BombsUsed++;
            if (BombsUsed == TotalNumberOfBombs)
                GameOver();

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }


}
