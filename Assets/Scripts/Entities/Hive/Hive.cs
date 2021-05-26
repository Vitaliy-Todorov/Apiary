using System.Collections;
using UnityEngine;

public class Hive : MonoBehaviour, IOnClick
{
    [SerializeField]
    HiveParameters parameters;
    [SerializeField]
    GameObject hiveMenu;
    HiveMenu menu;

    public SpawningBees _stateSpawningBees;
    public CollectGiveHoneyHive _stateCollectGiveHoneyHive;
    public DeathBees _stateDeathBees;

    float currentHoneyStocks = 0;

    protected IEnumerator createObject;
    protected IEnumerator beeInHives;

    public float СurrentHoneyStocks { get => currentHoneyStocks; set => currentHoneyStocks = value; }

    void Start()
    {
        //Подключаем меню улья
        menu = hiveMenu.GetComponent<HiveMenu>();
        menu.MaxValue(parameters.maxHoney, parameters.maxNumberObject);
        menu.SetBees(0);
        menu.SetHoney(0);
        //Создаём спавнер
        _stateSpawningBees = gameObject.AddComponent<SpawningBees>();
        _stateSpawningBees.Init(parameters);
        //Создаём собирателя мёда
        _stateCollectGiveHoneyHive = gameObject.AddComponent<CollectGiveHoneyHive>();
        _stateCollectGiveHoneyHive.Init(this, parameters, _stateSpawningBees, menu);
        //Умирание пчёл за еденицу времени
        _stateDeathBees = gameObject.AddComponent<DeathBees>();
        _stateDeathBees.Init(this, parameters.deathBees, menu);
    }

    public void CollectBeesInHive()
    {
        //Отключаем проверку заполенности резервуара пчёл и их выход из улья
        _stateCollectGiveHoneyHive.OnEnter(true);

        foreach (GameObject insectGmObj in _stateSpawningBees.CreateObjects)
        {
            HiveDweller insect = insectGmObj.GetComponent<HiveDweller>();

            insect.GoTo();
        }
    }

    public void SendForСollection()
    {
        _stateCollectGiveHoneyHive.OnEnter(false);

        foreach (GameObject insectGmObj in _stateSpawningBees.CreateObjects)
        {
            insectGmObj.SetActive(true);

            HiveDweller insect = insectGmObj.GetComponent<HiveDweller>();
            insect.WorkingGoTo();
        }
    }

    public void OnClick()
    {
        hiveMenu.SetActive(true);
    }
}
