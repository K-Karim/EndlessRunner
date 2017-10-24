/*
 * Graphics and Interaction (COMP30019) 
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour {

	private int currWave;
	//Max amount of waves to calculate at one time
	private int MAX_WAVES = 8;
	//rate at which waves decay
	private float WAVE_DECAY = .98f;
	//minimum amplitude before wave amplitude is set to 0
	private float WAVE_THRESHOLD = .01f;

	private float OFFSET_MULTIPLIER = 2.5f;

	public float distanceX, distanceZ;
	//amplitude of each wave
	private float[] waveAmplitude;
	//increase for larger waves when colliding with objects
	public float magnitudeDivider;
	//position of impact (or origin of wave)
	private Vector2[] impactPos;
	//distance of wave from impact
	private float[] dist;
	//spread of each wave
	public float speedWaveSpread;

	Mesh mesh;

	/*Initialization */
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;

		waveAmplitude = new float[MAX_WAVES];
		impactPos = new Vector2[MAX_WAVES];
		dist = new float[MAX_WAVES];
	}

	/* Update amplitude and distance of waves */
	void Update () {
	
		//Cycles through each wave
		for (int i=0; i<MAX_WAVES; i++){

			waveAmplitude[i] = GetComponent<Renderer>().material.GetFloat("_WaveAmplitude" + (i+1));


			//if wave below threshold, set to 0
			if (waveAmplitude[i] < WAVE_THRESHOLD){
				GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + (i+1), 0);
				dist[i] = 0;
			}
			//If wave still has amplitude, move further from origin and reduce amplitude
			else{
				dist[i] += speedWaveSpread;
				GetComponent<Renderer>().material.SetFloat("_Distance" + (i+1), dist[i]);
				GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + (i+1), waveAmplitude[i] * WAVE_DECAY);
			}


		}
	}

	/* On Trigger, make wave at point of impact*/
	void OnTriggerEnter(Collider col){
		if (col.GetComponent<Rigidbody>()){
			//Cycles through each wave per collision
			currWave++;
			if (currWave == MAX_WAVES + 1){
				currWave = 1;
			}

			//resets old wave to 0 if not already
			waveAmplitude[currWave-1] = 0;
			dist[currWave-1] = 0;

			//distance between collision and plane
			distanceX = this.transform.position.x - col.gameObject.transform.position.x;
			distanceZ = this.transform.position.z - col.gameObject.transform.position.z;

			//position of impact
			impactPos[currWave - 1].x = col.transform.position.x;
			impactPos[currWave - 1].y = col.transform.position.z;

			//set values for wave (to be used by the shader)
			GetComponent<Renderer>().material.SetFloat("_xImpact" + currWave, col.transform.position.x);
			GetComponent<Renderer>().material.SetFloat("_zImpact" + currWave, col.transform.position.z);

			GetComponent<Renderer>().material.SetFloat("_OffsetX" + currWave, distanceX / mesh.bounds.size.x * OFFSET_MULTIPLIER);
			GetComponent<Renderer>().material.SetFloat("_OffsetZ" + currWave, distanceZ / mesh.bounds.size.z * OFFSET_MULTIPLIER);

			GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + currWave, col.GetComponent<Rigidbody>().velocity.magnitude * magnitudeDivider);

		}
	}
}
