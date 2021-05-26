using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]
    public Hive hives;

    [Header("Game Menu")]
    [SerializeField]
    GameObject gameMenu;
    [SerializeField]
    Button mainMenuButton;
    [SerializeField]
    Button statisticsButton;
    [SerializeField]
    Button exitButton;

    [Header("Меню статистики")]
    [SerializeField]
    public GameObject statisticsMenu;
    [SerializeField]
    public Text allHoney;
    [SerializeField]
    public Text allBess;
    [SerializeField]
    public Text allDrone;
    [SerializeField]
    Button backButton;

    GameMenu gameMenuState;
    StatisticsMenu statisticsMenuState;

    private void Start()
    {
        Time.timeScale = 0;

        gameMenuState = new GameMenu(gameMenu);
        statisticsMenuState = new StatisticsMenu(this);

        mainMenuButton.onClick.AddListener(MainMenu);

        statisticsButton.onClick.AddListener(Statistics);

        exitButton.onClick.AddListener(ExitPressed);

        backButton.onClick.AddListener(Back);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //Запускаем время после загрузки
        Time.timeScale = 1;
    }

    void Statistics()
    {
        gameMenuState.OnExit();
        statisticsMenuState.OnEnter();
        statisticsMenuState.SetValue();
    }

    void ExitPressed()
    {
        Application.Quit();
    }

    void Back()
    {
        statisticsMenuState.OnExit();
        gameMenuState.OnEnter();
    }
}
