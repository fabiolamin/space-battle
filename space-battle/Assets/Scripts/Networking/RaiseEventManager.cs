using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RaiseEventManager : MonoBehaviour
{
    public void Create(byte raiseEventCode, object content)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
        {
            Receivers = ReceiverGroup.All
        };
        SendOptions sendOptions = new SendOptions { Reliability = true };

        PhotonNetwork.RaiseEvent(raiseEventCode, content, raiseEventOptions, sendOptions);
    }
}
