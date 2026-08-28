using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OpponentManager : MonoBehaviour{
    public GameObject[] opponentCharacter;
    void Start(){
        if(opponentCharacter.Length==0){
            Debug.LogError("No opponent assigned to the Opponent Manager");
            return;
        }
        ActivateRandomOpponent();
    }
    void ActivateRandomOpponent(){
        int randomIndex=Random.Range(0,opponentCharacter.Length);
        for(int i=0;i<opponentCharacter.Length;i++){
            if(i==randomIndex){
                opponentCharacter[i].SetActive(true);
            }else{
                opponentCharacter[i].SetActive(false);
            }
        }
    }
}