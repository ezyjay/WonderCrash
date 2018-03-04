﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public GameObject end_text;
    public GameObject player;
    int notepadToGet;
    public GameObject finalDoor;
    public GameObject panelInfo;
    public GameObject cellHeroDoor;
    // Use this for initialization


    private void Awake()
    {
        notepadToGet = 3;
    }

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SpottedEnd()
    {
        panelInfo.SetActive(true);
        panelInfo.GetComponentInChildren<Text>().text = "Game Over : Vous vous êtes fait repérer. Redirection vers le menu.";
        //end_text.SetActive(true);
        Pause();
        StartCoroutine(waitBeforeGoToMenu());
    }


    public void GoToNextLevel(int level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    IEnumerator waitBeforeGoToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void Pause()
    {
        //Time.timeScale = 0;
        player.GetComponent<PlayerController>().talking = true;
        player.GetComponent<CameraFPS>().talking = true;
        DialogueManager.getChildGameObject(player, "Main Camera").GetComponent<CameraFPS>().talking = true;
    }

    public void removeNotePad()
    {
        notepadToGet -= 1;
        if(notepadToGet == 2)
        {
            cellHeroDoor = GameObject.Find("Door_hero");
            cellHeroDoor.GetComponent<DoorPivoter>().Unlock();
        }
        if(notepadToGet == 0)
        {
            //Debug.LogError("Je passe ce tuto de merde");
            panelInfo.SetActive(true);
            panelInfo.GetComponentInChildren<Text>().text = "Dirigez vous vers la porte d'escalier !";
            finalDoor.AddComponent<TutoFinished>();
        }
    }
}
