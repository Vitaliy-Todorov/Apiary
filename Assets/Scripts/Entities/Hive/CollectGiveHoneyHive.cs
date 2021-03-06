using System.Collections;
using UnityEngine;

/// <summary>
/// Что бы пчёлы возвращались в улий делаем OnEnter(true)
/// </summary>
public class CollectGiveHoneyHive : MonoBehaviour, IState
{
    Hive _hive;
    HiveParameters _parameters;
    HiveMenu _menu;
    //Если false то пчёлы заходят в улей, только с полным резервуаром меда
    bool _collectBees;

    SpawningBees _stateSpawningBees;

    protected IEnumerator beeInHives;

    public void Init(Hive hive, HiveParameters parameters,SpawningBees stateSpawningBees, HiveMenu menu)
    {
        _hive = hive;
        _parameters = parameters;
        _stateSpawningBees = stateSpawningBees;
        _menu = menu;
    }

    public void OnEnter()
    {
        enabled = true;
    }

    //Если пчелы возвращаются в улей на постоянно то true. Если _collectBees = false, то проверяем заполненность мёдного резервуара пчёл
    public void OnEnter(bool collectBees)
    {
        _collectBees = collectBees;
        enabled = true;
    }

    public void OnExit()
    {
        enabled = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        Bee bee = collision.gameObject.GetComponent<Bee>();

        //Нужно проверять заполненность мёдного резервуара пчёл? Если _collectBees = false, то проверяем
        bool filledHoneyStocks = true;
        if (bee != null && !_collectBees)
            filledHoneyStocks = bee.FilledHoneyStocks();

        //Объект относится к пчёлам? Эта пчела из этого улья? Её хранилище мёда заполнено?
        if (bee != null && _stateSpawningBees.CreateObjects.Contains(collision.gameObject) && filledHoneyStocks)
        {
            beeInHives = BeeInHives(collision.gameObject, bee);
            StartCoroutine(beeInHives);
        }


        HiveDweller hiveDweller = collision.gameObject.GetComponent<HiveDweller>();
        //Объект относится к пчёлам? Эта пчела из этого улья? Объявлен сбор??
        if (hiveDweller != null && _stateSpawningBees.CreateObjects.Contains(collision.gameObject) && _collectBees)
            collision.gameObject.SetActive(false);

        _menu.SetBees(_stateSpawningBees.CreateObjects.Count);
    }

    IEnumerator BeeInHives(GameObject beeGmObj, Bee bee)
    {/*
        if (_hive.СurrentHoneyStocks < _parameters.maxHoney)
            _hive.СurrentHoneyStocks += bee.GettHoney();*/

        if (bee.СurrentHoneyStocks < _parameters.maxHoney - _hive.СurrentHoneyStocks)
            _hive.СurrentHoneyStocks += bee.GettHoney();
        else
            _hive.СurrentHoneyStocks = _parameters.maxHoney;
        _menu.SetHoney(_hive.СurrentHoneyStocks);

        beeGmObj.SetActive(false);

        //Если пчелы возвращаются в улей на постоянно
        if (!_collectBees)
        {
            yield return new WaitForSeconds(_parameters.timeBeesGiveHoney);

            if (beeGmObj != null && _hive.СurrentHoneyStocks < _parameters.maxHoney)
                beeGmObj.SetActive(true);
        }
    }
}
