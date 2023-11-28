using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VictoryManagerScript : MonoBehaviour
{
    int playerCount = 0;

    public void PlayerEntered()
    {
        playerCount++;
        Debug.Log("Player entered!");
        CheckPlayers();
    }

    void CheckPlayers()
    {
        Debug.Log("Player count" + playerCount);
        if (playerCount == 2) {
            Debug.Log("Victory!");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("VictoryScene");
            }
            else
            {
                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("LoadVictoryScene", RpcTarget.All);
                
            }
            PhotonNetwork.LeaveRoom();
        }
        else Debug.Log("Just a player entered");
    }

    [PunRPC]
    void LoadVictoryScene()
    {
        PhotonNetwork.LoadLevel("VictoryScene");
    }
}
