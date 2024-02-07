using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private string HowToPlay_String = "";

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
}
