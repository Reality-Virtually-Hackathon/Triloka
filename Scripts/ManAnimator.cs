using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAnimator : MonoBehaviour
{
    private Animation anim;
    public GameObject man;

    // Use this for initialization
    void Start()
    {
        anim = man.GetComponent<Animation>();
        Debug.Log("Initial");
        anim["Take 001"].speed = 0.35f;
        anim.PlayQueued("Take 001");

        anim.CrossFade("Take 001");
        StartCoroutine("back", "Take 001");
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator back(string name)
    {
        while (true) { 
            anim[name].wrapMode = WrapMode.PingPong;
            yield return new WaitForSeconds(anim[name].length * 2 / anim[name].speed);
            anim.Stop();

            yield return new WaitForSeconds(5);
            anim["Take 001"].speed = -anim["Take 001"].speed;

            anim.Play();
            Debug.Log("Play!");
        }

    }
}

