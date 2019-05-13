using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*<summary>
 * Wall limits game field. Occupied fields are marked as not empty.
 * On collision with head game is over.
 * </summary>
 */
public class Wall : MonoBehaviour
{
    public static List<Wall> wall;

    /*<summary>
     * BuildWall builds wall around gamefield, and marks all occupied fields as not empty
     * </summary>
     */
    public void BuildWall(Field field)
    {
        wall = new List<Wall>();

        for (int i = 0; i < field.xSize; i++)
        {
            for (int j = 0; j < field.ySize; j++)
            {
                if (i == 0 || i == field.xSize - 1 || j == 0 || j == field.ySize - 1)
                {
                    Field.fields[i][j].IsEmpty = false;
                    Vector3 position = Field.fields[i][j].gameObject.transform.position;
                    position.z = -1;
                    wall.Add(Instantiate(gameObject, position, new Quaternion()).GetComponent<Wall>());
                }
            }
        }
    }
}
