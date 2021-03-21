using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject SoundBtt;
    private int trigger_sound;

    void Start()
    {
        trigger_sound = 1;
        if(PlayerPrefs.HasKey("t_sound"))
        {
            trigger_sound = PlayerPrefs.GetInt("t_sound");
        }
        UpdateSoundButtonImage();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }

    private void UpdateSoundButtonImage()
    {
        if (trigger_sound == 1)
        {
            SoundBtt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/soundOn");
        }
        else
        {
            SoundBtt.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/soundOff");
        }
    }

    public void onPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void onExitButton()
    {
        Application.Quit();
    }

    public void onSoundButton()
    {
        if(trigger_sound == 1)
        {
            trigger_sound = 0;
        }
        else
        {
            trigger_sound = 1;
        }

        UpdateSoundButtonImage();
        PlayerPrefs.SetInt("t_sound", trigger_sound);
    }

}
