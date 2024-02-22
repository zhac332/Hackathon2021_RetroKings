using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private string HowToPlay_String = "";
    [SerializeField] private string Logo_String = "";

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void LocalButton_OnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void RulesButton_OnClick()
    {
        Application.OpenURL(HowToPlay_String);
    }

    public void LogoButton_OnClick()
    {
        Application.OpenURL(Logo_String);
    }
}
