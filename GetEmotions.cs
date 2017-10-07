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

        StartCoroutine(AnalyzeTone("this is very sad"));

    }

    // Update is called once per frame
    void Update () {
		
	}

    public IEnumerator AnalyzeTone(string _stringToTest)
    {
        analyzingFinish = false;
        print(_stringToTest);

        if (!_toneAnalyzer.GetToneAnalyze(OnGetToneAnalyze, _stringToTest))
            print("Watson Tone Analyzer: Failed to analyze");

        while (analyzingFinish == false)
        {
            yield return null;
        }
        yield return result_score;

    }

    public void OnGetToneAnalyze(ToneAnalyzerResponse resp, string data)
    {
        print(data);
        float score_sadness = get_emotion_score(data, "sadness");
        float score_analytical = get_emotion_score(data, "analytical");
        float score_anger = get_emotion_score(data, "anger");
        float score_confident = get_emotion_score(data, "confident");
        float score_tentative = get_emotion_score(data, "tentative");
        float score_fear = get_emotion_score(data, "fear");
        float score_joy = get_emotion_score(data, "joy");
        float score_polite = get_emotion_score(data, "polite");
        float score_impolite = get_emotion_score(data, "impolite");
        float score_frustrated = get_emotion_score(data, "frustrated");

        result_score = score_sadness * -1 + score_analytical * 1 + score_anger * -2 + score_confident * 0.5f + score_tentative * 1 + score_fear * -1 + score_joy * 2 + score_polite*1 + score_impolite*-1 + score_frustrated*-1;
        analyzingFinish = true;
        print(result_score);
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
