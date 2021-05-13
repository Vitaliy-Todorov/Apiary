using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBee : Motion, IHoneyGetter
{
    [SerializeField]
    BeesParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;

    List<GameObject> HoneyGivers;
    GameObject _hiveThisBee;
    MovementStateBee movementState;

    private IEnumerator coroutine;

    private void Start()
    {
        HoneyGivers = new List<GameObject>();

        movementState = gameObject.AddComponent<MovementStateBee>();
        movementState.OnEnter(parameters.speed, "Honey");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Если объект может давать мёд, то берём его
        IHoneyGiver honeyGiver = collision.gameObject.GetComponent<IHoneyGiver>();

        if (honeyGiver is IHoneyGiver)
        {
            coroutine = HoneyGet(honeyGiver, parameters.getHoneyTime);
            StartCoroutine(coroutine);
            movementState.OnExit();
        }
    }

    /*
        public void X(GameObject y)
        {
            IHoneyGiver honeyGiver = y.GetComponent<IHoneyGiver>();
            coroutine = HoneyGet(honeyGiver, parameters.getHoneyTime);
            StartCoroutine(coroutine);
            movementState.OnExit();
        }*/

    public IEnumerator HoneyGet(IHoneyGiver honeyGiver, float waitTime)
    {
        while (true)
        {
            //Проверяем может ли пчела ещё взять мёд и наличие объекта у которого мы хотим взять мёд
            if (сurrentHoneyStocks < parameters.maxHoneyStocks && !honeyGiver.Equals(null))
                try
                {
                    сurrentHoneyStocks +=
                        honeyGiver.HoneyGive(gameObject, parameters.getHoney);
                }
                catch
                {
                    movementState.OnEnter("Honey");
                }

            //Проверяем существует ли дающий мёд и проверяем наличие свободных мест, если нет, дальше ищем мёд
            if (honeyGiver.Equals(null))
            {
                movementState.OnEnter("Honey");
                yield break;
            }
            //Проверяем заполненность хранилища мёда пчелы, если оно заполнено летим в улей Hive
            else if (сurrentHoneyStocks >= parameters.maxHoneyStocks)
            {
                movementState.OnEnter("Hive");
                yield break;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
