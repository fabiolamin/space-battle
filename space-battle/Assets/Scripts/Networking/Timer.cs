using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class Timer : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable customPropertie;
    private bool hasStarted = false;
    private float time;
    [SerializeField] float initialTime = 180f;
    [SerializeField] Text timerText;

    private void Update()
    {
        if (hasStarted)
        {
            UpdateTimer();
        }
    }

    public void SetTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            time = initialTime;
            customPropertie = new ExitGames.Client.Photon.Hashtable()
            {
                {"Timer",time}
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(customPropertie);
        }

        hasStarted = true;
    }

    public void UpdateTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            time -= Time.deltaTime;
            ExitGames.Client.Photon.Hashtable auxiliaryCustomPropertie = PhotonNetwork.CurrentRoom.CustomProperties;
            auxiliaryCustomPropertie.Remove("Timer");
            auxiliaryCustomPropertie.Add("Timer", time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(auxiliaryCustomPropertie);
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        time = (float)propertiesThatChanged["Timer"];
        SetText();
    }

    private void SetText()
    {
        if (time > 0)
        {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;

            timerText.text = minutes + ":" + seconds.ToString("D2");
        }
    }
}
