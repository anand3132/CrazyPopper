using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public static AudioController instance;
	public AudioClip au_Level_Failed;
	public AudioClip au_Level_Complete;
	public AudioClip au_Popup;
	public AudioSource MyAudioSource;
	public AudioSource BackGroundSource;
	private AudioController(){}

	public static AudioController GetInstance(){
		return instance;
	}

	void Start(){
		
		if(instance) {
			Destroy(gameObject);
			Debug.LogError("Already initialised");
		}else{
			AudioController.instance=this;
		}
		MyAudioSource=gameObject.GetComponent<AudioSource>();
	}

	public void PlayAudio(AudioClip _clip){
		if(_clip)
		{
			MyAudioSource.clip=_clip;
			MyAudioSource.Play();
		}
	}

	public void PlayBGM(){
		if(!BackGroundSource.isPlaying)
			BackGroundSource.Play();
	}

	public void PauseBGM(){
		if(BackGroundSource.isPlaying)
			BackGroundSource.Pause();
	}

	public void ChangeBGM(AudioClip _clip){
		if(BackGroundSource.isPlaying){
			BackGroundSource.Stop();
		}
		BackGroundSource.clip=_clip;
		PlayBGM();
	}

}
