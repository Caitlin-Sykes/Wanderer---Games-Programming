using UnityEngine;

public class LevelNav : MonoBehaviour
{
    
    private Camera cam;

    public LevelGen lg;

    public GridController gc;

    private GameObject startRoom {get; set;}

    void Awake() {
        cam = this.GetComponent<Camera>();
    }

    public void startPlayerRoom() {
        startRoom = gc.rooms[lg.randomRoom()];
        print(startRoom);
    }

    //A function to initialise the starting camera
    public void MoveCamera() {

        Transform pos = startRoom.transform.Find("SpawnInit");
        // pos.position -= new Vector3(0,0,1);
        cam.transform.localPosition = Vector3.Lerp(transform.position, pos.position, 1); 
    }

    //A function to move the camera to the next room
    private void MoveCamera(GameObject room) {

        Transform pos = room.transform.Find("SpawnInit");
        // pos.position -= new Vector3(0,0,1);
        cam.transform.localPosition = Vector3.Lerp(transform.position, pos.position, 1); 
    }
}
