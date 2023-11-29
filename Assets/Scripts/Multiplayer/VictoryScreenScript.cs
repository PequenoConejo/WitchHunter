using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class VictoryScreenScript : MonoBehaviour
{
    public void OnLobbyButtonClicked()
    {

        // PhotonNetwork.LoadLevel("DemoAsteroids-GameScene");
        // PhotonNetwork.LoadLevel("LobbyScene");

        // Just leaving when demo ends
        Application.Quit();
    }
}
