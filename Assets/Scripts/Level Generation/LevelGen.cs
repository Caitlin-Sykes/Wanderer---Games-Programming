using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GridController gc;

    public GameObject[] layouts;

    private int previousRoom;

    private bool firstRun {get; set;} = true;

    // A function to generate the start location
    public void generateStartLocation()
    {
        // Stores the current poisition in list
        int index = Random.Range(0, gc.rooms.Count);

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
        print(layout.tag);

        // While it doesn't fit the conditions
        while (placementValidation(layout, index) == false)
        {
            if (firstRun == true) {
                layout = generateRoomLayout();
                firstRun = false;
                print("first run");
            }

            else {
                if (checkPreviousRoom(layout) == false) {
                    layout = generateRoomLayout();
                }

                else {
                    print("true");
                    break;
                }
            }
            // StartCoroutine(checkPreviousRoom(layout));

            // else if (firstRun == false && checkPreviousRoom(layout) == false) {
            //     layout = generateRoomLayout();
            // }
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

    //Generates room layout
    private GameObject generateRoomLayout()
    {
        return layouts[Random.Range(0, layouts.Length)] as GameObject;
    }

    public void mainPath()
    {
        GameObject prevRoom = gc.rooms[previousRoom];

        List<char> banned = new List<char>();

        // random char
        char dir = prevRoom.tag[Random.Range(0, prevRoom.tag.Length)];

        int index;
        print(dir);
        while (dir != -1)
        {
            try
            {
                if (dir == 'U' && !banned.Contains('U'))
                {
                    index = previousRoom - 10;
                    newRoom(gc.rooms[index], index);
                    break;
                }
                else if (dir == 'D' && !banned.Contains('D'))
                {
                    index = previousRoom + 10;
                    newRoom(gc.rooms[index], index);
                    break;
                }
                else if (dir == 'L' && !banned.Contains('L'))
                {
                    index = previousRoom - 1;
                    newRoom(gc.rooms[index], index);
                    break;
                }
                else if (dir == 'R' && !banned.Contains('R'))
                {
                    index = previousRoom + 1;
                    newRoom(gc.rooms[index], index);
                    break;
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

    //validate placement
    //@param: layout as the room layout
    //@param: index the index of the current room
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
            print("wrong5");
        }
        else if (ind == 9)
        {
            // cannot be up or right
            if (layout.tag.Contains("U") || layout.tag.Contains("R"))
            {
                print("ping");
                return false;
            }
            print("wrong6");
        }
        else if (ind == 90)
        {
            // cannot be down or left
            if (layout.tag.Contains("D") || layout.tag.Contains("L"))
            {
                return false;
            }
            print("wrong7");
        }
        else if (ind == 99)
        {
            // cannot be down or right
            if (layout.tag.Contains("D") || layout.tag.Contains("R"))
            {
                return false;
            }
            print("wrong8");
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
                print("wrong");
            }
            //This equates to the bottom row
            else if (ind >= 91 && ind <= 98)
            {
                if (layout.tag.Contains("D"))
                {
                    return false;
                }
                print("wrong2");
            }
            else if (ind.ToString().Contains("0"))
            {
                if (layout.tag.Contains("L"))
                {
                    return false;
                }
                print("wrong3");
            }
            else if (ind.ToString().Contains("9"))
            {
                if (layout.tag.Contains("R"))
                {
                    print("wrong4");

                    return false;
                }
            }
        }

        return true;
    }

    //checks previous room to make sure paths connect
    private bool checkPreviousRoom(GameObject layout) {
        
        // if the previous room doesn't have the corresponding direction
        //so if previous room doesn't have left in its tag, and the new room has right, recalculate
        if (gc.rooms[previousRoom].tag.Contains("L") && layout.tag.Contains("R")) {
            // can't happen
            print("a");
            return true;
        }

        else if (gc.rooms[previousRoom].tag.Contains("R") && layout.tag.Contains("L")) {
            return true;
        }

        else if (gc.rooms[previousRoom].tag.Contains("U") && layout.tag.Contains("D")) {
            print("ae2");
            return true;
        }

        else if (gc.rooms[previousRoom].tag.Contains("D") && layout.tag.Contains("U")) {
            print("ae3");
            return true;
        }

        else {
            print("ae4");
            return false;
        }

    }

    // IEnumerator checkPreviousRoom(GameObject layout) {
        
    //     // if the previous room doesn't have the corresponding direction
    //     //so if previous room doesn't have left in its tag, and the new room has right, recalculate
    //     if (gc.rooms[previousRoom].tag.Contains("L") && layout.tag.Contains("R")) {
    //         // can't happen
    //         print("true");
    //         // return true;
    //     }

    //     else if (gc.rooms[previousRoom].tag.Contains("R") && layout.tag.Contains("L")) {
    //         print("true");
    //     }

    //     else if (gc.rooms[previousRoom].tag.Contains("U") && layout.tag.Contains("D")) {
    //         print("true");
    //     }

    //     else if (gc.rooms[previousRoom].tag.Contains("D") && layout.tag.Contains("U")) {
    //         print("true");
    //         print("true");
    //     }

    //     else {
    //         print("false");
    //         yield return null;
    //     }

    // }
}
