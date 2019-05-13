using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 * SnakeElement is semi abstract class which stores a common variables for snake elements.
 * Also it includes method to move elements during the game
 * </sumary>
 */
public class SnakeElement : MonoBehaviour
{
    public enum Direction { UP, LEFT, DOWN, RIGHT }

    private Direction currentDirection;

    //position of element connected to field coordinates
    private int _xPos = 2;
    private int _yPos = 2;

    public int xPos
    {
        get { return _xPos; }
        set
        {
            if (value >= Field.fields.Length)
            {
                Debug.Log("Head X Pos higher than allowed");
                _xPos = value % Field.fields.Length;
            }
            else if (value < 0)
            {
                Debug.Log("Head X Pos lower than allowed");
                _xPos = Field.fields.Length - 1;
            }
            else _xPos = value;
        }
    }

    public int yPos
    {
        get { return _yPos; }
        set
        {
            if (value >= Field.fields[0].Length)
            {
                Debug.Log("Head Y Pos higher than allowed");
                _yPos = value % Field.fields[0].Length;
            }
            else if (value < 0)
            {
                Debug.Log("Head Y Pos lowet than allowed");
                _yPos = Field.fields[0].Length - 1;
            }
            else _yPos = value;
        }
    }

    public Direction CurrentDirection
    {
        get { return currentDirection; }
        set { currentDirection = value; }
    }

    /*<summary>
     * AddElement adds new element to a given SnakeElement list.
     * Head at the beginning, tail at the end.
     * Position of new element is setting from appropriate field for head,
     * and from position of current last tail element for SnakeTail.
     * </summary>
     */
    public virtual void AddElement(List<SnakeElement> snakeElements) { }

    /*<summary>
    * MoveTail method moves tail element in a given SnakeElement list.
    * It starts at the end and goes to beginning. Setting up position and rotation to element from previous element
    * </summary>
    */
    public static void MoveTail(List<SnakeElement> snake)
    {
        
        for (int i = snake.Count - 1; i >= 1; i--)
        {
            //assign position and rotation
            snake[i].gameObject.transform.position = snake[i - 1].gameObject.transform.position;
            snake[i].gameObject.transform.rotation = snake[i - 1].gameObject.transform.rotation;

            if(i == snake.Count - 1)
            {
                //if there was last element set field as empty
                Field.fields[snake[i].xPos][snake[i].yPos].IsEmpty = true;
            }

            //assign position connected to field coordinates
            snake[i].xPos = snake[i - 1].xPos;
            snake[i].yPos = snake[i - 1].yPos;

            //mark occupied field as not empty
            Field.fields[snake[i].xPos][snake[i].yPos].IsEmpty = false;

        }
    }

    /*<summary>
    * MoveHead method moves head element in a given SnakeElement list.
    * It checks a direction, rotate head, and move it to a new position
    * </summary>
    */
    public static void MoveHead(List<SnakeElement> snake)
    { 
        SnakeHead head = snake[0] as SnakeHead;

        switch (head.CurrentDirection)
        {
            case SnakeElement.Direction.UP:
                {
                    head.yPos++;
                    break;
                }
            case SnakeElement.Direction.RIGHT:
                {
                    head.xPos++;
                    break;
                }
            case SnakeElement.Direction.DOWN:
                {
                    head.yPos--;
                    break;
                }
            case SnakeElement.Direction.LEFT:
                {
                    head.xPos--;
                    break;
                }

        }

        //set up position connected with game field
        Vector3 pos = Field.fields[head.xPos][head.yPos].gameObject.transform.position;
        pos.z = -1;
        head.gameObject.transform.position = pos;

        //mark occupied field as not empty
        Field.fields[head.xPos][head.yPos].IsEmpty = false;
    }
}