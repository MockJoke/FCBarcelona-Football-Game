using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    int goal; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);

        goal = PlayerPrefs.GetInt("goal", goal);
        //print("goals :" + goal);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "post1" || collision.gameObject.tag == "post2")
        {
            goal += 1; //increase goal after collision  

            PlayerPrefs.SetInt("goal", goal);
            
            Destroy(this.gameObject);
        }

    }
}
