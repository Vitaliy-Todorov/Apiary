using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour, IState
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
    public GameObject contentScrollView;
    [SerializeField]
    public GameObject menuHiveStatistics;
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

    public void OnEnter()
    {
        gameObject.SetActive(true);
        statisticsMenuState.OnExit();
        gameMenuState.OnEnter();
    }

    public void OnExit()
    {
        gameMenuState.OnExit();
        statisticsMenuState.OnEnter();
        gameObject.SetActive(false);
    }

    private void Awake()
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

    public GameObject InstantiateMenuHiveStatistics()
    {
        return Instantiate(menuHiveStatistics, contentScrollView.transform);
    }

    public void DestroyMenuHiveStatistics(List<GameObject> menuHiveStatisticsObjs)
    {
        for (int i = 0; menuHiveStatisticsObjs.Count > 0; i++)
        {
            Destroy(menuHiveStatisticsObjs[i]);
            menuHiveStatisticsObjs.RemoveAt(i);
            i--;
        }
    }
}
