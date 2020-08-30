using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource BgMusic; 

    // Start is called before the first frame update
    void Start()
    {
        BgMusic = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
