using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraditionalMethod : MonoBehaviour {

	void Start(){
		cubes = GameObject.FindGameObjectsWithTag("Cube");
		velocities = new float[cubes.Length];
		accel = -9.8f;
		for(int i = 0; i < cubes.Length; i++){
			velocities[i] = 0;
		}
	}
	GameObject[] cubes;
	float accel;
	float[] velocities;
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < cubes.Length; i++){
			cubes[i].transform.position += Vector3.up * velocities[i] * Time.deltaTime;
			velocities[i] += accel * Time.deltaTime;
			if(Physics.Raycast(cubes[i].transform.position, Vector3.down, 1f)){
				velocities[i] = 3f * Random.Range(1.5f, 2.0f);
			}
		}
	}
}
