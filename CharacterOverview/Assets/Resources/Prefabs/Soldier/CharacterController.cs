using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public void stopAllAnimations()
    {
        gameObject.GetComponent<Animator>().SetBool("isWalking", false);
    }
    public void PlayWalk()
    {
        gameObject.GetComponent<Animator>().SetBool("isWalking", true);
    }
}
