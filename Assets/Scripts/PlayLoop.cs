using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoop : MonoBehaviour {

	[FMODUnity.EventRef] public string chimeLoop;
	private FMOD.Studio.EventInstance chimeLoopInstance;
	
	// Use this for initialization
	void Start () {
		chimeLoopInstance = FMODUnity.RuntimeManager.CreateInstance(chimeLoop);
		chimeLoopInstance.setParameterValue("Release", 0.0f);
		//FMODUnity.RuntimeManager.PlayOneShot(chimeLoop, transform.position);

		chimeLoopInstance.start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
