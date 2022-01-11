using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector3 = System.Numerics.Vector3;

public class ObstaclePlacer : MonoBehaviour{

    public int ObsToPlace = 10;
    GameObject _obstacle;
    public GameObject[] obstacles = new GameObject[0];

    public float obstacleCheckRadius = 3f;
    public int maxSpawnAttemptsPerObstacle = 10;

    void Awake(){
        for (int i = 0; i < ObsToPlace; i++){
            _obstacle = obstacles[UnityEngine.Random.Range(0, obstacles.Length)];

            UnityEngine.Vector3 position = UnityEngine.Vector3.zero;

            bool validPosition = false;
            int spawnAttempts = 0;

            while (!validPosition && spawnAttempts < maxSpawnAttemptsPerObstacle){
                spawnAttempts++;

                position = new UnityEngine.Vector3(UnityEngine.Random.Range(-50.0f, 50.0f), 0,
                    UnityEngine.Random.Range(-50.0f, 50.0f));
                validPosition = true;

                Collider[] colliders = Physics.OverlapSphere(position, obstacleCheckRadius);

                foreach (Collider col in colliders){
                    if (col.tag == "Wall"){
                        validPosition = false;
                    }
                }
            }

            if (validPosition){
                Instantiate(_obstacle, position + _obstacle.transform.position, Quaternion.identity);
            }
        }
    }
}
