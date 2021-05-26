using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/// <summary>
/// Для перемещения.  
/// Перед первым использованием способ движения, его нужно инциализировать через: Init(ВидДвиженияParameters)
/// </summary>
public class MovementInsect : MonoBehaviour, IState
{
    MovementState currentState;
    GoToHoney _honeyGoTo;
    GoTo _goTo;
    GoToRandom _goToRandom;
    IHiveDwellerParameters _parameters;

    bool updateInFixedUpdate;
    Vector3 _moveTo;

    IEnumerator moveToCoroutine;

    public void Init(BeesParameters parameters)
    {
        _parameters = parameters;
        _honeyGoTo = new GoToHoney(gameObject);
        updateInFixedUpdate = true;
        currentState = _honeyGoTo;
        StopCoroutine(moveToCoroutine);
    }

    public void Init(IGoToParameters parameters)
    {
        _parameters = parameters;
        _goTo = new GoTo(gameObject, parameters.GetWeMove());
        currentState = _goTo;
        moveToCoroutine = MoveToCoroutine();
        StartCoroutine(moveToCoroutine);
        updateInFixedUpdate = false;
    }

    public void Init(IGoToRandomParameters parameters)
    {
        _parameters = parameters;
        _goToRandom = new GoToRandom(gameObject, parameters);
        currentState = _goToRandom;
        moveToCoroutine = MoveToCoroutine();
        StartCoroutine(moveToCoroutine);
        updateInFixedUpdate = false;
    }

    public void OnEnter() => enabled = true;

    /// <summary>
    /// Идём к ближайшему свободному цветку
    /// </summary>
    public void OnEnterGoToHoney()
    {
        enabled = true;
        currentState = _honeyGoTo;
        updateInFixedUpdate = true;
        StopCoroutine(moveToCoroutine);
    }

    /// <summary>
    /// Идём к объету переданному в параметрах
    /// </summary>
    public void OnEnterGoTo()
    {
        enabled = true;
        currentState = _goTo;
        updateInFixedUpdate = false;
        updateInFixedUpdate = true;
        StopCoroutine(moveToCoroutine);
    }

    /// <summary>
    /// Хаотичное движение
    /// </summary>
    public void OnEnterGoToRandom()
    {
        enabled = true;
        currentState = _goToRandom;
        updateInFixedUpdate = false;
        moveToCoroutine = MoveToCoroutine();
        StartCoroutine(moveToCoroutine);
    }

    public void OnExit() => enabled = false;

    protected IEnumerator MoveToCoroutine()
    {
        while (true)
        {
            _moveTo = currentState.GoTu;
            yield return new WaitForSeconds(0.6f);
        }
    }

    private void FixedUpdate()
    {
        if(updateInFixedUpdate)
            _moveTo = currentState.GoTu;
        Motion.Move(transform, _moveTo, _parameters.GetSpeed()); 
    } 
}
