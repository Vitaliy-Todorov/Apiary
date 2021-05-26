using System;
using UnityEngine;

public abstract class HiveDweller : MonoBehaviour
{
    [SerializeField]
    public GoToParameters parameters;
    //Нигде не использую, но мало ли
    protected string _id;

    [NonSerialized]
    //Куда возвращаться по умолчанию 
    public GameObject _spawningThisBee;
    [NonSerialized]
    public MovementInsect _stateMovement;

    public string Id { get => _id; }

    public void Init(GameObject spawningThisBee, string id)
    {
        parameters.SetWeMove(spawningThisBee);
        _spawningThisBee = spawningThisBee;
        _id = id;
    }

    //Основное рабочее состояние
    public abstract void WorkingGoTo();

    //Возвращение в некую ключевую точку
    public abstract void GoTo();
}
