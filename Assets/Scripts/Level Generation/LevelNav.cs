using UnityEngine;

public class LevelNav : MonoBehaviour
{
    
    private Camera cam;

    public LevelGen lg;

    public GridController gc;

    private int start;

    public GameObject currentRoom {get; set;}

    public GameObject player;

    public GameObject escape;

    void Awake() {
        cam = this.GetComponent<Camera>();
    }

    //A function to generate the starting room
    public void startPlayerRoom() {
        start = lg.randomRoom();
        currentRoom = gc.rooms[start];
    }

    //A function to initialise the starting camera
    public void MoveCamera() {

        //Gets the initial spawn location
        Transform pos = currentRoom.transform.Find("SpawnInit");

        //Shifts the camera
        cam.transform.localPosition = Vector3.Lerp(transform.position, pos.position, 1); 

        //Gets the enemy spawn component
        EnemySpawn es = currentRoom.GetComponent<EnemySpawn>();

        //Sets the starting room to be cleared so no enemies spawn
        es.clear = EnemySpawn.Clear.Cleared;
        
        Doors door = currentRoom.GetComponentInChildren<Doors>();
    
        Instantiate(player, pos.position, Quaternion.identity); 


    }

    public void escapeRoom() {
        int randomRoom = lg.randomRoom();

        if (start != randomRoom) {

            //Sets current room to new room
            GameObject room = gc.rooms[randomRoom];
            
            //Gets the initial spawn location
            Transform pos = room.transform.Find("SpawnInit");
            
            Instantiate(escape, pos.position, Quaternion.identity);
        }

        else {
            escapeRoom();
        }
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

        //Gets the current health
        int health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().health;

        //Destroys previous player object
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        
        //Sets camera to the centre of the room
        cam.transform.localPosition = Vector3.Lerp(transform.position, currentRoom.transform.Find("SpawnInit").position, 1); 

        //Instantiate new player
         Instantiate(player, pos.position, Quaternion.identity); 

        //Sets the new health
        GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().setHealth(health);
    }
}
