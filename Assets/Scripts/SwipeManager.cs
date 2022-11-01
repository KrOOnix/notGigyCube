using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public PlayerController player;
    private Vector2 startPos;
    public int minSwipeRange = 20;
    private bool touchDown = false;

    private void Update()
    {
       

        if (touchDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            touchDown = true;
        }

        if (touchDown)
        {
            if(Input.touches[0].position.y >= startPos.y + minSwipeRange)
            {
                touchDown = false;
                player.isSwipeUp = true;
            }else if (Input.touches[0].position.x <= startPos.x - minSwipeRange)
            {
                touchDown = false;
                player.isSwipeLeft = true;
            }
            else if (Input.touches[0].position.x >= startPos.x + minSwipeRange)
            {
                touchDown = false;
                player.isSwipeRight = true;
            }
            else if (Input.touches[0].position.y <= startPos.y - minSwipeRange)
            {
                touchDown = false;
                player.isSwipeDown = true;
            }
        }

        if (touchDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            touchDown = false;
        }

    }
}
