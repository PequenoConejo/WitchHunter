using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;

public class MenuManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.GetComponent<TMP_InputField>().text, roomOptions);
        //PhotonNetwork.CreateRoom(createInput.text, roomOptions);
        //Debug.Log(createInput.GetComponent<TMP_InputField>().text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.GetComponent<TMP_InputField>().text);
        //PhotonNetwork.JoinRoom(joinInput.text);
        // Debug.Log(joinInput.GetComponent<TMP_InputField>().text);

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("FirstLevel");
    }
}
