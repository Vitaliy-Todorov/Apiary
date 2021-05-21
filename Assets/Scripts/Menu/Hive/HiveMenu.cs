using UnityEngine;
using UnityEngine.UI;

public class HiveMenu : MonoBehaviour
{
    [SerializeField]
    Button exitButton;

    [Header("Меню действий")]
    [SerializeField]
    GameObject actionsHive;
    [SerializeField]
    Button collectBeesInHive;
    [SerializeField]
    Button sendForСollection;
    [SerializeField]
    Button actions;

    [Header("Меню информации")]
    [SerializeField]
    GameObject informationHive;
    [SerializeField]
    Slider honey;
    [SerializeField]
    Text bees;
    [SerializeField]
    Hive hive;
    [SerializeField]
    Button back;

    ActionsHive actionsHiveState;
    InformationHive informationHiveState;

    int maxBees;

    private void Start()
    {
        informationHiveState = new InformationHive(informationHive);
        actionsHiveState = new ActionsHive(actionsHive);

        collectBeesInHive.onClick.AddListener(CollectBeesInHive);
        sendForСollection.onClick.AddListener(SendForСollection);
        actions.onClick.AddListener(Actions);
        back.onClick.AddListener(Back);
        exitButton.onClick.AddListener(ExitPressed);
    }

    void Actions()
    {
        actionsHiveState.OnEnter();
        informationHiveState.OnExit();
    }

    void Back()
    {
        informationHiveState.OnEnter();
        actionsHiveState.OnExit();
    }

    void CollectBeesInHive()
    {
        hive.CollectBeesInHive();
    }

    void SendForСollection()
    {
        hive.SendForСollection();
    }

    void ExitPressed()
    {
        gameObject.SetActive(false);
    }

    public void MaxValue(float maxHoney, int maxBees)
    {
        honey.maxValue = maxHoney;
        this.maxBees = maxBees;
    }

    public void SetHoney(float health)
    {
        honey.value = health;
    }

    public void SetBees(int bee)
    {
        bees.text = bee + "/" + maxBees;
    }
}
