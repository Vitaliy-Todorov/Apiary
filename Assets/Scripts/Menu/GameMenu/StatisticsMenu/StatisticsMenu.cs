using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenu : IState
{
    GameMenuManager _menuManager;

    GameObject _statisticsMenu;
    Text _allHoney;
    Text _allBess;
    Text _allDrone;

    List<GameObject> menuHiveStatisticsObjs = new List<GameObject>();

    public StatisticsMenu(GameMenuManager menuManager)
    {
        _menuManager = menuManager;
        _statisticsMenu = menuManager.statisticsMenu;
        _allHoney = menuManager.allHoney;
        _allBess = menuManager.allBess;
        _allDrone = menuManager.allDrone;
    }

    public void OnEnter()
    {
        _statisticsMenu.SetActive(true);
        SetValue();
    }

    public void OnExit()
    {
        _menuManager.DestroyMenuHiveStatistics(menuHiveStatisticsObjs);
        _statisticsMenu.SetActive(false);
    }

    /// <summary>
    /// Отображаем значения пасеки и добавляем информацию по каждому улию
    /// </summary>
    public void SetValue()
    {
        float allHoney = 0;
        float allHoneyMax = 0;
        int allBees = 0;
        int allDrone = 0;
        int allBeesMax = 0;

        foreach (Hive hive in Hive.allHives)
        {
            //Суммируем информацию по всем ульям
            allHoney += hive.СurrentHoneyStocks;
            allHoneyMax += hive.parameters.maxHoney;
            allBees += hive.CountingBees();
            allDrone += hive.CountingDrones();
            allBeesMax += hive.parameters.maxNumberObject;

            //Добавляем информацию о конкретном ульи
            GameObject menuHiveStatisticsObj = _menuManager.InstantiateMenuHiveStatistics();
            MenuHiveStatistics menuHiveStatistics = menuHiveStatisticsObj.GetComponent<MenuHiveStatistics>();
            menuHiveStatistics.Init(hive);
            //Составляем список, что бы потом удалить эти объекты
            menuHiveStatisticsObjs.Add(menuHiveStatisticsObj);
        }

        _allHoney.text = allHoney + "/" + allHoneyMax;
        _allBess.text = allBees + "/" + allBeesMax;
        _allDrone.text = allDrone + "/" + allBeesMax;
    }
}
