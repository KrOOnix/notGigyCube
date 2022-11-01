using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool isSwipeRight = false;
    [HideInInspector]
    public bool isSwipeLeft = false;
    [HideInInspector]
    public bool isSwipeUp = false;
    [HideInInspector]
    public bool isSwipeDown = false;
    [SerializeField]
    Material white;
    [SerializeField]
    Material red;
    [SerializeField]
    Material blue;
    MeshRenderer mr;
    Rigidbody rb;
    Transform movePoint;
    Vector3 currentPosition;
    Vector3 previousPosition;
    [HideInInspector]
    public bool isWall = false;
    [HideInInspector]
    public bool canMove = true;
    enum Color { white,red,blue};
    Color color;
    [HideInInspector]
    public string firstColor;
    bool canDie = false;
    private void Start()
    {

        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        color = (Color)Random.Range(0, 2);
        switch (color)
        {
            case Color.white:
                mr.material = white;
                color = Color.white;
                break;
            case Color.red:
                mr.material = red;
                color = Color.red;
                break;
            case Color.blue:
                mr.material = blue;
                color = Color.blue;
                break;
        }
        firstColor = color.ToString();
        Debug.Log(firstColor);
        movePoint = GameObject.FindGameObjectWithTag("move point").transform;
        
        currentPosition = movePoint.position;
        previousPosition = currentPosition;
        Debug.Log("CP: " + currentPosition);
        Debug.Log("PP: " + previousPosition);
    }


    private void Update()
    {

        if (isWall)
        {
            movePoint.position = previousPosition;
            canMove = true;
            currentPosition = movePoint.position;
            previousPosition = currentPosition;
            isWall = false;

        }

        if (isSwipeUp)
        {
            previousPosition = movePoint.position;
            movePoint.position = new Vector3(movePoint.position.x - 1f, 0.5f, movePoint.position.z);
            isSwipeUp = false;
            currentPosition = movePoint.position;

        }

        if (isSwipeDown)
        {
            previousPosition = movePoint.position;
            movePoint.position = new Vector3(movePoint.position.x + 1f, 0.5f, movePoint.position.z);
            isSwipeDown = false;
            currentPosition = movePoint.position;


        }

        if (isSwipeRight)
        {
            previousPosition = movePoint.position;
            movePoint.position = new Vector3(movePoint.position.x, 0.5f, movePoint.position.z + 1f);
            isSwipeRight = false;
            currentPosition = movePoint.position;

        }

        if (isSwipeLeft)
        {
            previousPosition = movePoint.position;
            movePoint.position = new Vector3(movePoint.position.x, 0.5f, movePoint.position.z - 1f);
            isSwipeLeft = false;
            currentPosition = movePoint.position;

        }

        rb.MovePosition(movePoint.position);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WhiteTile" && color == Color.white)
        {

            collision.gameObject.GetComponent<Tile>().DestroyTile(color.ToString());
            canDie = false;
            StartCoroutine(backTrack());

        }

        if (collision.gameObject.tag == "RedTile" && color == Color.red)
        {

            collision.gameObject.GetComponent<Tile>().DestroyTile(color.ToString());
            canDie = false;
            StartCoroutine(backTrack());
        }

        if (collision.gameObject.tag == "BlueTile" && color == Color.blue)
        {

            collision.gameObject.GetComponent<Tile>().DestroyTile(color.ToString());
            canDie = false;
            StartCoroutine(backTrack());
        }

        if(collision.gameObject.tag == "BlackTile" && canDie)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void ChangeColor (string colorName)
    {
        switch (colorName)
        {
            case "white":
                mr.material = white;
                color = Color.white;
                break;
            case "blue":
                mr.material = blue;
                color = Color.blue;
                break;
            case "red":
                mr.material = red;
                color = Color.red;
                break;
        }
    }

    IEnumerator backTrack()
    {
        yield return new WaitForSeconds(2f);
        canDie = true;
        
    }
   
}