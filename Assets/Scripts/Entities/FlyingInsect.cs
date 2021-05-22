using UnityEngine;

public class FlyingInsect : MonoBehaviour
{
    [SerializeField]
    public FlyingInsectParameters parameters;
    //Нигде не использую, но мало ли
    protected string _id;

    //Куда возвращаться по умолчанию 
    public GameObject _spawningThisBee;

    public MovementBee _stateMovement;

    public string Id { get => _id; }

    public void Init(GameObject spawningThisBee, string id)
    {
        _spawningThisBee = spawningThisBee;
        _id = id;
    }

    protected void Start()
    {
        _stateMovement = gameObject.AddComponent<MovementBee>();
        _stateMovement.Init();
        //Задаём точку возврата
        _stateMovement.OnEnter<GoTo>(_spawningThisBee.transform.position);
        _stateMovement.OnExit();
    }
}
