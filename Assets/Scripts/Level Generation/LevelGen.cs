using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour {
    public string RoomName { get; set; } //name of the room
    public string PossibleDirections { get; set; } //possible directions

    public string Type { get; set; } //type of room (start, end etc)

    public Vector3 Position { get; set; } //position of room (start, end etc)

    ///Constructor.
    public Room(string roomName, string possibleDirections, string type, Vector3 position) {
        RoomName = roomName;
        PossibleDirections = possibleDirections;
        Type = type;
        Position = position;
    }

    
}
public class LevelGen : MonoBehaviour
{

    public List<GameObject> potentialStarts;
    private GameObject startLoc;

    private Room startingRoom;
    public GameObject[] rooms;

    public Transform RoomPar;

    private Room previousRoom;

    // Start is called before the first frame update
    void Start()
    {
        generateStartLocation();

        // mainPath();


        
    }

    // A function to generate the start location
    // Doesn't quite work as intended, but can be worked with.
   private void generateStartLocation() {
  
        // Gets random start from the list of potential start locations
        startLoc = potentialStarts[Random.Range(0, potentialStarts.Count)];
        // BoxCollider2D boxy = startLoc.GetComponent<BoxCollider2D>(); 

        // Gets random room layout to start in
        GameObject startRoom = rooms[Random.Range(0, rooms.Length)];
        startingRoom = new Room("start", startRoom.tag, "start", startLoc.transform.position);
        print(startLoc.transform.position);
  
        GameObject room = Instantiate(startRoom, Vector3.zero, Quaternion.identity, startLoc.transform);
   


        // RoomPar.transform.position = boxy.transform.position;

        previousRoom = startingRoom;


   }

   private void mainPath() {
        string possibleDirections = previousRoom.PossibleDirections;
        
        // random char
        int i = Random.Range(0, possibleDirections.Length);
        char dir = possibleDirections[i];
        print("random direction: " + dir);
        //going up - +10 to y
        //going down -10 to y
        // left -10 to x
        // right +10 to x

        Vector3 position;
        GameObject room;
        //new coords
        if (dir == 'U') {
            position = transform.TransformPoint(Vector3.up * 40);
            // Gets random room layout to start in
            room = rooms[Random.Range(0, rooms.Length)];
            position = previousRoom.Position + position;


            print(position);
            previousRoom = new Room("second", room.tag, "second", position);
            Instantiate(room, position, Quaternion.identity, RoomPar);
        }

        else if (dir == 'D') {
            position = transform.TransformPoint(Vector3.down * 40);
            // Gets random room layout to start in
            room = rooms[Random.Range(0, rooms.Length)];


            print(position);
            position = previousRoom.Position + position;
            previousRoom = new Room("second", room.tag, "second", position);
            Instantiate(room, position, Quaternion.identity, RoomPar);
        }

        else if (dir == 'L') {

            position = transform.TransformPoint(Vector3.left * 40);
            // Gets random room layout to start in
            room = rooms[Random.Range(0, rooms.Length)];


            print(position);
            position = previousRoom.Position + position;
            previousRoom = new Room("second", room.tag, "second", position);
            Instantiate(room, position, Quaternion.identity, RoomPar);
        }

        else if (dir == 'R')
        {
            position = transform.TransformPoint(Vector3.right * 40);
            // Gets random room layout to start in
            room = rooms[Random.Range(0, rooms.Length)];

            position = previousRoom.Position + position;


            print(position);
            previousRoom = new Room("second", room.tag, "second", position);
            Instantiate(room, position, Quaternion.identity, RoomPar);
        }
                
        

        // // Gets random room layout to start in
        // room = rooms[Random.Range(0, rooms.Length)];


        // print(position);
        // previousRoom = new Room("second", room.tag, "second", position);
        // Instantiate(room, position, Quaternion.identity, RoomPar);


        
   }
}
