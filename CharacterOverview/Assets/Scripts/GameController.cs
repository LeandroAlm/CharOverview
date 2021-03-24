using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Game Interection, controls all games clicks and trigger aniamtions, events or actions

    [SerializeField] GameObject CharPanel, MatPanel, AnimPanel, OpponentPanel, CharController, MatController, AnimController, ScoreGO, WarningGO;

    private GameObject Character, EnemyTarget;
    private bool CharIsOpen, AnimIsOpen, OpponentIsOpen, MatIsOpen;
    private int score;

    void Start()
    {
        // Load Models
        CharIsOpen = false;
        AnimIsOpen = false;
        OpponentIsOpen = false;
        MatIsOpen = false;
        score = 0;

        if(PlayerPrefs.GetInt("t_sound") == 1)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }

    public void setModelToLoad(string Model)
    {
        string path = "Prefabs/" + Model + "/Pref";
        
        if (Character != null)
        {
            GameObject.Destroy(Character);
        }

        GameObject tempGO = GameObject.Instantiate(Resources.Load(path) as GameObject, new Vector3(0, 0, -9), Quaternion.identity);
        tempGO.transform.name = "CharacterPlayer";
        Character = tempGO;
        Character.GetComponent<CharacterController>().SetGameController(gameObject, Model);

        int i = 0;
        foreach (Transform child in AnimPanel.transform)
        {
            if(child.GetComponent<Button>() != null)
            {
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                child.GetComponent<Button>().onClick.AddListener(() => setAnimtion(i));
                i++;
            }
        }

        CharPanel.SetActive(false);
        CharController.SetActive(false);
        AnimController.SetActive(true);
        MatController.SetActive(true);
    }

    public void setOpponentToLoad(string Model)
    {
        string path = "World/" + Model + "/Pref";

        if(EnemyTarget != null)
        {
            GameObject.Destroy(EnemyTarget);
        }

        GameObject tempGO = GameObject.Instantiate(Resources.Load(path) as GameObject, new Vector3(0, 0, 8), Quaternion.identity);
        tempGO.transform.name = "EnemyTarget";
        EnemyTarget = tempGO;
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
        else if (anim_id == 2)
        {
            Character.GetComponent<CharacterController>().PlayShoot();
        }
    }

    public void onCharButtonClick(GameObject btt)
    {
        if(CharIsOpen)
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Max");
        }
        else
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Min");
        }

        CharIsOpen = !CharIsOpen;
        CharPanel.SetActive(CharIsOpen);
    }

    public void onMatButtonClick(GameObject btt)
    {
        if (MatIsOpen)
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Max");
        }
        else
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Min");
        }

        MatIsOpen = !MatIsOpen;
        MatPanel.SetActive(MatIsOpen);
    }

    public void onOpponentButtonClick(GameObject btt)
    {
        if (OpponentIsOpen)
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Max");
        }
        else
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Min");
        }

        OpponentIsOpen = !OpponentIsOpen;
        OpponentPanel.SetActive(OpponentIsOpen);
    }

    public void onAnimButtonClick(GameObject btt)
    {
        if (AnimIsOpen)
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Max");
        }
        else
        {
            btt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Min");
        }

        AnimIsOpen = !AnimIsOpen;
        AnimPanel.SetActive(AnimIsOpen);
    }

    public void onMenuBttClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void onMatChoose(int matID)
    {
        Character.GetComponent<CharacterController>().setNewMaterial(matID);
    }

    public void hitVerifyTarget(RaycastHit hit)
    {
        // Instanciate bullet impact and auto destroy
        GameObject tempGO = Instantiate(Resources.Load("Prefabs/Hit") as GameObject, hit.point, Quaternion.identity);
        GameObject.Destroy(tempGO, 2.5f);


        if (hit.collider.CompareTag("Opponent") == true)
        {
            score++;
            ScoreGO.GetComponent<Text>().text = "Score: " + score;
            WarningGO.GetComponent<WarningController>().playWarningbyIDandTime(2, 1.0f);
        }
        else
        {
            if(EnemyTarget == null)
            {
                WarningGO.GetComponent<WarningController>().playWarningbyIDandTime(0, 2.0f);
            }
            else
            {
                WarningGO.GetComponent<WarningController>().playWarningbyIDandTime(1, 1.0f);
            }
        }
    }
}
