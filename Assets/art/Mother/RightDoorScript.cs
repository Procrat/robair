using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDoorScript : MonoBehaviour{


public LastScenePlay LastScenePlay;
public GameObject RightDoorSign;


  void Start(){
    RightDoorSign.SetActive(false);
  }

  void Update(){
  }

  public void OnTriggerEnter2D(){
    RightDoorSign.SetActive(true);
    LastScenePlay.RightDoorOpen = true;

  }

  public void OnTriggerExit2D(){
    RightDoorSign.SetActive(false);
    LastScenePlay.RightDoorOpen = false;

  }
}
