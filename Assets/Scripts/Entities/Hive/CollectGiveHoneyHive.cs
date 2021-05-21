using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGiveHoneyHive : MonoBehaviour
{
    Hive _hive;
    HiveParameters _parameters;
    HiveMenu _menu;

    SpawningBees _stateSpawningBees;

    protected IEnumerator beeInHives;

    public void Init(Hive hive, HiveParameters parameters,SpawningBees stateSpawningBees, HiveMenu menu)
    {
        _hive = hive;
        _parameters = parameters;
        _stateSpawningBees = stateSpawningBees;
        _menu = menu;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bee bee = collision.gameObject.GetComponent<Bee>();

        //Объект относится к пчёлам? Эта пчела из этого улья? Её хранилище мёда заполнено?
        if (bee != null && _stateSpawningBees.CreateObjects.Contains(collision.gameObject) && bee.FilledHoneyStocks())
        {
            beeInHives = BeeInHives(collision.gameObject, bee);
            StartCoroutine(beeInHives);
        }

        _menu.SetBees(_stateSpawningBees.CreateObjects.Count);
    }

    IEnumerator BeeInHives(GameObject beeGmObj, Bee bee)
    {
        if (_hive.СurrentHoneyStocks < _parameters.maxHoney)
            _hive.СurrentHoneyStocks += bee.GettHoney();
        _menu.SetHoney(_hive.СurrentHoneyStocks);

        beeGmObj.SetActive(false);

        yield return new WaitForSeconds(_parameters.timeBeesGiveHoney);

        if (_hive.СurrentHoneyStocks < _parameters.maxHoney)
            beeGmObj.SetActive(true);
    }
}
