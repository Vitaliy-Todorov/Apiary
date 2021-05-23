using System;
using UnityEngine;

public abstract class FlyingInsect : MonoBehaviour
{
    [SerializeField]
    public FlyingInsectParameters parameters;
    //Нигде не использую, но мало ли
    protected string _id;

    [NonSerialized]
    //Куда возвращаться по умолчанию 
    public GameObject _spawningThisBee;
    [NonSerialized]
    public MovementBees _stateMovement;

    public string Id { get => _id; }

    public void Init(GameObject spawningThisBee, string id)
    {
        _spawningThisBee = spawningThisBee;
        _id = id;
    }

    protected void Awake()
    {
        _stateMovement = gameObject.AddComponent<MovementBees>();
        _stateMovement.Init(this);
    }

    protected void Start()
    {
        //Задаём точку возврата
        _stateMovement.OnEnter<GoTo>(_spawningThisBee.transform.position);
    }
}
