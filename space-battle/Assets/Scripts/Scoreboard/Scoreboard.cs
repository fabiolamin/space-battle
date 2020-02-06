using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Text playerScorePrefab;

    private void OnEnable()
    {
        SetPlayersScore();
    }

    private void SetPlayersScore()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            string nickName = player.GetComponent<PhotonView>().Owner.NickName;
            int score = player.GetComponent<PlayerScore>().Score;
            Text playerScore = Instantiate(playerScorePrefab).GetComponent<Text>();
            playerScore.transform.SetParent(transform);
            playerScore.text = nickName + " " + score.ToString();
        }
    }
}
