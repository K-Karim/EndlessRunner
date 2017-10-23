/*
 * Graphics and Interaction (COMP30019) Project 2
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour {
	
	private int waveNumber;
	//Max amount of waves to calculate at one time
	private int MAX_WAVES = 8;
	//rate at which waves decay
	private float WAVE_DECAY = .98f;
	//minimum amplitude before wave amplitude is set to 0
	private float WAVE_THRESHOLD = .01f;

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

	//Initialisation
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;

		waveAmplitude = new float[MAX_WAVES];
		impactPos = new Vector2[MAX_WAVES];
		dist = new float[MAX_WAVES];
	}

	//Called every frame
	void Update () {
	
		//Cycles through each wave
		for (int i=0; i<MAX_WAVES; i++){

			waveAmplitude[i] = GetComponent<Renderer>().material.GetFloat("_WaveAmplitude" + (i+1));
			//If wave still has amplitude, move further from origin and reduce amplitude
			if (waveAmplitude[i] > 0){
				dist[i] += speedWaveSpread;
				GetComponent<Renderer>().material.SetFloat("_Distance" + (i+1), dist[i]);
				GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + (i+1), waveAmplitude[i] * WAVE_DECAY);
			}

			//if wave below threshold, set to 0
			if (waveAmplitude[i] < WAVE_THRESHOLD){
				GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + (i+1), 0);
				dist[i] = 0;
			}

		}
	}

	//Trigger so that things move through the wave
	void OnTriggerEnter(Collider col){
		if (col.GetComponent<Rigidbody>()){
			//Cycles through each wave per collision
			waveNumber++;
			if (waveNumber == MAX_WAVES + 1){
				waveNumber = 1;
			}

			//resets old wave to 0 if not already
			waveAmplitude[waveNumber-1] = 0;
			dist[waveNumber-1] = 0;

			//distance between collision and plane
			distanceX = this.transform.position.x - col.gameObject.transform.position.x;
			distanceZ = this.transform.position.z - col.gameObject.transform.position.z;

			//position of impact
			impactPos[waveNumber - 1].x = col.transform.position.x;
			impactPos[waveNumber - 1].y = col.transform.position.z;

			//set values for wave (to be used by the shader)
			GetComponent<Renderer>().material.SetFloat("_xImpact" + waveNumber, col.transform.position.x);
			GetComponent<Renderer>().material.SetFloat("_zImpact" + waveNumber, col.transform.position.z);

			GetComponent<Renderer>().material.SetFloat("_OffsetX" + waveNumber, distanceX / mesh.bounds.size.x * 2.5f);
			GetComponent<Renderer>().material.SetFloat("_OffsetZ" + waveNumber, distanceZ / mesh.bounds.size.z * 2.5f);

			GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + waveNumber, col.GetComponent<Rigidbody>().velocity.magnitude * magnitudeDivider);

		}
	}
}
