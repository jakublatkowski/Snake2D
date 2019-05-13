using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*<summary>
 * GameManager manages game settings and loads appropriate scenes.
 * There is also storing information about game over
 * </summary>
 */
public class GameManager : MonoBehaviour
{    

    [SerializeField]
    [Range(.1f, 1f)]
    private float _gameSpeed = .3f;

    //static reference to existing instance of GameManager
    public static GameManager instance;

    public bool gameOver = false;
    public bool win = false;

    public float GameSpeed
    {
        get { return _gameSpeed; }
        set
        {
            if (_gameSpeed < .1f)
            {
                Debug.LogWarning("Interval lower than .1f");
                _gameSpeed = .1f;
            }
            else if (_gameSpeed > 1f)
            {
                Debug.LogWarning("Interval greater than 1");
                _gameSpeed = 1f;
            }
            else
            {
                _gameSpeed = value;
            }
        }
    }

    /*<summary>
    * Awake Method checks if there is any GameManager and if not creates a new one;
    *</summary>   
    */
    void Awake()
    {
        
        if(GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            //if other GameManager exists destroy new one
            Destroy(gameObject);
            return;
        }

        // allows data transfering between scenes
        DontDestroyOnLoad(gameObject);

    }

    /*<summary>
     * Play method is called from main menu and loads a game scene.
     * It allows to start playing.
     * </summary>
     */
    public void Play()
    {
        gameOver = false;
        win = false;

        SceneManager.LoadScene("GameScene");
    }

     /*<summary>
     * Quit method is called from main menu and quits an application.
     * </summary>
     */
    public void Quit()
    {
        Application.Quit();
    }

    /*<summary>
    * GoBackToMainMenu method is called from game over menu and loads a main menu scene.
    * </summary>
    */
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*<summary>
    * ReloadScene method is called from game over menu and reloads a game scene.
    * It starts a fresh new game after game over.
    * </summary>
    */
    public void ReloadScene()
    {
        gameOver = false;
        win = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*<summary>
    * GameOver method is called when head collides with wall or tail.
    * It stops a game and allow to turn on game over menu
    * </summary>
    */
    public void GameOver()
    {
        gameOver = true;

        try
        {
            //game over menu is activated from UIController script
            FindObjectOfType<UIController>().GameOver();
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("UIController: " + e.Message);
        }
    }
}