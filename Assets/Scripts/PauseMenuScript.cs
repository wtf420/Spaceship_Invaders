using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class PauseMenuScript : MonoBehaviour
{
    //private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";
    public List<GameObject> PauseMenuCanvas= new List<GameObject>();

    private bool isVisible = false;

    private Player player;

    bool State = false; // false: UnActive, true: Active

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.sensitivity = GameManager.Instance.Sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject winning = PauseMenuCanvas.Find(e => e.name == "WinningScene");
            // player cannot pause while winning scene showing.
            if(winning == null ||(winning != null && winning.activeSelf == false)) 
            {
                switch (State)
                {
                    case false:
                        Show();
                        State = true;
                        Cursor.visible = true; // show cursor
                        player.GamePaused();
                        break;

                    case true:
                        Hide();
                        State = false;
                        Cursor.visible = false;
                        player.GameResume();
                        break;
                }
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            HUD.Instance.Coin++;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if(HUD.Instance.Life <= 0)
            {
                HUD.Instance.Life++;
                player.gameObject.SetActive(true);
            }
        }
    }

    public void NavigateTo(int targetCanvas)
    {
        foreach (GameObject canvas in PauseMenuCanvas)
        {
            canvas.SetActive(false);
        }
        PauseMenuCanvas[targetCanvas].SetActive(true);
        if(PauseMenuCanvas[targetCanvas].name == "WinningScene")
        {
            player.GamePaused();
        }
        else if(PauseMenuCanvas[targetCanvas].name == "AudioMenuCanvas")
        {
            AudioManager.Instance.GetComponentAudioVolume();
        }
        else if(PauseMenuCanvas[targetCanvas].name == "ControlMenuCanvas")
        {
            Slider Sensitivity = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
            Debug.Log(string.Format("sensitivity: {0}, {1}", GameManager.Instance.Sensitivity, Sensitivity.name));
            if(Sensitivity != null)
                Sensitivity.value = GameManager.Instance.Sensitivity;
        }
    }

    public int FindCanvasByName(string name)
    {
        int index = PauseMenuCanvas.FindIndex(e => e.name == name);

        return index;
    }

    public void Show()
    {
        PauseMenuCanvas[0].SetActive(true);
        isVisible = true;
    }

    public void Hide()
    {
        foreach (GameObject canvas in PauseMenuCanvas)
        {
            canvas.SetActive(false);
        }
        isVisible = false;
        //ResumeGame
        Cursor.visible = false; // invisible cursor

        player.GameResume();
    }

    public bool isShowing()
    {
        return isVisible;
    }

    public void ReturnToMainMenu()
    {
        player.GameResume();

        GameManager.Instance.MainMenu();
    }

    public void NextLevel()
    {
        player.GameResume();

        GameManager.Instance.NextLevel();
    }

    public void TryAgain()
    {
        player.GameResume();

        GameManager.Instance.Restart();
    }

    public void SetSensitivity(float sensitivity)
    {
        GameManager.Instance.Sensitivity = sensitivity;
        player.sensitivity = sensitivity;
    }

    public void SettingVolume(float volume)
    {
        AudioManager.Instance.SettingVolume(volume);
    }
}
