using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;

public class GetEmotions : MonoBehaviour {
    ToneAnalyzer _toneAnalyzer;
    public string _stringToTest;
    // Use this for initialization
    void Start () {
        Credentials credentials = new Credentials("078652c5-dafe-4774-8fa5-d651d3ffb1f2", "lMouYHxZVPjx", "https://gateway.watsonplatform.net/tone-analyzer/api");
        
        _toneAnalyzer = new ToneAnalyzer(credentials);


        _stringToTest = "I am feeling sad.";
        AnalyzeTone();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AnalyzeTone()
    {

        if (!_toneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, _stringToTest))
            print("Watson Tone Analyzer: Failed to analyze");
        else
            print(_stringToTest);
            print("working?");
    }

    public void OnGetToneAnalyze(ToneAnalyzerResponse resp, string data)
    {
        print(data);
    }
}
