using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDoorScript : MonoBehaviour{

    public LastScenePlay LastScenePlay;
    public GameObject LeftDoorSign;


    void Start(){
      LeftDoorSign.SetActive(false);
    }

    void Update(){
    }

    public void OnTriggerEnter2D(){
      LeftDoorSign.SetActive(true);
      LastScenePlay.LeftDoorOpen = true;

    }

    public void OnTriggerExit2D(){
      LeftDoorSign.SetActive(false);
      LastScenePlay.LeftDoorOpen = false;

    }
}
