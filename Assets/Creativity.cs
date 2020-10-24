//Nathan Boldy
//100653593
//10/23/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic script for playing sounds
public class Creativity : MonoBehaviour
{
	public AudioSource audioEmitter;
	
	public void playSound()
	{
		audioEmitter.Play();
	}
}

//Empty void of nothingness that would have been filled with memetastic madness if the actual AI functionality didn't eat up 3 days of my time