using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAnimator : MonoBehaviour {
	private Animation anim;
	public GameObject man;

	// Use this for initialization
	void Start () 
	{
		anim = man.GetComponent<Animation> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("Initial");
		anim ["Take 001"].speed = 0.35f;
		anim.PlayQueued ("Take 001");

		anim.CrossFade ("Take 001");
		StartCoroutine ("back", "Take 001");
			
	}

	IEnumerator back(string name)
	{
		anim [name].wrapMode = WrapMode.PingPong;
		yield return new WaitForSeconds (anim [name].length * 2 / anim [name].speed);
		anim.Stop ();

		anim [name].speed = -0.35f;
		anim.Play ();
		Debug.Log("Play!");

	}

}
