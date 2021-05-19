using System.Collections;
using UnityEngine;

public class Hive : SpawningDifferentObjects
{
    SpawningDifferentObjects spawningDifferentObjects;
    [SerializeField]
    HiveParameters parameters1;
    [SerializeField]
    HiveMenu hiveMenu;

    float сurrentHoneyStocks = 0;
    int сurrentBees = 0;

    protected IEnumerator createObject;
    protected IEnumerator beeInHives;

    void Start()
    {
        //Создаём спавнер
        Init(parameters1, gameObject);
        createObject = CreateObject();
        StartCoroutine(createObject);
        //Подключаем меню улья
        hiveMenu.MaxValue(parameters1.maxHoney, parameters1.maxNumberObject);
        hiveMenu.SetBees(0);
        hiveMenu.SetHoney(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bee bee = collision.gameObject.GetComponent<Bee>();
        //Если это не пчела, не взаимодействуем
        if (!bee)
            return;

        if (createObjects.Contains(collision.gameObject) && bee.FilledHoneyStocks())
        {
            beeInHives = BeeInHives(collision.gameObject, bee);
            StartCoroutine(beeInHives);
        }

        сurrentBees += 1;

        hiveMenu.SetHoney(сurrentHoneyStocks);
        hiveMenu.SetBees(сurrentBees);
    }

    IEnumerator BeeInHives(GameObject beeGmObj, Bee bee)
    {
        сurrentHoneyStocks += bee.GettHoney();

        beeGmObj.SetActive(false);

        yield return new WaitForSeconds(3);


        beeGmObj.SetActive(true);
    }
}
