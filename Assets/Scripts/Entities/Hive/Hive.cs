using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hive : MonoBehaviour, IOnClick
{
    [SerializeField]
    public HiveParameters parameters;
    [SerializeField]
    GameObject hiveMenu;
    HiveMenu menu;

    public SpawningBees _stateSpawningBees;
    public CollectGiveHoneyHive _stateCollectGiveHoneyHive;
    public DeathBees _stateDeathBees;
    public CountingHiveDweller _countingHiveDweller;

    float currentHoneyStocks = 0;

    protected IEnumerator createObject;
    protected IEnumerator beeInHives;

    public static List<Hive> allHives = new List<Hive>();

    public float СurrentHoneyStocks { get => currentHoneyStocks; set => currentHoneyStocks = value; }

    private void Awake()
    {
        allHives.Add(this);
        //Создаём спавнер
        _stateSpawningBees = gameObject.AddComponent<SpawningBees>();
        //Создаём собирателя мёда
        _stateCollectGiveHoneyHive = gameObject.AddComponent<CollectGiveHoneyHive>();
        //Умирание пчёл за еденицу времени
        _stateDeathBees = gameObject.AddComponent<DeathBees>();
    }

    void Start()
    {
        //Подключаем меню улья
        menu = hiveMenu.GetComponent<HiveMenu>();
        menu.MaxValue(parameters.maxHoney, parameters.maxNumberObject);
        menu.SetBees(0);
        menu.SetHoney(0);

        _stateSpawningBees.Init(parameters);
        _stateCollectGiveHoneyHive.Init(this, parameters, _stateSpawningBees, menu);
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

        foreach (GameObject hiveDwellerGmObj in _stateSpawningBees.CreateObjects)
        {
            hiveDwellerGmObj.SetActive(true);

            HiveDweller hiveDweller = hiveDwellerGmObj.GetComponent<HiveDweller>();
            hiveDweller.WorkingGoTo();
        }
    }

    public void OnClick()
    {
        hiveMenu.SetActive(true);
    }

    public static int CountingAllBees()
    {
        int bees = 0;
        foreach (Hive hive in allHives)
            bees += hive._stateSpawningBees.CreateObjects.Count(bee => bee.GetComponent<Bee>());

        return bees;
    }

    public static int CountingAllDrones()
    {
        int bees = 0;
        foreach (Hive hive in allHives)
            bees += hive._stateSpawningBees.CreateObjects.Count(bee => bee.GetComponent<Drone>());

        return bees;
    }

    public int CountingBees()
    {
        return _stateSpawningBees.CreateObjects.Count(bee => bee.GetComponent<Bee>());
    }

    public int CountingDrones()
    {
        return _stateSpawningBees.CreateObjects.Count(bee => bee.GetComponent<Drone>());
    }
}
