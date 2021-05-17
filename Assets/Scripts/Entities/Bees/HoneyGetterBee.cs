using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyGetterBee : IHoneyGetter
{
    public BeesParameters _parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;

    GameObject _beeGmOdj;
    Bee _bee;
    IState _stateMovement;

    private IEnumerator coroutine;

    public HoneyGetterBee(GameObject beeGmOdj, IState stateMovement)
    {
        _beeGmOdj = beeGmOdj;
        _bee = beeGmOdj.GetComponent<Bee>();
        _parameters = _bee.parameters;
        _stateMovement = stateMovement;
    }

    /// <summary>
    /// Даёт мёд и переключает состояние на движение когда мёд заканчивается
    /// </summary>
    public void OnEnter(object honeyGiver)
    {
        coroutine = HoneyGet((IHoneyGiver)honeyGiver, _parameters.getHoneyTime);
        _bee.StartCoroutine(coroutine);
    }

    public void OnExit() 
    {
        _bee.StopCoroutine(coroutine);
    }

    public IEnumerator HoneyGet(IHoneyGiver honeyGiver, float waitTime)
    {
        while (true)
        {
            //Проверяем может ли пчела ещё взять мёд и наличие объекта у которого мы хотим взять мёд, если нет свободных мест возникает ошибка
            if (сurrentHoneyStocks < _parameters.maxHoneyStocks && !honeyGiver.Equals(null))
                try
                {
                    сurrentHoneyStocks +=
                        honeyGiver.HoneyGive(_beeGmOdj, _parameters.getHoney);
                }
                catch
                {
                    _stateMovement.OnEnter<HoneyGoTo>();
                    yield break;
                }

            //Проверяем существует ли дающий мёд, если нет, дальше ищем мёд
            if (honeyGiver.IsDestroyed())
            {
                _stateMovement.OnEnter<HoneyGoTo>();
                yield break;
            }
            //Проверяем заполненность хранилища мёда пчелы, если оно заполнено летим в улей Hive
            else if (сurrentHoneyStocks >= _parameters.maxHoneyStocks)
            {
                _stateMovement.OnEnter<GoTo>();
                yield break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
