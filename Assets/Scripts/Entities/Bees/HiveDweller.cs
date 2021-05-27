using System;
using UnityEngine;

public abstract class HiveDweller : MonoBehaviour, IGeneratedObject
{
    [SerializeField]
    public GoToParameters parameters;
    GameObject weMove;
    //Нигде не использую, но мало ли
    protected string _id; 

    [NonSerialized]
    //Куда возвращаться по умолчанию 
    public GameObject _spawningThisBee;
    [NonSerialized]
    public MovementInsect _stateMovement;

    public GameObject WeMove { get => weMove; }
    public string Id { get => _id; }

    public void Init(GameObject spawningThisBee, string id)
    {
        weMove = spawningThisBee;
        _spawningThisBee = spawningThisBee;
        _id = id;
    }

    public void Start()
    {
        _stateMovement = gameObject.AddComponent<MovementInsect>();
        _stateMovement.Init(weMove, (GoToParameters)parameters);
    }

    //Основное рабочее состояние
    public abstract void WorkingGoTo();

    //Возвращение в некую ключевую точку
    public abstract void GoTo();
}
