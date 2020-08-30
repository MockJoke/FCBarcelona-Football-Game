using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    int  goal, LevelNo, i; //MaxLevel;
    public Button[] LevelBtns;
    public Sprite[] tick;

    public void Start()
    {
        LevelNo = PlayerPrefs.GetInt("LevelNo", 1);

        goal = PlayerPrefs.GetInt("goal", 0);

        //MaxLevel = PlayerPrefs.GetInt("MaxLevel");

        for (i = 0; i <= LevelNo; i++)
        {
            LevelBtns[i].interactable = true;
            LevelBtns[i].GetComponentInChildren<Text>().text = (i + 1).ToString();

            if (i == (LevelNo))
            {
                LevelBtns[i].GetComponent<Image>().sprite = null;
                LevelBtns[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

            else
            {
                if (PlayerPrefs.HasKey("skip_" + (i + 1)))
                {
                    LevelBtns[i].GetComponent<Image>().sprite = null;
                    LevelBtns[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                }
                else
                {
                    LevelBtns[i].GetComponent<Image>().sprite = tick[i];
                }
            }
        }
    }

    public void LevelBtnClick(int no)
    {
        PlayerPrefs.SetInt("LevelNo", no);
        SceneManager.LoadScene("Play");
    }
    
}
