using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class JobSystemMethod : MonoBehaviour {

	// Use this for initialization
	void Start () {
		cubes = GameObject.FindGameObjectsWithTag("Cube");
		velocities = new NativeArray<float>(cubes.Length, Allocator.Persistent);
		for(int i = 0; i < velocities.Length; i++){
			velocities[i] = 0;
		}
	}
	GameObject[] cubes;
	NativeArray<float> velocities;
	[SerializeField, Range(1, 30)]
	int minCommandsPerJob;
	
	// Update is called once per frame
	void Update () {
		var commands = new NativeArray<RaycastCommand>(cubes.Length, Allocator.TempJob);
		var results = new NativeArray<RaycastHit>(cubes.Length, Allocator.Temp);

		for(int i = 0; i < cubes.Length; i++){
			commands[i] = new RaycastCommand(cubes[i].transform.position, Vector3.down);
		}

		RaycastCommand.ScheduleBatch(commands, results, minCommandsPerJob).Complete();
		commands.Dispose();
		
		for(int i = 0; i < cubes.Length; i++){
			if(results[i].distance <= 0.5f){
				velocities[i] = 3.0f * Random.Range(1.1f, 1.5f);
			}
			velocities[i] += -9.8f * Time.deltaTime;

			cubes[i].transform.position += Vector3.up * velocities[i] * Time.deltaTime;
		}
		results.Dispose();
	}

	void OnDestroy(){
		velocities.Dispose();
	}
}
