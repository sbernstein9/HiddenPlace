using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Beat;

public class ExampleSyncer : MonoBehaviour {

	
	[FMODUnity.EventRef] public string chimeLoop;
	private FMOD.Studio.EventInstance chimeLoopInstance;

	public KeyCode myThrowKey;
	public Vector3 HandPosition;
	public GameObject wallL;
	Collider wallColLeft;
	Collider wallColRight;
	Quaternion startRot;
	Quaternion endRot;

	// Use this for initialization
	void Start () {
		
		HandPosition = transform.position;
		wallColLeft = wallL.GetComponent<Collider>();
		
		chimeLoopInstance = FMODUnity.RuntimeManager.CreateInstance(chimeLoop);
		chimeLoopInstance.setParameterValue("Release", 0.0f);
		//FMODUnity.RuntimeManager.PlayOneShot(chimeLoop, transform.position);

		chimeLoopInstance.setVolume(100);
		chimeLoopInstance.start();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(myThrowKey)) {
			Clock.Instance.SyncFunction (_HeadTowardsWall, Clock.BeatValue.Quarter);
			
		}
		if (Input.GetKeyUp(myThrowKey)) {
			Clock.Instance.SyncFunction (_ComeBackToHand, Clock.BeatValue.Eighth);
		}
		
	}


	private void _HeadTowardsWall() {
		// StartCoroutine(Coroutines.DoOverEasedTime(LENGTH_OF_TIME_IN_SECONDS, EASING_FUNCTION, t =>
		//FMODUnity.RuntimeManager.PlayOneShot(chimeLoopInstance, transform.position);
		
		
		//playingChimeSound = true;
		
		var startPosition = transform.position;
		Vector3 endPosition = wallL.transform.position;
		startRot = transform.rotation;
		Quaternion endRot = Random.rotation;
		
		//GetTarget (true, 0, 0, out endPosition.x, out endPosition.y, out endPosition.z);
		Debug.Log("TO WALL");

		StartCoroutine(Coroutines.DoOverEasedTime(Clock.Instance.HalfLength(), Easing.Linear, t =>
		{
			// this is where we define what happens inside of a coroutine we're generating on the spot
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			//Debug.Log("t = " + t);
			//Debug.Log("start" + startPosition + "end" + endPosition);
			transform.rotation = Quaternion.Lerp(startRot, endRot, t);
			
			//chimeLoopInstance.setVolume(t);
		}));
	}

	private void _ComeBackToHand() {
		StopAllCoroutines ();

		var startPosition = transform.position;
		var endPosition = HandPosition;

		Debug.Log("TO HAND");

		Quaternion currentRot = transform.rotation;
		StartCoroutine(Coroutines.DoOverEasedTime(Clock.Instance.EighthLength(), Easing.Linear, t =>
		{
			// this is where we define what happens inside of a coroutine we're generating on the spot
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			transform.rotation = Quaternion.Lerp(currentRot, startRot, t);
			//Debug.Log("t = " + t);
			
			//chimeLoopInstance.setVolume(1 - t);
			
			if (t > 0.6f)
			{
				chimeLoopInstance.setParameterValue("Release", 1.0f);
			}

		}));
	}
}

