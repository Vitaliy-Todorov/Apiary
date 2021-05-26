using UnityEngine;

public class GameMenu : IState
{
    GameObject _gameMenu;

    public GameMenu(GameObject gameMenu)
    {
        _gameMenu = gameMenu;
    }

    public void OnEnter()
    {
        _gameMenu.SetActive(true);
    }

    public void OnExit()
    {
        _gameMenu.SetActive(false);
    }
}
