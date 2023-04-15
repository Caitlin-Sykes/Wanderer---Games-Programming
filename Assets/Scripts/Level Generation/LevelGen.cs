using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelGen : MonoBehaviour
{
    public GridController gc;

    public GameObject[] layouts; //all layouts excluding ends

    public GameObject[] endLayouts; //all rooms that only have one exit/entrance

    private int previousRoom { get; set; } = -1; // the previous room generated

    private bool firstRun { get; set; } = true; //if first run of generation

    private int mainPathLength { get; set; } // the length of the main path

    void Start()
    {
        mainPathLength = Random.Range(6, 9);
    }

    // A function to generate the start location
    public void generateStartLocation()
    {
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
        GameObject layout = generateRoomLayout();
        print(index);
        while (
            gridPlacementValidation(layout, index) == false
            || checkSurroundingRoom(layout, index) == false || checkPreviousRoom(layout, index) == false
        )
        {
            //If it is on its first run (and therefore doesn't have a previous room)
            if (firstRun == true && gridPlacementValidation(layout, index) == true)
            {
                firstRun = false;
                break;
            }

            //if first run is false and meets conditions, breaks from loop
            else if (firstRun == false && gridPlacementValidation(layout, index) == true
            && checkSurroundingRoom(layout, index) == true && checkPreviousRoom(layout, index) == true
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

        
        Destroy(nl); //removes old room
        room.name = index.ToString(); //sets the name of the room to the index
        gc.rooms[index] = room; //replaces room in list with new room
        previousRoom = index; //previous room is set to index
    }

    // A function to create a new end room;
    //@param: index - position in the list of rooms
    private void newRoom(int index)
    {

        GameObject layout = generateEndRoomLayout();

        while (
            gridPlacementValidation(layout, index) == false
            || checkSurroundingRoom(layout, index) == false
            || checkPreviousRoom(layout, index) == false
        )
        {

            //if first run is false and meets conditions, breaks from loop
            if (gridPlacementValidation(layout, index) == true
            && checkSurroundingRoom(layout, index) == true
            && checkPreviousRoom(layout, index) == true)
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
        DEBUG("GridPlacement", layout, ind);
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
            if (layout.tag.Contains("U") || layout.tag.Contains("R"))
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
            if (layout.tag.Contains("D") || layout.tag.Contains("R"))
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
            else if (ind.ToString().Contains("0"))
            {
                if (layout.tag.Contains("L"))
                {
                    return false;
                }
            }
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
        DEBUG("prevRooms", layout, ind);
        // if the previous room doesn't have the corresponding direction
        //so if previous room doesn't have left in its tag, and the new room has right, recalculate
        //will only be -1 on first run
        if (previousRoom == -1)
        {
            return true;
        }
        else if (
            layout.tag.Contains("L")
            && gc.rooms[previousRoom].tag.Contains("R")
            && ind == previousRoom + 1
        )
        {
            // can't happen
            print("a");
            return true;
        }
        else if (
            layout.tag.Contains("R")
            && gc.rooms[previousRoom].tag.Contains("L")
            && ind == previousRoom - 1
        )
        {
            return true;
        }
        else if (
            gc.rooms[previousRoom].tag.Contains("U") //I HAVE SWAPPED THESE TWO
            && layout.tag.Contains("D")
            && ind == previousRoom - 10
        )
        {
            print("ae2");
            return true;
        }
        else if (
            gc.rooms[previousRoom].tag.Contains("D")
            && layout.tag.Contains("U") ///I HAVE SWAPPED THESE TWO
            && ind == previousRoom + 10
        )
        {
            print("ae3");
            return true;
        }
        else
        {
            print("ae4");
            return false;
        }
    }

    //Checks the surrounding rooms to make sure paths connect
    //@param: layout - the room layout
    //@param: ind - index of the current room
    private bool checkSurroundingRoom(GameObject layout, int ind)
    {
        DEBUG("SurroundingRooms", layout, ind);

        //checks room above
        if (
            ind > 9
            && layout.tag.Contains("U")
            && !(gc.rooms[ind - 10].tag.Contains("D") || gc.rooms[ind - 10].tag.Contains("ER"))
        )
        {
            DEBUG("first", gc.rooms[ind - 10], -1);
            return false;
        }
        //checks room to the left
        else if (
            !ind.ToString().Contains("0")
            && layout.tag.Contains("L")
            && !(gc.rooms[ind - 1].tag.Contains("R") || gc.rooms[ind - 1].tag.Contains("E"))
        )
        {
            DEBUG("second", gc.rooms[ind - 10], -2);
            return false;
        }
        //checks room to the right
        else if (
            !ind.ToString().Contains("9")
            && layout.tag.Contains("R")
            && !(gc.rooms[ind + 1].tag.Contains("L") || gc.rooms[ind + 1].tag.Contains("E"))
        )
        {
            DEBUG("third", gc.rooms[ind - 10], -3);
            return false;
        }
        //checks room below
        else if (
            ind < 90
            && layout.tag.Contains("D")
            && !(gc.rooms[ind + 10].tag.Contains("U") || gc.rooms[ind + 10].tag.Contains("E"))
        )
        {
            DEBUG("fourth", gc.rooms[ind - 10], -4);
            return false;
        }

        return true;
    }


    //debug function
    //to be removed in future as i borrowed it and haven't cited it
    //if i forget, really sorry, this isn't mine and should have been removed.
    private void DEBUG(string func, GameObject stuff, int ind)
    {
        string path = "Debug/Infi.txt";

        //Write some text to the test.txt file

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(func + " " + stuff.tag + " " + ind);

        writer.Close();
    }

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
}
//no zero for left,
//no 9 for right
