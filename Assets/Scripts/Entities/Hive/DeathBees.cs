using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBees : MonoBehaviour, IState
{
    DeathBeesParameters _parameters;

    HiveMenu _menu;
    List<GameObject> createObjects = new List<GameObject>();

    protected IEnumerator destroyBees;

    public void Init(Hive hive, DeathBeesParameters parameters, HiveMenu menu)
    {
        _parameters = parameters;
        createObjects = hive._stateSpawningBees.CreateObjects;
        _menu = menu;
    }

    public void OnEnter()
    {
        enabled = true;
    }

    public void OnExit()
    {
        enabled = false;
    }

    void Start()
    {
        destroyBees = DestroyBees();
        StartCoroutine(destroyBees);
    }

    IEnumerator DestroyBees()
    {
        while (true)
        {
            //Если нет созданных объектов
            if (createObjects.Count == 0)
                yield return new WaitForSeconds(_parameters.time);

            int destroyedObjects = 0;
            //проверяем наличие трутней, если находим, уничтожаем
            for (int i = 0; i < createObjects.Count; i++)
                if (createObjects[i].GetComponent<Drone>() != null)
                {
                    Destroy(createObjects[i]);
                    createObjects.RemoveAt(i);
                    i--;
                    destroyedObjects++;
                    if (destroyedObjects >= _parameters.numberObjectsDestroyed)
                        break;
                }

            //Если нужно уничтожить больше пчёл, чем есть трутней
            if (destroyedObjects < _parameters.numberObjectsDestroyed)
                for (int i = 0; (i < createObjects.Count) && (_parameters.numberObjectsDestroyed - destroyedObjects > 0) ; i++)
                {
                    Destroy(createObjects[i]);
                    createObjects.RemoveAt(i);
                    i--;
                    destroyedObjects++;
                }

            _menu.SetBees(createObjects.Count);

            yield return new WaitForSeconds(_parameters.time);
        }
    }
}
