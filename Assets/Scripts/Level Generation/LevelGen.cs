using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public class Room : MonoBehaviour {
//     public string RoomName { get; set; } //name of the room
//     public string PossibleDirections { get; set; } //possible directions

//     public string Type { get; set; } //type of room (start, end etc)

//     public Vector3 Position { get; set; } //position of room (start, end etc)

//     ///Constructor.
//     public Room(string roomName, string possibleDirections, string type, Vector3 position) {
//         RoomName = roomName;
//         PossibleDirections = possibleDirections;
//         Type = type;
//         Position = position;
//     }

    
// } //maybe can be removed, will see
public class LevelGen : MonoBehaviour
{

    private GridController gc;

    public GameObject[] layouts;

    private int previousRoom;

    // Start is called before the first frame update
    void Start()
    {
        gc = this.GetComponent<GridController>();

        
    }

    // A function to generate the start location
   public void generateStartLocation() {
  
        // Stores the current poisition in list
        int index = Random.Range(0, gc.rooms.Count);

        print(index);
        
        // Gets random start from the list of potential start locations
        GameObject startLoc = gc.rooms[index];
        
        newRoom(startLoc, index);
   }

    //A function to create a new room
    // nl = the room to be replaced
    // index = position in the list of rooms
   private void newRoom(GameObject nl, int index) {
        
        GameObject room = Instantiate(generateRoomLayout(), nl.transform.position, Quaternion.identity, nl.transform.parent);

        Destroy(nl); //removes old room
        room.name = index.ToString(); 
        gc.rooms[index] = room;
        previousRoom = index;
    

   }



    //Generates room layout
   private GameObject generateRoomLayout() {
    return layouts[Random.Range(0, layouts.Length)] as GameObject;
   }

//    private void mainPath() {
//         string possibleDirections = previousRoom.PossibleDirections;
        
//         // random char
//         int i = Random.Range(0, possibleDirections.Length);
//         char dir = possibleDirections[i];
//         print("random direction: " + dir);
//         //going up - +10 to y
//         //going down -10 to y
//         // left -10 to x
//         // right +10 to x

//         Vector3 position;
//         GameObject room;
//         //new coords
//         if (dir == 'U') {
//             position = transform.TransformPoint(Vector3.up * 40);
//             // Gets random room layout to start in
//             room = rooms[Random.Range(0, rooms.Length)];
//             position = previousRoom.Position + position;


//             print(position);
//             previousRoom = new Room("second", room.tag, "second", position);
//             Instantiate(room, position, Quaternion.identity, RoomPar);
//         }

//         else if (dir == 'D') {
//             position = transform.TransformPoint(Vector3.down * 40);
//             // Gets random room layout to start in
//             room = rooms[Random.Range(0, rooms.Length)];


//             print(position);
//             position = previousRoom.Position + position;
//             previousRoom = new Room("second", room.tag, "second", position);
//             Instantiate(room, position, Quaternion.identity, RoomPar);
//         }

//         else if (dir == 'L') {

//             position = transform.TransformPoint(Vector3.left * 40);
//             // Gets random room layout to start in
//             room = rooms[Random.Range(0, rooms.Length)];


//             print(position);
//             position = previousRoom.Position + position;
//             previousRoom = new Room("second", room.tag, "second", position);
//             Instantiate(room, position, Quaternion.identity, RoomPar);
//         }

//         else if (dir == 'R')
//         {
//             position = transform.TransformPoint(Vector3.right * 40);
//             // Gets random room layout to start in
//             room = rooms[Random.Range(0, rooms.Length)];

//             position = previousRoom.Position + position;


//             print(position);
//             previousRoom = new Room("second", room.tag, "second", position);
//             Instantiate(room, position, Quaternion.identity, RoomPar);
//         }
                
        

//         // // Gets random room layout to start in
//         // room = rooms[Random.Range(0, rooms.Length)];


//         // print(position);
//         // previousRoom = new Room("second", room.tag, "second", position);
//         // Instantiate(room, position, Quaternion.identity, RoomPar);


        
//    }
}
