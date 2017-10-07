using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Utilities;

public class GetEmotions : MonoBehaviour {
    ToneAnalyzer _toneAnalyzer;
    public string _stringToTest;
    public float result_score;
    public bool analyzingFinish;
    // Use this for initialization
    void Start () {
        Credentials credentials = new Credentials("078652c5-dafe-4774-8fa5-d651d3ffb1f2", "lMouYHxZVPjx", "https://gateway.watsonplatform.net/tone-analyzer/api");
        
        _toneAnalyzer = new ToneAnalyzer(credentials);


        AnalyzeTone("this is sad.");
        AnalyzeTone("we should be happy.");
        AnalyzeTone("this should work now.");
        AnalyzeTone("i dont feel good anymore.");
        AnalyzeTone("this is not right.");
        AnalyzeTone("it did not work out well.");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AnalyzeTone(string _stringToTest)
    {
        if (!_toneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, _stringToTest))
            print("Watson Tone Analyzer: Failed to analyze");
      
    }

    public void OnGetToneAnalyze(ToneAnalyzerResponse resp, string data)
    {

        float score_sadness = get_emotion_score(data, "sadness");
        float score_analytical = get_emotion_score(data, "analytical");
        float score_anger = get_emotion_score(data, "anger");
        float score_confident = get_emotion_score(data, "confident");
        float score_tentative = get_emotion_score(data, "tentative");
        float score_fear = get_emotion_score(data, "fear");
        float score_joy = get_emotion_score(data, "joy");

        result_score = score_sadness * -1 + score_analytical * 1 + score_anger * -2 + score_confident * 2 + score_tentative * 1 + score_fear * -1 + score_joy * 2;

    }

    public float get_emotion_score(string data, string emotion)
    {
        float score = 0;
        int index = data.IndexOf(emotion);
        if (index != -1)
        {
            string tmp = data.Remove(index, data.Length-index);
            tmp = tmp.Split(',')[tmp.Split(',').Length - 2];
            score = float.Parse(tmp.Split(':')[tmp.Split(':').Length - 1]);
        }
        return score;
    }
}
