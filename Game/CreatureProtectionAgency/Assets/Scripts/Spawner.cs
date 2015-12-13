using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
	[System.Serializable]
	public class EnemySpawner
	{
		public EnemyController enemyToSpawn;
		public float timeToSpawn;
		public Transform positionToSpawn;
	}
	private float timer;
	
	public List<EnemySpawner> enemiesToSpawn = new List<EnemySpawner> ();
	
	// Update is called once per frame
	void Update ()
	{
		if (enemiesToSpawn.Count > 0) {
			for (int i = 0; i < enemiesToSpawn.Count; ++i) {
				if (timer > enemiesToSpawn [i].timeToSpawn) {
					EnemyController enemy = Instantiate (enemiesToSpawn [i].enemyToSpawn);
					enemy.transform.position = enemiesToSpawn [i].positionToSpawn.position;
					enemiesToSpawn.Remove (enemiesToSpawn [i]);
                    PlayerController.Instance.enemies.Add(enemy);
				}
			}
		}
		timer += Time.deltaTime;
	}
}
