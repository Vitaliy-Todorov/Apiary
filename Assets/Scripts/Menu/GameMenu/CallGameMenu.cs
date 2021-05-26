using UnityEngine;

public class CallGameMenu : MonoBehaviour
{
    [SerializeField]
    GameMenuManager GameMenu;
    bool ofOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ofOn = !ofOn;
            if (ofOn)
            {
                GameMenu.OnEnter();
                Time.timeScale = 0;
            } else
            {
                GameMenu.OnExit();
                Time.timeScale = 1;
            }
        }
    }
}
