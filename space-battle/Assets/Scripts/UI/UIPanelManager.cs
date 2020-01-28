﻿using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, startPanel, lobbyPanel, roomPanel, winnerPanel;

    private void Awake()
    {
        DisableAllPanels();
        EnableMainMenuPanel();
    }

    private void DisableAllPanels()
    {
        mainMenuPanel.SetActive(false);
        startPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(false);
        winnerPanel.SetActive(false);
    }
    public void EnableMainMenuPanel()
    {
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

    public void EnableGameOverPanel()
    {
        roomPanel.SetActive(false);
        winnerPanel.SetActive(true);
    }
}
