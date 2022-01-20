using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startGame;
    public Button quitGame;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Button btn = startGame.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);

        btn = quitGame.GetComponent<Button>();
        btn.onClick.AddListener(QuitGame);

    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    void QuitGame()
    {
        Application.Quit();
    }


}
