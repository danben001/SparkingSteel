using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

	// Use this for initialization
	void Start () {

       
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlayWalkSound()
    {
        var AudioSource = GetComponent<AudioSource>();
        AudioSource.Play();

    }
}
