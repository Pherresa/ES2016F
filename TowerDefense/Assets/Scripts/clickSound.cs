using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class clickSound : MonoBehaviour {

	// Use this for initialization
	private AudioClip hover;
	private AudioClip click;
	private AudioSource source;

	public void  loadNextCanvas(){
		SceneManager.LoadScene ("OtherScene");
	}

	public void OnHover(){

		source.PlayOneShot(hover);
	}

	public void OnClick(){

		source.PlayOneShot (click);
	}
		




}
