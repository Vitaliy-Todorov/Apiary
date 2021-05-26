using UnityEngine;
using UnityEngine.UI;

public class MenuHiveStatistics : MonoBehaviour
{
    [SerializeField]
    Text _nameHive;
    [SerializeField]
    Text _honey;
    [SerializeField]
    Text _bess;
    [SerializeField]
    Text _drone;

    public void Init(Hive hive)
    {
        _nameHive.text = hive.gameObject.name;
        _honey.text = hive.СurrentHoneyStocks + "/" + hive.parameters.maxHoney;
        _bess.text = hive.CountingBees() + "/" + hive.parameters.maxNumberObject;
        _drone.text = hive.CountingDrones() + "/" + hive.parameters.maxNumberObject;
    }
}
