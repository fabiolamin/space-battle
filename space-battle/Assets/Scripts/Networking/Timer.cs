using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class Timer : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable customPropertie;
    private bool hasStarted = false;
    [SerializeField] float initialTime = 15f;
    [SerializeField] Text timerText;
    public float CurrentTime { get; set; }

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
            CurrentTime = initialTime;
            customPropertie = new ExitGames.Client.Photon.Hashtable()
            {
                {"Timer", CurrentTime}
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(customPropertie);
        }

        hasStarted = true;
    }

    public void UpdateTimer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CurrentTime -= Time.deltaTime;
            ExitGames.Client.Photon.Hashtable auxiliaryCustomPropertie = PhotonNetwork.CurrentRoom.CustomProperties;
            auxiliaryCustomPropertie.Remove("Timer");
            auxiliaryCustomPropertie.Add("Timer", CurrentTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(auxiliaryCustomPropertie);
        }
    }

    public void SetText()
    {
        if (CurrentTime > 0)
        {
            int minutes = (int)CurrentTime / 60;
            int seconds = (int)CurrentTime % 60;

            timerText.text = minutes + ":" + seconds.ToString("D2");
        }
    }
}
