using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPoints;
    public GameObject standbyCamera;
    public GameObject playerCamera;

    private GameObject spawnedPlayerPrefab;
    List<string> chatMessages;
    int maxChatMessages = 5;
    private string[] playerNames = { "Rick", "Morty", "Beth", "Summer" };
    private string playerName;

    public float respawnTimer = 0;

    private void Start()
    {
        int namePicker = Random.Range(0, playerNames.Length);
        playerName = playerNames[namePicker];
        chatMessages = new List<string>();
    }

    public override void OnJoinedRoom()
    {
        SpawnPlayer();
        base.OnJoinedRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

    public void SpawnPlayer()
    {
        int spawnPicker = Random.Range(0, spawnPoints.Length);
        XRRig rig = FindObjectOfType<XRRig>();
        rig.transform.position = spawnPoints[spawnPicker].transform.position;
        rig.transform.rotation = spawnPoints[spawnPicker].transform.rotation;

        spawnedPlayerPrefab = PhotonNetwork.Instantiate("NetworkPlayer", spawnPoints[spawnPicker].transform.position, spawnPoints[spawnPicker].transform.rotation, 0);

        standbyCamera.gameObject.SetActive(false);
        Debug.Log(playerCamera);
        Debug.Log(playerCamera.gameObject);

        playerCamera.gameObject.SetActive(true);
    }

    public void MoveToStandby()
    {
        standbyCamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer <= 0)
            {
                // Time to respawn the player!
                SpawnPlayer();
            }
        }
    }
}
