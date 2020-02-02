using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneAnimScript : MonoBehaviour{

    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject Scene4;
    public GameObject Scene5;
    public GameObject Scene6;
    public GameObject Scene7;
    public GameObject Scene8;
    public GameObject Scene9;
    public GameObject Scene10;
    public GameObject Scene11;
    public GameObject ReplayButton;
    public bool EndofScenereached;

    void Start(){
      Scene1.SetActive(true);
      Scene2.SetActive(false);
      Scene3.SetActive(false);
      Scene4.SetActive(false);
      Scene5.SetActive(false);
      Scene6.SetActive(false);
      Scene7.SetActive(false);
      Scene8.SetActive(false);
      Scene9.SetActive(false);
      Scene10.SetActive(false);
      Scene11.SetActive(false);
      ReplayButton.SetActive(false);

      StartCoroutine(NextScene());

    }


    void Update(){
      if(EndofScenereached){
        if (Input.GetKeyDown(KeyCode.Space)){
          SceneManager.LoadScene("StartScene");
        }
      }

    }

    IEnumerator NextScene(){
      yield return new WaitForSeconds(1);
      Scene1.SetActive(false);
      Scene2.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene2.SetActive(false);
      Scene3.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene3.SetActive(false);
      Scene4.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene4.SetActive(false);
      Scene5.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene5.SetActive(false);
      Scene6.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene6.SetActive(false);
      Scene7.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene7.SetActive(false);
      Scene8.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene8.SetActive(false);
      Scene9.SetActive(true);
      yield return new WaitForSeconds(1);
      Scene9.SetActive(false);
      Scene10.SetActive(true);
      yield return new WaitForSeconds(2);
      Scene10.SetActive(false);
      Scene11.SetActive(true);
      yield return new WaitForSeconds(1);
      ReplayButton.SetActive(false);
      EndofScenereached = true;

    }
}
