using UnityEngine;

public class LevelNav : MonoBehaviour
{
    
    private Camera cam;

    public LevelGen lg;

    public GridController gc;

    private GameObject currentRoom {get; set;}

    public GameObject player;

    void Awake() {
        cam = this.GetComponent<Camera>();
    }

    //A function to generate the starting room
    public void startPlayerRoom() {
        currentRoom = gc.rooms[lg.randomRoom()];
    }

    //A function to initialise the starting camera
    public void MoveCamera() {

        Transform pos = currentRoom.transform.Find("SpawnInit");
        cam.transform.localPosition = Vector3.Lerp(transform.position, pos.position, 1); 

        Instantiate(player, pos.position, Quaternion.identity); 

    }

    //A function to move the camera to the next room
    //@param roomInt - index of the new room
    //@param dir - direction of the door
    public void MoveCamera(int roomInt, string dir) {
        
        //Sets current room to new room
        currentRoom = gc.rooms[roomInt];

        Transform pos;
        
        //If S1, need the down spawn position
        if (dir == "S1") {
            pos = currentRoom.transform.Find("SpawnD");
        }
        //If S2, need the left spawn position
        else if (dir == "S2") {
            pos = currentRoom.transform.Find("SpawnL");
        }

        //If S3, need the up spawn position
        else if (dir == "S3") {
            pos = currentRoom.transform.Find("SpawnU");
        }

        else {
            pos = currentRoom.transform.Find("SpawnR");

        }
       
        //Destroys previous player object
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        //Sets camera to the centre of the room
        cam.transform.localPosition = Vector3.Lerp(transform.position, currentRoom.transform.Find("SpawnInit").position, 1); 

        //Instantiate new player
        Instantiate(player, pos.position, Quaternion.identity); 
    }
}
