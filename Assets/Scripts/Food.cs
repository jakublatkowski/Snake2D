using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 * Food is an object which is eaten by the Snake.
 * Normal food adds 1 point, special adds 10.
 * If food stays on field, it is set up as not empty
 * </summary>
 */
public class Food : MonoBehaviour
{
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        //adding some rotation for a nice effect
        gameObject.transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);
    }

    /*<summary>
     * AddNewOne checks if in game field are some empty fields and if there are, it random position of new one, 
     * place it there, and check bottom field as not empty
     * </summary>
      */
    public void AddNewOne()
    {
        int xSize = Field.fields.Length;
        int ySize = Field.fields[0].Length;

        //list with empty fields for ease of use randomization
        List<Field> emptyFields = new List<Field>();

        //searching for all empty fields
        for (int i = 1; i < xSize-1; i++) // avoid first and last field because there is a wall
        {
            for (int j = 1; j < ySize-1; j++)// ^^
            {
                if(Field.fields[i][j].IsEmpty)
                {
                    //if field is emty add it to randomization
                    emptyFields.Add(Field.fields[i][j]);
                }
            }
        }

        //if there is no empty field probably snake takes all game fields
        if(emptyFields.Count == 0)
        {
            Debug.Log("No more free space to create new Food");
            //don't have to generate new one, because there will be game over
            return;             
        }

        //random a new position of food
        int index = Random.Range(0, emptyFields.Count);

        //mark field as not empty
        emptyFields[index].IsEmpty = false;
        Vector3 pos = emptyFields[index].gameObject.transform.position;
        pos.z = -1f;

        //instantiate a new food object
        Food newOne = Instantiate(gameObject, pos, new Quaternion()).GetComponent<Food>();
        newOne.gameObject.name = gameObject.name;
    }
}