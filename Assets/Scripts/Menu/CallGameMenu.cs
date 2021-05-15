using UnityEngine;

public class CallGameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject GameMenu;
    bool ofOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ofOn = !ofOn;
            GameMenu.gameObject.SetActive(ofOn);
            if (ofOn)
            {
                Time.timeScale = 0;
            } else 
            {
                Time.timeScale = 1;
            }
        }
    }
}
