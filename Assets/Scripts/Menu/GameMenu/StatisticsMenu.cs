using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenu : IState
{
    Hive _hives;

    GameObject _statisticsMenu;
    Text _allHoney;
    Text _allBess;
    Text _allDrone;

    float _maxHoney;
    int _maxBees;
    int _maxDrone;

    public StatisticsMenu(GameMenuManager menuManager)
    {
        _statisticsMenu = menuManager.statisticsMenu;
        _allHoney = menuManager.allHoney;
        _allBess = menuManager.allBess;
        _allDrone = menuManager.allDrone;
        _hives = menuManager.hives;
        /*_maxHoney = _hives.AllHoney();
        _maxBees = ;
        _maxDrone = ;*/
    }

    public void OnEnter()
    {
        _statisticsMenu.SetActive(true);
    }

    public void OnExit()
    {
        _statisticsMenu.SetActive(false);
    }

    public void SetValue()
    {
        //SetBees(_hives.countingBees.CountingBee());
    }

    public void SetHoney(float health)
    {
        //honey.value = health;
    }

    public void SetBees(int bee)
    {
        _allBess.text = bee + "/" + "_maxBees";
    }
}
