using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button playButton;

    [SerializeField]
    Button settingsButton;

    [SerializeField]
    GameObject settingsMenuObj;

    [SerializeField]
    Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayPressed);

        settingsButton.onClick.AddListener(delegate {
            SettingsPressed(settingsMenuObj);
            });

        exitButton.onClick.AddListener(ExitPressed);
    }

    void PlayPressed()
    {
        SceneManager.LoadScene(1);
    }

    void SettingsPressed(GameObject obj)
    {
        gameObject.SetActive(false);
        obj.SetActive(true);
    }

    void ExitPressed()
    {
        Application.Quit();
    }
}
