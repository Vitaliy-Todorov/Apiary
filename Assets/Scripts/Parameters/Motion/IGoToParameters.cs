using UnityEngine;

//Изначально приписывал это для того, чтобы можно было встраивать положение порождающего объекта в параметры других типов,
//но так как у разных пчёл разные ульи и надо было создавать экземпляр ScriptableObject для каждого улья, я решил так не делать.
//Теперь этот интерфейс используется только для инициализации определённого типа движения с помощью перегрузки
/// <summary>
/// Интерфейс используется инициализации определённого типа движения с помощью перегрузки Init(IGoToParameters parameters)
/// </summary>
public interface IGoToParameters: IHiveDwellerParameters
{
    void SetWeMove(GameObject weMove);
    GameObject GetWeMove();
}
