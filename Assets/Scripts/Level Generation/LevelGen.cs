using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GridController gc;

    public GameObject[] layouts; //all layouts excluding ends

    public GameObject[] endLayouts; //all rooms that only have one exit/entrance

    private int previousRoom; // the previous room generated

    private bool firstRun { get; set; } = true; //if first run of generation

    private int mainPathLength { get; set; } // the length of the main path

    void Start()
    {
        mainPathLength = Random.Range(6, 9);
    }

    // A function to generate the start location
    public void generateStartLocation()
    {
        // Stores the current poisition in list
        int index = Random.Range(0, gc.rooms.Count);
        print("START INDEX HERE: " + index);
        // Gets random start from the list of potential start locations
        GameObject startLoc = gc.rooms[index];

        newRoom(startLoc, index);
    }

    //A function to create a new room
    // nl = the room to be replaced
    // index = position in the list of rooms
    private void newRoom(GameObject nl, int index)
    {
        GameObject layout = generateRoomLayout();

        // While it doesn't fit the conditions
        while (
            placementValidation(layout, index) == false || checkPreviousRoom(layout, index) == false
        )
        {
            // If length of tag = 1, means it is one of the rooms with only one exit
            if (
                firstRun == true
                && placementValidation(layout, index) == true
                && layout.tag.Length != 1
            )
            {
                firstRun = false;
                break;
            }
            else if (firstRun == false)
            {
                layout = generateRoomLayout();
                print("not first run");
                continue;
            }
            else
            {
                layout = generateRoomLayout();
            }
        }

        GameObject room = Instantiate(
            layout,
            nl.transform.position,
            Quaternion.identity,
            nl.transform.parent
        );

        Destroy(nl); //removes old room
        room.name = index.ToString();
        gc.rooms[index] = room;
        previousRoom = index;
    }

    // A function to create a new end room;
    //@param: index - position in the list of rooms
    private void newRoom(int index)
    {
        print("Last Room: " + index);
        GameObject layout = generateEndRoomLayout();

        // While it doesn't fit the conditions
        while (
            placementValidation(layout, index) == false || checkPreviousRoom(layout, index) == false
        )
        {
            // If length of tag = 1, means it is one of the rooms with only one exit
            if (
                placementValidation(layout, index) == true
                && checkPreviousRoom(layout, index) == true
            )
            {
                break;
            }
            else
            {
                layout = generateEndRoomLayout();
            }
        }

        GameObject room = Instantiate(
            layout,
            gc.rooms[index].transform.position,
            Quaternion.identity,
            gc.rooms[index].transform.parent
        );

        Destroy(gc.rooms[index]); //removes old room
        room.name = index.ToString();
        gc.rooms[index] = room;
        previousRoom = index;
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
            print(i);
            GameObject prevRoom = gc.rooms[previousRoom];

            List<char> banned = new List<char>();

            // random direction
            char dir = prevRoom.tag[Random.Range(0, prevRoom.tag.Length)];

            int index;

            while (dir != -1)
            {
                try
                {
                    if (dir == 'U' && !banned.Contains('U'))
                    {
                        index = previousRoom - 10;
                        newRoomSwitch(i, index);
                        break;
                    }
                    else if (dir == 'D' && !banned.Contains('D'))
                    {
                        index = previousRoom + 10;
                        newRoomSwitch(i, index);
                        break;
                    }
                    else if (dir == 'L' && !banned.Contains('L'))
                    {
                        index = previousRoom - 1;
                        newRoomSwitch(i, index);
                        break;
                    }
                    else if (dir == 'R' && !banned.Contains('R'))
                    {
                        index = previousRoom + 1;
                        newRoomSwitch(i, index);
                        break;
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
                    dir = prevRoom.tag[Random.Range(0, prevRoom.tag.Length)];
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
    private bool placementValidation(GameObject layout, int ind)
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
        // if the previous room doesn't have the corresponding direction
        //so if previous room doesn't have left in its tag, and the new room has right, recalculate
        if (
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
            gc.rooms[previousRoom].tag.Contains("U")
            && layout.tag.Contains("D")
            && ind == previousRoom - 10
        )
        {
            print("ae2");
            return true;
        }
        else if (
            gc.rooms[previousRoom].tag.Contains("D")
            && layout.tag.Contains("U")
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
}
