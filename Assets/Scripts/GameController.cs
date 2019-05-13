using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*<summary>
 * GameController creates an initial game filed, and first snake.
 * It is responsible for snae movement, and delivers prefabs to instantiate all game objects
 * </summary>
 */
public class GameController : MonoBehaviour
{
    [Header("Prefabs:")]
    public GameObject fieldPrefab;
    public GameObject wallPrefab;
    public GameObject headPrefab;
    public GameObject tailPrefab;
    public GameObject foodPrefab;
    public GameObject specialFoodPrefab;

    [Header("Time:")]
    [Range(.1f, 1.0f)]
    public float interval = .3f;
    private float nextFrameTime;
    [Range(1f, 10f)]
    public float maxTimeToNewSpecialFood = 3f;
    private float timeToNewSpecialFood;

    public static List<SnakeElement> snake;

    public static bool inverseActive = false;

    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    // Start is called before the first frame update

    /*<summary>
     * Starts creating a new gamefield, and creating first snake.
     * Then it's adjusting an game speed interval, and special food time.
     * </summary>
     */
    void Start()
    {
        //to be sure if it's set up
        GameManager.instance.gameOver = false;

        //interval from options
        interval = GameManager.instance.GameSpeed;

        nextFrameTime = Time.time + interval;
        timeToNewSpecialFood = Random.Range(0f, maxTimeToNewSpecialFood); //generating time to instantiate a new special food object

        //generate GameField
        Field field = fieldPrefab.gameObject.GetComponent<Field>();
        field.CreateField();

        //create wall;
        Wall wall = wallPrefab.gameObject.GetComponent<Wall>();
        wall.BuildWall(field);

        //snake initialization
        snake = new List<SnakeElement>();

        SnakeElement head = headPrefab.gameObject.GetComponent<SnakeHead>();
        head.AddElement(snake);

        for (int i = 0; i < 4; i++)
        {
            SnakeElement tail = tailPrefab.gameObject.GetComponent<SnakeTail>();
            tail.AddElement(snake);
            SnakeElement.MoveTail(snake);
            SnakeElement.MoveHead(snake);
        }


        //food creation
        Food food = foodPrefab.gameObject.GetComponent<Food>();
        food.AddNewOne();

    }

    // Update is called once per frame
    /*<summary>
     * Update is responsible for movement, ar for instantiation of special food.
     * </summary>
     */
    void Update()
    {
        //if game is over don't want snake to move
        if (GameManager.instance.gameOver)
        {
            return;
        }

        //to be sure if game could be continued if for some reason food was not created
        if(GameObject.FindGameObjectsWithTag("NormalFood").Length == 0)
        {
            foodPrefab.GetComponent<Food>().AddNewOne();
        }

        //counting time to instantiate new special food object
        timeToNewSpecialFood -= Time.deltaTime;

        if(timeToNewSpecialFood <= 0) // if all time has passed
        {
            //create new special food object
            SpecialFood sf = specialFoodPrefab.GetComponent<SpecialFood>();
            sf.AddNewOne();

            //random new time to create new special food object
            //range is selected to not create new object before old one is not expired
            timeToNewSpecialFood = Random.Range(2 * sf.timeToDestroy, 2 * sf.timeToDestroy + maxTimeToNewSpecialFood);
        }

        //if there is not a time for a next move
        if (Time.time < nextFrameTime) return;

        nextFrameTime += interval; // set up a new move time

        SnakeElement.MoveTail(snake); //firstly move a snake tail

        SnakeHead head = snake[0] as SnakeHead; // head will always be firs member

        //check if rotation is set up
        if (isTurningLeft)
        {
            isTurningLeft = false;
            head.RotateHeadCounterClockwise();
           
        }else if (isTurningRight)
        {
            isTurningRight = false;
            head.RotateHeadClockwise();
        }

        SnakeElement.MoveHead(snake); // move head including rotation
    }

    //activate an apropriate turn and disactivate other one
    public void TurnLeft()
    {
        isTurningLeft = true;
        isTurningRight = false;
    }

    //activate an apropriate turn and disactivate other one
    public void TurnRight()
    {
        isTurningRight = true;
        isTurningLeft = false;
    }

}