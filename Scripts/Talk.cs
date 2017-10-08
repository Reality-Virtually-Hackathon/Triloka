using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour {

    public AudioSource intro;
    public AudioSource story;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playIntro()
    {
        if(!story.isPlaying && !intro.isPlaying)
            intro.Play();
    }
    public void playStory()
    {
        if (intro.isPlaying)
        {
            intro.Stop();
            intro.time = 0;
        }

        story.Play();
    }

    public void stopAll()
    {
        intro.Stop();
        story.Stop();
        intro.time = 0;
        story.time = 0;
    }
}
