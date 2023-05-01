using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGen : MonoBehaviour
{
    public GridController gc;

    public GameObject[] layouts; //all layouts excluding ends

    public GameObject[] endLayouts; //all rooms that only have one exit/entrance

    private int previousRoom { get; set; } = -1; // the previous room generated

    private bool firstRun { get; set; } = true; //if first run of generation

    private int mainPathLength { get; set; } // the length of the main paths

    private string neededDir = ""; //needed directions

    [SerializeField]
    public List<int> rooms; //the index of the main path

    void Start()
    {
        mainPathLength = Random.Range(6, 9);
    }

    // A function to generate the start location
    public void generateStartLocation()
    {
        rooms.Clear();
        // Stores the current position in list
        int index = Random.Range(0, gc.rooms.Count);

        // Gets random start from the list of potential start locations
        GameObject startLoc = gc.rooms[index];

        //Calls newRoom
        newRoom(startLoc, index);
    }

    //A function to create a new room
    //@param nl = the room to be replaced
    //@param index = position in the list of rooms
    private void newRoom(GameObject nl, int index)
    {
        // int timer = 0;
        GameObject layout = generateRoomLayout();
        while (
            gridPlacementValidation(layout, index) == false
            || checkSurroundingRoom(layout, index) == false
            || checkPreviousRoom(layout, index) == false
        )
        {
            // timer++;

            //  if (timer == 120) {
            //     print("infloop");
            //     gc.resetFunc();
            //     break;
            // }

            //If it is on its first run (and therefore doesn't have a previous room)
            if (firstRun == true && gridPlacementValidation(layout, index) == true)
            {
                firstRun = false;
                break;
            }
            //if first run is false and meets conditions, breaks from loop
            else if (
                firstRun == false
                && gridPlacementValidation(layout, index) == true
                && checkSurroundingRoom(layout, index) == true
                && checkPreviousRoom(layout, index) == true
            )
            {
                break;
            }
            //else, sets a new layout, and continues
            else
            {
                layout = generateRoomLayout();
                continue;
            }
        }

        //Instantiates new room as room
        GameObject room = Instantiate(
            layout,
            nl.transform.position,
            Quaternion.identity,
            nl.transform.parent
        );

        rooms.Add(index); //adds to the list of the main rooms
        Destroy(nl); //removes old room
        room.name = index.ToString(); //sets the name of the room to the index
        gc.rooms[index] = room; //replaces room in list with new room
        previousRoom = index; //previous room is set to index
    }

    // A function to create a new end room; used to generate an end room
    //@param: index - position in the list of rooms
    private void newRoom(int index)
    {
        GameObject layout = generateEndRoomLayout();

        // int timer = 0;
        while (
            gridPlacementValidation(layout, index) == false
            || checkSurroundingRoom(layout, index) == false
            || checkPreviousRoom(layout, index) == false
        )
        {
            // timer++;

            //  if (timer == 120) {
            //     gc.resetFunc();
            //     print("infloop");

            // }

            //if first run is false and meets conditions, breaks from loop
            if (
                gridPlacementValidation(layout, index) == true
                && checkSurroundingRoom(layout, index) == true
                && checkPreviousRoom(layout, index) == true
            )
            {
                break;
            }
            //else, sets a new layout, and continues
            else
            {
                layout = generateEndRoomLayout();
                continue;
            }
        }

        GameObject room = Instantiate(
            layout,
            gc.rooms[index].transform.position,
            Quaternion.identity,
            gc.rooms[index].transform.parent
        );

        rooms.Add(index); //adds to the list of the main rooms
        Destroy(gc.rooms[index]); //removes old room
        room.name = index.ToString(); //sets name of room to index
        gc.rooms[index] = room; //replaces room in list with new room
        previousRoom = index; //previousRoom is set to index
    }

    // A function to create a new room; used by fillingSurroundingRoom function
    //@param: index - position in the list of rooms
    //@param: layout - the layout of the room as a gameobject
    private void newRoom(int index, GameObject layout)
    {
        GameObject room = Instantiate(
            layout,
            gc.rooms[index].transform.position,
            Quaternion.identity,
            gc.rooms[index].transform.parent
        );

        rooms.Add(index); //adds to the list of the main rooms
        Destroy(gc.rooms[index]); //removes old room
        room.name = index.ToString(); //sets name of room to index
        gc.rooms[index] = room; //replaces room in list with new room
        previousRoom = index; //previousRoom is set to index
    }

    //Generates the normal room layout
    private GameObject generateRoomLayout()
    {
        return layouts[Random.Range(0, layouts.Length)] as GameObject;
    }

    //Randomly generates an end room based on the validation
    private GameObject generateEndRoomLayout()
    {
        return endLayouts[Random.Range(0, endLayouts.Length)] as GameObject;
    }

    //Generates the main path
    public void mainPath()
    {
        for (int i = 0; i <= mainPathLength; i++)
        {
            GameObject prevRoom = gc.rooms[previousRoom];

            List<char> banned = new List<char>(); //list of banned directions

            // random direction
            char dir = prevRoom.tag[Random.Range(0, prevRoom.tag.Length)];

            int index; //temp index

            while (dir != -1)
            {
                try
                {
                    //if direction is up and banned does not contain U
                    if (dir == 'U' && !banned.Contains('U'))
                    {
                        //set index to index minus ten
                        index = previousRoom - 10;

                        //if empty then call newRoomswitch
                        if (checkEmpty(index) == true)
                        {
                            newRoomSwitch(i, index);
                            break;
                        }
                        //if not throw exception
                        else
                        {
                            throw new System.Exception();
                        }
                    }
                    //if direction is down and banned does not contain D
                    else if (dir == 'D' && !banned.Contains('D'))
                    {
                        //set index to index plus ten
                        index = previousRoom + 10;
                        //if empty then call newRoomswitch
                        if (checkEmpty(index) == true)
                        {
                            newRoomSwitch(i, index);
                            break;
                        }
                        //if not throw exception
                        else
                        {
                            throw new System.Exception();
                        }
                    }
                    //if direction is L and banned does not contain L
                    else if (dir == 'L' && !banned.Contains('L'))
                    {
                        //subtract one from index
                        index = previousRoom - 1;
                        //if empty then call newRoomswitch
                        if (checkEmpty(index) == true)
                        {
                            newRoomSwitch(i, index);
                            break;
                        }
                        //if not throw exception
                        else
                        {
                            throw new System.Exception();
                        }
                    }
                    //if direction is R and banned does not contain R
                    else if (dir == 'R' && !banned.Contains('R'))
                    {
                        //add one to index
                        index = previousRoom + 1;
                        //if empty then call newRoomswitch
                        if (checkEmpty(index) == true)
                        {
                            newRoomSwitch(i, index);
                            break;
                        }
                        //if not throw exception
                        else
                        {
                            throw new System.Exception();
                        }
                    }
                    else
                    {
                        banned.Add(dir);
                        dir = prevRoom.tag[Random.Range(0, prevRoom.tag.Length)];
                    }
                }
                catch
                {
                    print("Out of the Grid. Trying another direction...");
                    banned.Add(dir);
                    if (banned.Count == 4)
                    {
                        break;
                    }
                    else
                    {
                        dir = prevRoom.tag[Random.Range(0, prevRoom.tag.Length)];
                    }
                }
            }
        }
    }

    //end of path generation
    //@param: counter - current iteration of for loop
    //@param: index - current index of room

    private void newRoomSwitch(int counter, int index)
    {
        //if final iteration of loop, generate end room
        if (counter == mainPathLength)
        {
            newRoom(index);
        }
        else
        {
            newRoom(gc.rooms[index], index);
        }
    }

    //validate placement
    //@param: layout - the room layout
    //@param: ind - index of the current room
    private bool gridPlacementValidation(GameObject layout, int ind)
    {
        //if index is certain numbers, cannot be certain layouts.
        if (ind == 0)
        {
            // cannot be up or left
            if (layout.tag.Contains("U") || layout.tag.Contains("L"))
            {
                return false;
            }
        }
        else if (ind == 9)
        {
            // cannot be up or right
            if (layout.tag.Contains("U") || layout.tag.Contains("R") && layout.tag != "ER")
            {
                return false;
            }
        }
        else if (ind == 90)
        {
            // cannot be down or left
            if (layout.tag.Contains("D") || layout.tag.Contains("L"))
            {
                return false;
            }
        }
        else if (ind == 99)
        {
            // cannot be down or right
            if (layout.tag.Contains("D") || (layout.tag.Contains("R")))
            {
                return false;
            }
        }
        // 0, 9, 90, & 99 are the corner boxes in the 10x10 grid.
        //
        else
        {
            //This equates to the top row
            if (ind >= 1 && ind <= 8)
            {
                if (layout.tag.Contains("U"))
                {
                    return false;
                }
            }
            //This equates to the bottom row
            else if (ind >= 91 && ind <= 98)
            {
                if (layout.tag.Contains("D"))
                {
                    return false;
                }
            }

            //if it has a 0, it is the most left it can be
            else if (ind.ToString().Contains("0"))
            {
                if (layout.tag.Contains("L"))
                {
                    return false;
                }
            }
            //if it is nine, it is the most right it can be
            else if (ind.ToString().Contains("9"))
            {
                if (layout.tag.Contains("R"))
                {
                    return false;
                }
            }
        }

        return true;
    }

    //checks previous room to make sure paths connect
    //@param: layout - the room layout
    //@param: ind - index of the current room
    private bool checkPreviousRoom(GameObject layout, int ind)
    {
        // if the previous room doesn't have the corresponding direction
        //so if previous room doesn't have left in its tag, and the new room has right, recalculate
        //will only be -1 on first run
        if (previousRoom == -1)
        {
            return false;
        }
        else if (
            layout.tag.Contains("L")
            && gc.rooms[previousRoom].tag.Contains("R")
            && ind == previousRoom + 1
        )
        {
            return true;
        }
        else if (
            layout.tag.Contains("R") && layout.tag != "ER"
            && gc.rooms[previousRoom].tag.Contains("L")
            && ind == previousRoom - 1
        )
        {
            return true;
        }
        else if (
            gc.rooms[previousRoom].tag.Contains("U")
            && layout.tag.Contains("D")
            && ind == previousRoom - 10
        )
        {
            return true;
        }
        else if (
            gc.rooms[previousRoom].tag.Contains("D")
            && layout.tag.Contains("U")
            && ind == previousRoom + 10
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Checks the surrounding rooms to make sure paths connect
    //@param: layout - the room layout
    //@param: ind - index of the current room
    private bool checkSurroundingRoom(GameObject layout, int ind)
    {
        //checks room above
        if (
            ind > 9
            && layout.tag.Contains("U")
            && !(gc.rooms[ind - 10].tag.Contains("D") || gc.rooms[ind - 10].tag.Contains("ER"))
        )
        {
            return false;
        }
        //checks room to the left
        else if (
            !ind.ToString().Contains("0")
            && layout.tag.Contains("L")
            && !(gc.rooms[ind - 1].tag.Contains("R") || gc.rooms[ind - 1].tag.Contains("E"))
        )
        {
            return false;
        }
        //checks room to the right
        else if (
            !ind.ToString().Contains("9")
            && layout.tag.Contains("R")
            && !(gc.rooms[ind + 1].tag.Contains("L") || gc.rooms[ind + 1].tag.Contains("E"))
        )
        {
            return false;
        }
        //checks room below
        else if (
            ind < 90
            && layout.tag.Contains("D")
            && !(gc.rooms[ind + 10].tag.Contains("U") || gc.rooms[ind + 10].tag.Contains("E"))
        )
        {
            return false;
        }

        return true;
    }


    // A function to check if a room is empty or not
    // @param: index as index of the room
    private bool checkEmpty(int index)
    {
        if (gc.rooms[index].tag.Contains("ER"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //A function to fill in the remaining paths
    public void fillInSurroundingPaths()
    {
        //will tolist fix my issue
        foreach (int room in rooms.ToList())
        {
            string dir = "";
            previousRoom = room;

            //Foreach direction in the tag
            foreach (char direct in gc.rooms[room].tag)
            {
                
                try
                {
                    //If the direction is left and the room to the left contains ER
                    if (direct == 'L' && gc.rooms[room - 1].tag.Contains("ER"))
                    {
                        

                        dir = checkDoorways((room - 1));
                       
                        //If direction is 1, adds an end room
                        if (dir.Length == 1)
                        {
                            newRoom((room - 1), endLayouts[2]);
                        }
                        else
                        {
                            //for each possible layout in layouts
                            foreach (GameObject layout in layouts)
                            {
                                //if the tag equals the directio, creates a new room
                                if (order(layout.tag) == order(dir))
                                {
                                    newRoom((room - 1), layout);
                                    break;
                                }
                            }
                        }
                    }

                    //If the direction is right and the room to the right contains ER
                    if (direct == 'R' && gc.rooms[room + 1].tag.Contains("ER"))
                    {
                        dir = checkDoorways((room + 1));
    
                        if (dir.Length == 1)
                        {
                            newRoom((room + 1), endLayouts[1]);
                        }
                        else
                        {
                            foreach (GameObject layout in layouts)
                            {
                                if (order(layout.tag) == order(dir))
                                {
                                    newRoom((room + 1), layout);
                                    break;
                                }
                            }
                        }
                    }

                    //If the direction is above and the room to above contains ER
                    if (direct == 'U' && gc.rooms[room - 10].tag.Contains("ER"))
                    {
                        dir = checkDoorways((room - 10));
                                                print(dir);

                        if (dir.Length == 1)
                        {
                            print("Checking Room Above");

                            newRoom((room - 10), endLayouts[0]);
                            print("One Length Room Above");
                            print(endLayouts[0]);
                        }
                        else
                        {
                            foreach (GameObject layout in layouts)
                            {
                                if (order(layout.tag) == order(dir))
                                {
                                    print("Create NEW ROOM");

                                    newRoom((room - 10), layout);
                                    break;
                                }
                            }
                        }
                    }

                    //If the direction is down and the room below contains ER
                    if (direct == 'D' && gc.rooms[room + 10].tag.Contains("ER"))
                    {
                        dir = checkDoorways(room + 10);

                        if (dir.Length == 1)
                        {     
                            newRoom((room + 10), endLayouts[3]);
                        }
                        else
                        {
                            foreach (GameObject layout in layouts)
                            {
                                if (order(layout.tag) == order(dir))
                                {
                                    newRoom((room + 10), layout);
                                    break;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    print("Beep Boop. Out of Index error, maybe?");
                }
            }
        }
    }

    //A function to figure out what directions are needed when filling in rooms;
    //@param: centre - the centre of the room
    private string checkDoorways(int centre)
    {
        neededDir = "";

        //check room to the right for a left door
        if (gc.rooms[centre + 1].tag.Contains("L"))
        {
            neededDir += "R";
        }

        //check room to the left for a right door
        if (gc.rooms[centre - 1].tag.Contains("R") && gc.rooms[centre - 1].tag != ("ER"))
        {
            neededDir += "L";
        }

        //check room above for a down door
        if (((centre - 10) >= 0) && gc.rooms[centre - 10].tag.Contains("D"))
        {
            neededDir += "U";
        }

        //check room below for a up door
        if (((centre + 10 <= 100)) && gc.rooms[centre + 10].tag.Contains("U"))
        {
            neededDir += "D";
        }

        return neededDir;
    }

    //A function to alphabeticise
    //@param: strng as string
    private static string order(string strng)
    {
        // Convert to char array.
        List<char> tempList = new List<char>();
        tempList.AddRange(strng);

        // Sort letters.
        tempList.Sort();

        // Return modified string.
        return string.Join("", tempList);
    }

    //A function to return a random room
    public int randomRoom() { return rooms[Random.Range(0, rooms.Count)];}
}
