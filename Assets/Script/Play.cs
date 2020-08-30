using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public GameObject WinObj, LossObj, PlayObj, can; 

    int speed = 5, velocity = 150, LevelNo, goal; //score; 
    float[] LevelSecs = { 30, 18, 10, 9, 6, 3 };
    int[] Target = { 3, 4, 5, 6, 7, 8 }; //{ 3, 5, 9, 10, 15, 30 };
    public Text ScoreBoard; 
    public GameObject ball, field;
    float time;
    float[] TimeLimit = new float[6];

    Rigidbody2D Player; 

    // Start is called before the first frame update
    void Start()
    {
        //goal = PlayerPrefs.GetInt("goal");

        Player = GetComponent<Rigidbody2D>();

        LevelNo = PlayerPrefs.GetInt("LevelNo",1);

        goal = PlayerPrefs.GetInt("goal", 0);

        //float RandX = Random.Range(50, Screen.width - 50);  //range of field set to generate clones
        //float RandY = Random.Range(50, Screen.height - 50);

        //Vector2 pos = new Vector2(RandX, RandY);
        //GameObject BallGen = Instantiate(ball, pos, Quaternion.identity);   //obj, pos, rotation given to prefab

        //BallGen.transform.SetParent(field.transform);   //parent given to prefab 

        for (int i=0; i< 6; i++)
        {

            TimeLimit[i] = 100f; //LevelSecs[i] * 10; 
         
        }

        InvokeRepeating("GenerateBall", 0f, LevelSecs[LevelNo-1]);    //call GenerateBall method at 2 sec and then repeatedly after 3 sec


    }

    public void GenerateBall()
    {
        float RandX = Random.Range(50, Screen.width - 50);  
        float RandY = Random.Range(50, Screen.height - 50);

        Vector2 pos = new Vector2(RandX, RandY);
        GameObject BallGen = Instantiate(ball, pos, Quaternion.identity);   

        BallGen.transform.SetParent(field.transform);

        //Destroy(BallGen, 10f);  //destroy ball automatically after 10 sec

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        print(Mathf.FloorToInt(time));

        goal = PlayerPrefs.GetInt("goal", goal);

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    float x = Mathf.Clamp(transform.position.x - speed, 50, Screen.width - 50); //to restrict the movement field of Player: pos var to control, min range, max 
        //    transform.position = new Vector2(x, transform.position.y);  //tranform the pos on key press
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    float x = Mathf.Clamp(transform.position.x + speed, 50, Screen.width - 50);
        //    transform.position = new Vector2(x, transform.position.y);
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    float y = Mathf.Clamp(transform.position.y + speed, 50, Screen.height - 50);
        //    transform.position = new Vector2(transform.position.x, y);
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    float y = Mathf.Clamp(transform.position.y - speed, 50, Screen.height - 50);
        //    transform.position = new Vector2(transform.position.x, y);
        //}


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Player.velocity = new Vector2(-100, 0); //just on once key press Player will move with speed of 100 in left direction  
            Player.velocity = Vector2.left * velocity;  //vector2.left = vector(-1,0) and * velocity means (-1,0)*150 
            //Player.AddForce(Vector2.left * velocity);   //add force in left direction of (-1,0)*velocity which is (-150,0) 
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Player.velocity = new Vector2(100, 0);
            Player.velocity = Vector2.right * velocity;
            //Player.AddForce(Vector2.right * velocity);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Player.velocity = new Vector2(0, 100);
            Player.velocity = Vector2.up * velocity;
            //Player.AddForce(Vector2.up * velocity);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Player.velocity = new Vector2(0, -100);
            Player.velocity = Vector2.down * velocity;
            //Player.AddForce(Vector2.down * velocity);
        }

        //if(Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    Player.velocity = Vector2.zero; //on release of key Player will stop & vector2.zero = (0,0) 
        //}
        //if (Input.GetKeyUp(KeyCode.RightArrow))
        //{
        //    Player.velocity = Vector2.zero;
        //}
        //if (Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    Player.velocity = Vector2.zero;
        //}
        //if (Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    Player.velocity = Vector2.zero;
        //}

        //PlayerPrefs.GetInt("goal");
        ScoreBoard.text = "GOAlS:" + goal + "";

        //PlayerPrefs.GetInt("score");
        //ScoreBoard.text = "SCORE:" + score + "";

        if (time >= TimeLimit[LevelNo-1])
        {
            if (goal >= Target[LevelNo-1])
            {
                can.GetComponent<CanvasGroup>().interactable = false;
                WinObj.SetActive(true);
                PlayObj.SetActive(false);
                print("win");
                LevelNo++; 
            }
            else
            {
                can.GetComponent<CanvasGroup>().interactable = false;
                LossObj.SetActive(true);
                PlayObj.SetActive(false);
                print("lose");
            }
        }
        else
        {
            if (goal >= Target[LevelNo - 1])
            {
                can.GetComponent<CanvasGroup>().interactable = false;
                WinObj.SetActive(true);
                PlayObj.SetActive(false);
                print("win");
                LevelNo++;
            }
        }

    }

    public void ReMatchBtn()
    {
        time = 0; 
        LossObj.SetActive(false);
        //PlayObj.SetActive(true);

        PlayerPrefs.SetInt("goal", 0);

        can.GetComponent<CanvasGroup>().interactable = true;

        SceneManager.LoadScene("Play");
    }

    public void NextMatchBtn()
    {
        WinObj.SetActive(false);
        PlayObj.SetActive(true);

        PlayerPrefs.SetInt("goal", 0);

        SceneManager.LoadScene("Play");
    }

    public void HomeBtn()
    {
        WinObj.SetActive(false);
        LossObj.SetActive(false);
        SceneManager.LoadScene("Home");
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //print(collision.gameObject.tag);


    //    if(collision.gameObject.tag == "ball")
    //    {
    //        //score += 1; //increase score after collection 

    //        //print("goal");

    //        //Destroy(collision.gameObject);  //destroy obj after collecting or collision 


    //        //PlayerPrefs.SetInt("score", score); 


    //        //GenerateBall(); //generate ball after collecting the current one 

    //        //collision.gameObject.SetActive(false); 
    //    }

    //}
    private void OnDestroy()
    {
        GameObject[] DestroyBall = GameObject.FindGameObjectsWithTag("ball"); 

        for(int i=0; i<DestroyBall.Length; i++)
        {
            Destroy(DestroyBall[i].gameObject);
            //print(i);
        }
 
        PlayerPrefs.DeleteKey("goal");
    }
}
