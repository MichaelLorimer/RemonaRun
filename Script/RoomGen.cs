using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour {

	public GameObject[] availableRooms; 	// An array of avilable rooms to generate
	public List<GameObject> currentRooms;   // lists of current rooms
	public float screenWidthInPoints; 		//Width of the Screen for construction and deconstruction

	// Use this for initialization
	void Start ()
	{
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void AddRoom(float farthestRoomEndX)
	{
		// 1. Create a random room index for generation
		int randomRoomIndex = Random.Range(0, availableRooms.Length);

		//2. Get the room game object and instantiates 1 at random
		GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);

		//3. Setsthe room width gathered from the child object floor
		float roomWidth = room.transform.FindChild("Floor").localScale.x;

		//4. finds the Centre of the room to generate the next room
		float roomCentre = farthestRoomEndX + roomWidth * 0.4f;

		//5. ???
		room.transform.position = new Vector3 (roomCentre,0,0);

		//6. Adds a room to the end
		currentRooms.Add(room);
	}

	//Check if a new room is needed
	void GenRoomIfNeed()
	{
		//1. A list of rooms that canbe removed
		List<GameObject> roomsToRemove = new List<GameObject>();

		//2. bool for adding a room 
		bool addRooms = true;

		//3. Gathers the player position X
		float playerX = transform.position.x;

		//4. When you set the room position, you set the position of its 
		//center so you add the half room width to the position where the 
		//level ends. This way gets the point at which you should add the 
		//room, so that it started straight after the last room.
		float removeRoomX = playerX - screenWidthInPoints;

		//5.This sets the position of the room
		float addRoomX = playerX + screenWidthInPoints;

		//6. Finally you add the room to the list of current rooms.
		float farthestRoomEndX = 0;

		foreach (var room in currentRooms)
		{
			//7. enumerate current rooms.
			float roomWidth = room.transform.FindChild("Floor").localScale.x;
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
			float roomEndX = roomStartX + roomWidth;

			//8. check if a room needs to be added
			if (roomStartX > addRoomX)
				addRooms = false;
			
			//9. check if a room needsto be removed.
			if (roomEndX < removeRoomX)
				roomsToRemove.Add (room);

			//10. findthe farthest pointo fthe level to create the room if needed
			farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
		}

		//11. Removes rooms that are markedfor removal
		foreach (var room in roomsToRemove) 
		{
			currentRooms.Remove (room);
			Destroy (room);
		}

		//12. adds rooms if add room is still true 
		if (addRooms)
			AddRoom (farthestRoomEndX);
	}

	void FixedUpdate()
	{
		GenRoomIfNeed ();
	}
}
