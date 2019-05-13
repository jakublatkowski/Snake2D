using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*<summary>
 * UIController updates and stores points.
 * It shows game over menu.
 * </summary>
 */
public class UIController : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public Button leftButton;
    public Button rightButton;

    public GameObject gameOverPanel;
    public TextMeshProUGUI endGamePoints;

    int _points = 0;
    public void AddPoints(int points)
    {
        _points += points;
        pointText.text = "Points: " + _points.ToString();
    }

    /*<summary>
     * GameOver method deactivate control buttons and shows game over menu and shows point summary.
     * </summary>
     */
    public void GameOver()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);

        endGamePoints.text = pointText.text;
        pointText.gameObject.SetActive(false);

        gameOverPanel.SetActive(true);
    }
}
