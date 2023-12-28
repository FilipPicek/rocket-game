using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CollisionHnadler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip boom;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem boomParticles;
   

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable= false;


    void Start() 
    {
        audioSource=GetComponent<AudioSource>();

        
    }
    void Update() 
    {
        GoTONextLevel();
    }
    void GoTONextLevel()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel1();

        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable=!collisionDisable; //toggle collison

        }

    }



    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
              Debug.Log("This thing is firendly");
              break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
              Debug.Log("You picked up Fuel");
              break;
            default:
              StartChrashSequence(); 
              break;
            

        }
    }

    void StartSuccessSequence()
    {
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();


         GetComponent<Movement>().enabled = false;
         Invoke("LoadLevel1", levelLoadDelay);
    }

    void StartChrashSequence()
    {
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(boom);
        boomParticles.Play();
        



        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadLevel1()
    {
        int currentSceneIndex =SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=currentSceneIndex+1;
        if (nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }
   

    void ReloadLevel()
    {
        int currentSceneIndex =SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
  
}
