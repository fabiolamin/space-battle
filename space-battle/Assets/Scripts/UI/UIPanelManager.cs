using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, startPanel, lobbyPanel, roomPanel, scoreboardPanel, tutorialPanel, creditsPanel;
    [SerializeField] private Text connectionStatus;
    public Text ConnectionStatus 
    {
        get{ return connectionStatus; }
        set{ connectionStatus = value; }
    }

    private void Awake()
    {
        DisableAllPanels();
        EnableMainMenuPanel();
    }

    public void DisableAllPanels()
    {
        mainMenuPanel.SetActive(false);
        startPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(false);
        scoreboardPanel.SetActive(false);
    }
    public void EnableMainMenuPanel()
    {
        tutorialPanel.SetActive(false);
        creditsPanel.SetActive(false);
        scoreboardPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void EnableStartPanel()
    {
        mainMenuPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void EnableLobbyPanel()
    {
        startPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void EnableRoomPanel()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
    }

    public void EnableScoreboardPanel()
    {
        roomPanel.SetActive(false);
        scoreboardPanel.SetActive(true);
    }

    public void EnableTutorialPanel()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void EnableCreditsPanel()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
