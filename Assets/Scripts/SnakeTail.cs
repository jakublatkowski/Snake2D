using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 * SnakeTail is an element od snake which is moving after head element.
 * When head collides with it - game is over.
 * </summary>
 */
public class SnakeTail : SnakeElement
{
    //see SnakeElement
    public override void AddElement(List<SnakeElement> snakeElements)
    {   

        //position and rotation from current last Snake Element
        Vector3 position = snakeElements[snakeElements.Count - 1].gameObject.transform.position;
        Quaternion rotation = snakeElements[snakeElements.Count - 1].gameObject.transform.rotation;

        SnakeTail newTail = Instantiate(gameObject, position, rotation).GetComponent<SnakeTail>();
        newTail.gameObject.name = gameObject.name;

        snakeElements.Add(newTail);
    }
}