using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.DataPersistence;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    //private string[] scenePaths = {"Assets/Scenes/Level1.unity"};
    public List<GameObject> MainMenuCanvas = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.PlayMenuAudioBackGround();


    }

    public void NavigateTo(int targetCanvas)
    {
        foreach (GameObject canvas in MainMenuCanvas)
        {
            canvas.SetActive(false);
        }
        MainMenuCanvas[targetCanvas].SetActive(true);

        if (MainMenuCanvas[targetCanvas].name == "AudioMenuCanvas")
        {
            AudioManager.Instance.GetComponentAudioVolume();
        }
        else if (MainMenuCanvas[targetCanvas].name == "ControlMenuCanvas")
        {
            Slider Sensitivity = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
            if (Sensitivity != null)
                Sensitivity.value = GameManager.Instance.Sensitivity;
        }
    }

    public void NewGame()
    {
        GameManager.Instance.NewGame();
    }

    public void ContinueGame()
    {
        GameManager.Instance.LoadLevel(DataPersistenceManager.Instance.gameData.Level);
    }

    public void LoadLevel(int i)
    {
        GameManager.Instance.LoadLevel(i);
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void SetSensitivity(float sensitivity)
    {
        GameManager.Instance.Sensitivity = sensitivity;
    }
}
