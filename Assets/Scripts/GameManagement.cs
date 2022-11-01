using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [SerializeField]
    MapGenerator mapGenerator;
    public GameObject blackTile;
    public GameObject whiteTile;
    public GameObject blueTile;
    public GameObject redTile;
    [HideInInspector]
    public int numOfWhiteTiles=0;
    [HideInInspector]
    public int numOfBlueTiles=0;
    [HideInInspector]
    public int numOfRedTiles=0;
    PlayerController playerController;
    public Text scoreText;
    int score = 0;


   
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        GameObject[] whiteTiles = GameObject.FindGameObjectsWithTag("WhiteTile");
        for (int i = 0; i < whiteTiles.Length; i++)
        {
            numOfWhiteTiles++;
        }

        GameObject[] blueTiles = GameObject.FindGameObjectsWithTag("BlueTile");
        for (int i = 0; i <blueTiles.Length; i++)
        {
            numOfBlueTiles++;
        }

        GameObject[] redTiles = GameObject.FindGameObjectsWithTag("RedTile");
        for (int i = 0; i < redTiles.Length; i++)
        {
            numOfRedTiles++;
        }
 
        scoreText.text = "Score: " + score;
    }
 

    void Update()
    {
        if (numOfWhiteTiles == 0 && numOfBlueTiles == 0)
        {
            SceneManager.LoadScene("WinScene");

        }
        else if (numOfWhiteTiles == 0 && numOfRedTiles == 0)
        {
            SceneManager.LoadScene("WinScene");

        }
        else if (numOfBlueTiles == 0 && numOfRedTiles == 0)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public void RemoveTile(string color,Transform position)
    {
        if(color == "white")
        {
            Instantiate(blackTile, position.position, Quaternion.identity);
            numOfWhiteTiles--;
            score++;
            scoreText.text = "Score: " + score;
            if (numOfWhiteTiles == 0)
            {
                GameObject[] blackTiles = GameObject.FindGameObjectsWithTag("BlackTile");
                switch (playerController.firstColor)
                {
                    case "white":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(whiteTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                    case "blue":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(blueTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                    case "red":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(redTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                }

                int randomNum = Random.Range(0, 1);

                if (randomNum == 0)
                {
                    playerController.ChangeColor("blue");
                }
                else
                {
                    playerController.ChangeColor("red");
                }
            }
        }else if (color == "red")
        {
            score++;
            scoreText.text = "Score: " + score;
            Instantiate(blackTile, position.position, Quaternion.identity);
            numOfRedTiles--;
            if (numOfRedTiles == 0)
            {
                GameObject[] blackTiles = GameObject.FindGameObjectsWithTag("BlackTile");
                switch (playerController.firstColor)
                {
                    case "white":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(whiteTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                    case "blue":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(blueTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                    case "red":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(redTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                }

                int randomNum = Random.Range(0, 1);

                if (randomNum == 0)
                {
                    playerController.ChangeColor("blue");
                }
                else
                {
                    playerController.ChangeColor("white");
                }
            }
        }else
        {
            Instantiate(blackTile, position.position, Quaternion.identity);
            numOfBlueTiles--;
            score++;
            scoreText.text = "Score: " + score;
            if (numOfBlueTiles == 0)
            {
                GameObject[] blackTiles = GameObject.FindGameObjectsWithTag("BlackTile");
                switch (playerController.firstColor)
                {
                    case "white":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(whiteTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                    case "blue":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(blueTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                    case "red":
                        for (int i = 0; i < blackTiles.Length; i++)
                        {
                            Instantiate(redTile, blackTiles[i].transform.position, Quaternion.identity);
                            Destroy(blackTiles[i]);
                        }
                        break;
                }

                int randomNum = Random.Range(0, 1);

                if (randomNum == 0)
                {
                    playerController.ChangeColor("white");
                }
                else
                {
                    playerController.ChangeColor("red");
                }
            }
        }
       
    }


}
