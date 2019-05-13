using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary>
 * Field class is used to create gamefield, on witch game is playing.
 * </summary>
 */
public class Field : MonoBehaviour
{
    public static Field[][] fields;

    [Header("Position:")]
    [SerializeField]
    private float _startX = -2.195f;
    [SerializeField]
    private float _startY = -4.32f;

    // Is any object on this field
    private bool _isEmpty = true;

    public bool IsEmpty
    {
        get { return _isEmpty; }
        set { _isEmpty = value; }
    }

    public float startX
    {
        get { return _startX; }
        set { _startX = value; }
    }

    public float startY
    {
        get { return _startY; }
        set { _startY = value; }
    }

    [Header("Size:")]
    public int xSize = 7;
    public int ySize = 12;

    /*<summary>
     * CreateField creates game field starting from a given point and with given size;
     * It creates an array of fields, which will be used to define position of every object in a game
     * </summary>
     */
    public void CreateField()
    {
        fields = new Field[xSize][];

        for (int i = 0; i < xSize; i++)
        {
            fields[i] = new Field[ySize];

            for (int j = 0; j < ySize; j++)
            {
                //position of new field is calculating from starting point and shifted by localscale i times
                Vector3 position = new Vector3(startX + gameObject.transform.localScale.x * i, startY + gameObject.transform.localScale.y * j);

                //new field is added to an array.
                fields[i][j] = Instantiate(gameObject, position, new Quaternion()).GetComponent<Field>();
            }
        }
    }

    //changing scale of single field
    public void ChangeScale(Vector3 scale)
    {
        gameObject.transform.localScale = scale;
    }

}