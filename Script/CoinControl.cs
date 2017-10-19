using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
	// --- !! To Do !! ---
	//--------------------
	//- Make an array to hold the pickup prefab
	//- Coin Spawn fcuntion
	//- coin Destroy function + score incriment // Kinda done

	public static int maxCoin = 5; 		//Arbitrary Maximum coin spawn amount
	public GameObject Coins;

	public int coinValue = 100;     //Coin value for points
	private Rigidbody2D cb;
	// -- Spawn Variables --
	//

	public float minYSpawn = 0.0f;
	public float maxYSpawn = 2.0f;
	public float screenHeightInPoints; 		//Width of the Screen for construction and deconstruction



	float edge = 10f;
	float RemoveEdge;
	float screenWidthInPoints; 

	public int currentAmount = 0;      // how many coins currently exist

	// Coins lists
	// list [] currentCoins

	// Use this for initialization
	void Start () 
	{
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if coinspawn < MaxAmount - spawnCoin
		if (currentAmount < maxCoin)
		{
			//CoinSpawn ();
		}
	}

	public void CoinSpawn()
	{
		//--- To Do ---
		// 1.line spawn
		// 2. square spawn
		// 3. wiggle spawn
		float ranSpawn = Random.Range (minYSpawn, maxYSpawn);
		if (Coins != null) 
		{
			for (int i = 0; i < 1; i++) {
				//Line

				Vector2 SpawnPos = Vector2.zero;

				SpawnPos.x = edge;
				SpawnPos.y = ranSpawn;
				Instantiate (Coins, SpawnPos, Quaternion.identity);

				edge += 2;
				currentAmount++;
			}
		}
		//Code for spawning coins
		// - In Patterns
		// - In lines
		// - Randomly? 
	}


	public void CoinRemove(Rigidbody2D Player)
	{
		/*if (Coins.transform.position.x > Player.transform.position.x * 2) 
		{// for each
			DestroyImmediate (Coins, true);
		}
		// remove any coins left behind at a given distance
		*/
		currentAmount--;
	}


}
