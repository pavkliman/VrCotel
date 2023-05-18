using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
	private AudioSource _audioSource;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		TextToSpeech.Instance.OnSuccessfullyConvertTextToAudioAction += PlaySoundToConvert;
	}

	public void PlaySoundToConvert(AudioClip clip)
	{
		if(!_audioSource.isPlaying) 
			_audioSource.PlayOneShot(clip);
		else
        {
			_audioSource.Stop();
			_audioSource.PlayOneShot(clip);
		}

	}
}
