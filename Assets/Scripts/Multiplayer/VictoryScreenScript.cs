using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class VictoryScreenScript : MonoBehaviour
{
    public void OnLobbyButtonClicked()
    {
        /*PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;*/

        // PhotonNetwork.LoadLevel("DemoAsteroids-GameScene");
        // PhotonNetwork.LoadLevel("LobbyScene");
        // PhotonNetwork.LeaveRoom();
        Application.Quit();
    }
}
