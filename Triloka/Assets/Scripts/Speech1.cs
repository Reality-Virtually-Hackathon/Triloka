using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Speech1 : MonoBehaviour {

	DictationRecognizer speech_recog;
    bool started = false;


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
            Debug.Log("Dictation completed" + completionCause);
            started = false;
        };

        speech_recog.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        speech_recog.Start();
    }

    public void Update()
    {
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
	{
        if (text.ToLower().Equals("change")){
            speech_recog.Stop();
            FindObjectOfType<PullUpScript>().pullUp();
        }
	}
}
