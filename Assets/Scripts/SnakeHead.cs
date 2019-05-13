using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 * SnakeHead imlements methods needed to rotate head and check collisons.
 * </summary>
 */
public class SnakeHead :  SnakeElement
{

    //on start adding rigidbody needet to detect collisons
    private void Start()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    /*<summary>
     * OnTriggerEnter checks with wihich object head is colliding.
     * When collision is detected, AudioManager is calling to play sound.
     * </summary>
     */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        //first tail element. Head can collide with it because of specificaton of tail movement.
        //It does not crush the game, because it is not posible to bump into first tail element,
        //because head can not be turned 180 degrees
        if (other.gameObject == GameController.snake[1])
            return;

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Tail")
        {
            //play sound
            AudioManager.instance.PlaySound("GameOver");

            //gameover
            GameManager.instance.GameOver();

            return;
        }

        if (other.gameObject.tag == "NormalFood")
        {
            //playSound
            AudioManager.instance.PlaySound("NormalFood");

            //add points
            GameObject.FindObjectOfType<UIController>().AddPoints(1);

            //instantiate new food
            other.gameObject.GetComponent<Food>().AddNewOne();
        }

        if (other.gameObject.tag == "SpecialFood")
        {
            //play sound
            AudioManager.instance.PlaySound("SpecialFood");

            //add points
            GameObject.FindObjectOfType<UIController>().AddPoints(10);
        }

        //destroy existing
        Destroy(other.gameObject);

        //add snake tail
        SnakeTail tail = GameController.snake[GameController.snake.Count - 1] as SnakeTail;
        tail.AddElement(GameController.snake);
    }

    public void RotateHeadCounterClockwise()
    {
        gameObject.transform.rotation *= Quaternion.Euler(0, 0, 90);

        switch (CurrentDirection)
        {
            case Direction.UP:
                {
                    CurrentDirection = Direction.LEFT;
                    break;
                }
            case Direction.LEFT:
                {
                    CurrentDirection = Direction.DOWN;
                    break;
                }
            case Direction.DOWN:
                {
                    CurrentDirection = Direction.RIGHT;
                    break;
                }
            case Direction.RIGHT:
                {
                    CurrentDirection = Direction.UP;
                    break;
                }
        }
    }

    public void RotateHeadClockwise()
    {
        gameObject.transform.rotation *= Quaternion.Euler(0, 0, -90);

        switch (CurrentDirection)
        {
            case Direction.UP:
                {
                    CurrentDirection = Direction.RIGHT;
                    break;
                }
            case Direction.RIGHT:
                {
                    CurrentDirection = Direction.DOWN;
                    break;
                }
            case Direction.DOWN:
                {
                    CurrentDirection = Direction.LEFT;
                    break;
                }
            case Direction.LEFT:
                {
                    CurrentDirection = Direction.UP;
                    break;
                }
        }
    }
    
    // see SnakeElement
    public override void AddElement(List<SnakeElement> snakeElements)
    {
        Vector3 position = Field.fields[xPos][yPos].transform.position;
        position.z = -1;

        snakeElements.Insert(0, Instantiate(gameObject, position, Quaternion.Euler(0, 0, 180)).GetComponent<SnakeHead>());
    }

}
