using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScenePlay : MonoBehaviour{

    public bool LeftDoorOpen = false;
    public bool RightDoorOpen = false;

    void Start(){
    }


    void Update(){
      if (LeftDoorOpen && RightDoorOpen){
        StartCoroutine(LoadFinalScene());
      }

    }

    IEnumerator LoadFinalScene(){
      yield return new WaitForSeconds(1);
      SceneManager.LoadScene("EndScene");
    }
}
