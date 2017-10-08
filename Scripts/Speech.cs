using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Speech : MonoBehaviour {

	DictationRecognizer speech_recog;
    public Camera centerCamera;
    bool started = false;

    public GameObject[] storyTellers = new GameObject[2];


	// Use this for initialization
	void Start () {
        print("Starting...");
		speech_recog = new DictationRecognizer ();

		speech_recog.DictationResult += DictationRecognizer_DictationResult;
		speech_recog.AutoSilenceTimeoutSeconds = 3600f;

        speech_recog.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };

        speech_recog.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
            {
                Debug.Log("Dictation completed unsuccessfully: " + completionCause);
            }
            speech_recog.Start();
        };

        speech_recog.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

    }

    public void Update()
    {
        if (!started && this.GetComponent<Connection>().ready) { 
            speech_recog.Start();
            started = true;
        }

        Vector3 forward = centerCamera.transform.forward * 1000;
        Debug.DrawRay(centerCamera.transform.position, centerCamera.transform.forward, Color.green);


        Vector3 origin = centerCamera.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(origin, centerCamera.transform.forward, out hit, 1000))
        {
            if (hit.collider.gameObject.layer == 18)
            {
                foreach (GameObject g in storyTellers)
                {
                    if (hit.collider.gameObject.name.Equals(g.name))
                    {
                        hit.collider.gameObject.GetComponent<Talk>().playIntro();
                    }
                }
            }
        }
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
	{
        if (text.ToLower().Equals("change")){
            FindObjectOfType<PullUpScript>().pullUp();
        }
        // do something
        Vector3 origin = centerCamera.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(origin, centerCamera.transform.forward, out hit, 1000))
        {
            if(hit.collider.gameObject.name.Equals("WysaBot"))
            {
                this.GetComponent<Connection>().send(text);
                print(text);
            }
            else
            {
                foreach (GameObject g in storyTellers)
                {
                    if (hit.collider.gameObject.name.Equals(g.name))
                    {
                        stopStoryTells();
                        hit.collider.gameObject.GetComponent<Talk>().playStory();
                    }
                }
            }
        }
	}

    private void stopStoryTells()
    {
        foreach (GameObject g in storyTellers)
        {
            g.GetComponent<Talk>().stopAll();
        }
    }
}
