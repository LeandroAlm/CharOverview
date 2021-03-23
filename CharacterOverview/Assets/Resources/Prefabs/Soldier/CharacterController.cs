using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // All interections with char

    private GameObject GameController;
   
    private void Update()
    {
        // Need implement this for rotation fix
        if (gameObject.GetComponent<Animator>().GetBool("isShooting") == true && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shoot") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !gameObject.GetComponent<Animator>().IsInTransition(0))
        {
            shootTarget();
            stopAllAnimations();
        }
        if (gameObject.GetComponent<Animator>().GetBool("isShooting") == false && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0)
        {
            transform.GetChild(0).rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }
        //gameObject.GetComponent<Material>().
    }
    public void stopAllAnimations()
    {
        gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        gameObject.GetComponent<Animator>().SetBool("isShooting", false);
    }
    public void PlayWalk()
    {
        stopAllAnimations();
        gameObject.GetComponent<Animator>().SetBool("isWalking", true);
    }
    public void PlayShoot()
    {
        if(gameObject.GetComponent<Animator>().GetBool("isShooting") == false)
        {
            stopAllAnimations();
            gameObject.GetComponent<Animator>().SetBool("isShooting", true);
            transform.GetChild(0).RotateAround(transform.position, Vector3.up, 45);
        }
    }
    public void SetGameController(GameObject go)
    {
        GameController = go;
    }
    public void setNewMaterial(int matID)
    {
        Material tempMat = Resources.Load("Prefabs/Soldier/Materials/" + matID) as Material;
        for (int i = 1; i <= 6; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<Renderer>().material = tempMat;
        }
    }
    private void shootTarget()
    {
        Vector3 MagPos = new Vector3(0.21f, 1.46f, -8);
        Vector3 shootDir = (new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-0.5f, 2.5f), 10) - MagPos).normalized;
        RaycastHit hit;

        Physics.Raycast(MagPos, shootDir, out hit);

        GameController.GetComponent<GameController>().hitVerifyTarget(hit);
    }
}
