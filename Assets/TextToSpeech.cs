using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking; // Для подкачки подключаем
using UnityEngine.UI;

public enum Language
{
	ru,
	en
}
public class TextToSpeech : MonoBehaviour
{
	public static TextToSpeech Instance;
	public Action<AudioClip> OnSuccessfullyConvertTextToAudioAction;
	[SerializeField] private Language _language;
    private const string _urlGoogleTranslate = "https://translate.google.com/translate_tts?ie=UTF-8&tl=";
    private const string _urlGoogleTranslateClient = "&client=tw-ob&q=";
 
	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		DontDestroyOnLoad(gameObject);
	}

    private void Start()
    {
        
    }

    public void ConvertTextToAudio(TextMeshProUGUI textOnPanel)
	{
		StartCoroutine(Converting(textOnPanel.text));

	}

    private IEnumerator Converting(string textOnPanel)
    {
        string text = textOnPanel;
        int maxLength = 200;
        int startIndex = 0;

        // Create a list to hold the audio clips
        List<AudioClip> audioClips = new List<AudioClip>();

        while (startIndex < text.Length)
        {
            int length = Mathf.Min(maxLength, text.Length - startIndex);
            string url = _urlGoogleTranslate + _language.ToString() + _urlGoogleTranslateClient + text.Substring(startIndex, length);
            Debug.Log(url);
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.Success)
                {
                    AudioClip audio = DownloadHandlerAudioClip.GetContent(www);
                    audioClips.Add(audio); // Add the new audio clip to the list
                }
                else
                {
                    Debug.LogError("Error: " + www.error);
                }
            }
            startIndex += maxLength;
        }

        // Concatenate the audio clips into a new AudioClip
        int totalSampleCount = audioClips.Sum(c => c.samples);
        AudioClip concatenatedClip = AudioClip.Create("ConcatenatedClip", totalSampleCount, 1, 22050, false);
        float[] samples = new float[totalSampleCount];
        int i = 0;
        foreach (AudioClip clip in audioClips)
        {
            float[] clipSamples = new float[clip.samples];
            clip.GetData(clipSamples, 0);
            clipSamples.CopyTo(samples, i);
            i += clip.samples;
        }
        concatenatedClip.SetData(samples, 0);

        // Invoke the OnSuccessfullyConvertTextToAudioAction with the concatenated audio clip
        OnSuccessfullyConvertTextToAudioAction?.Invoke(concatenatedClip);
    }
}
