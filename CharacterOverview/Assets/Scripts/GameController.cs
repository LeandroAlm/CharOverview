using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject Character;
    void Start()
    {
        // Load Models
    }

    void Update()
    {
        
    }

    public void setAnimtion(int anim_id)
    {
        if (anim_id == 0)
        {
            Character.GetComponent<CharacterController>().stopAllAnimations();
        }
        else if (anim_id == 1)
        {
            Character.GetComponent<CharacterController>().PlayWalk();
        }
    }
}
