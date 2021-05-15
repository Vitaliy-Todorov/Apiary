using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    Button mainMenuButton;

    [SerializeField]
    Button exitButton;

    private void Start()
    {
        Time.timeScale = 0;

        mainMenuButton.onClick.AddListener(MainMenu);

        exitButton.onClick.AddListener(ExitPressed);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Запускаем время после загрузки
        Time.timeScale = 1;
    }

    void ExitPressed()
    {
        Application.Quit();
    }
}
