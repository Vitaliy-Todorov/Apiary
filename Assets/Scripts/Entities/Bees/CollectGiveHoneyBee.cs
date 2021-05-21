using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Даёт мёд и переключает состояние на движение когда мёд заканчивается
/// </summary>
public class CollectGiveHoneyBee : MonoBehaviour, HoneyConsumer, IState
{
    public BeesParameters _parameters;

    Bee _bee;
    GameObject _beeGmObj;

    private IEnumerator coroutine;
    public void Init(Bee bee)
    {
        _bee = bee;
    }

    private void Start()
    {
        //_bee = _beeGmObj.GetComponent<Bee>();
        _parameters = _bee.parameters;
    }

    public void OnEnter()
    {
        enabled = true;
    }

    public void OnExit() 
    {
        enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Если объект может давать мёд, то берём его
        IHoneyGiver _honeyGiver = collision.gameObject.GetComponent<IHoneyGiver>();

        if (_honeyGiver is IHoneyGiver && _bee.СurrentHoneyStocks < _parameters.maxHoneyStocks)
        {
            _bee._stateMovement.OnExit();
            coroutine = ConsumeHoney(_honeyGiver, _parameters.getHoneyTime);
            _bee.StartCoroutine(coroutine);
        }
        else if (_bee.СurrentHoneyStocks >= _parameters.maxHoneyStocks)
            _bee._stateMovement.OnEnter<GoTo>();
    }

    public IEnumerator ConsumeHoney(IHoneyGiver honeyGiver, float waitTime)
    {
        while (true)
        {
            float getHoney = _parameters.maxHoneyStocks - _bee.СurrentHoneyStocks;
            //Проверяем может ли пчела ещё взять мёд и наличие объекта у которого мы хотим взять мёд, если нет свободных мест возникает ошибка
            try
            {
                //Если нужно взять мёда меньше чем пчела берёт за один раз 
                if (getHoney >= _parameters.getHoney && !honeyGiver.Equals(null))
                    _bee.СurrentHoneyStocks += honeyGiver.HoneyGive(gameObject, _parameters.getHoney);
                else
                    _bee.СurrentHoneyStocks += honeyGiver.HoneyGive(gameObject, getHoney);
            }
            catch
            {
                _bee._stateMovement.OnEnter<HoneyGoTo>();
                yield break;
            }

            //Проверяем заполненность хранилища мёда пчелы, если оно заполнено летим в улей Hive
            if (_bee.СurrentHoneyStocks >= _parameters.maxHoneyStocks)
            {
                _bee._stateMovement.OnEnter<GoTo>();
                yield break;
            }
            //Проверяем существует ли дающий мёд, если нет, дальше ищем мёд
            else if (honeyGiver.IsDestroyed())
            {
                _bee._stateMovement.OnEnter<HoneyGoTo>();
                yield break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
