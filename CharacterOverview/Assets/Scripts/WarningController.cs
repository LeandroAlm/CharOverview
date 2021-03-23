using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    // Warning control, when trigger play an animation

    private float Animation_Tick, Animation_Duration, Animation_Percentage;
    private int Animation_ID;

    void Start()
    {
        ResetAllWarniongs();
    }

    void Update()
    {
        if(Animation_ID == 1)
        {
            Animation_Tick += Time.deltaTime;
            Animation_Percentage = Animation_Tick / Animation_Duration;

            if(Animation_Percentage >= 1.0)
            {
                Animation_Tick = 0.0f;
                Animation_Percentage = 0.0f;

                ResetAllWarniongs();
                Animation_ID = 0;
            }    
        }
    }

    private void ResetAllWarniongs()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void playWarningbyIDandTime(int warningID, float time)
    {
        // time in seconds
        Animation_Duration = time;
        Animation_Tick = 0.0f;
        Animation_Percentage = 0.0f;
        Animation_ID = 1;

        transform.GetChild(warningID).gameObject.SetActive(true);
    }
}
