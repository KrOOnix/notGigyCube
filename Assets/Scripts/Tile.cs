using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{


    private void Start()
    {
;       
    }

    public void DestroyTile(string color)
    {
       GameManagement gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();
        gm.RemoveTile(color, transform);
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Offcut")
        {
            GameManagement gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();
            switch (gameObject.tag)
            {
                case "WhiteTile":
                    gm.numOfWhiteTiles--;
                    break;
                case "RedTile":
                    gm.numOfRedTiles--;
                    break;
                case "BlueTile":
                    gm.numOfBlueTiles--;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
