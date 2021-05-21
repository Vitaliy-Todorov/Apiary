using System.Collections;
using UnityEngine;

public class Hive : MonoBehaviour, IOnClick
{
    [SerializeField]
    HiveParameters parameters;
    [SerializeField]
    GameObject hiveMenu;
    HiveMenu menu;

    SpawningBees _stateSpawningBees;
    CollectGiveHoneyHive _stateCollectGiveHoneyHive;

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
    }

    public void CollectBeesInHive()
    {
        foreach (GameObject beeGmObj in _stateSpawningBees.CreateObjects)
        {
            Bee bee = beeGmObj.GetComponent<Bee>();
            bee._stateHoneyGetter.OnExit();
            bee._stateMovement.OnEnter<GoTo>();
        }
    }

    public void SendForСollection()
    {
        foreach (GameObject beeGmObj in _stateSpawningBees.CreateObjects)
        {
            beeGmObj.SetActive(true);

            Bee bee = beeGmObj.GetComponent<Bee>();
            bee._stateHoneyGetter.OnEnter();
            bee._stateMovement.OnEnter<HoneyGoTo>();
        }
    }

    public void OnClick()
    {
        hiveMenu.SetActive(true);
    }
}
