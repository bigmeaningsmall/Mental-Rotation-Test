using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;
using Enums;
using TMPro;
using Random = UnityEngine.Random;

public class RotationTestManager : MonoBehaviour{

    [Header("UI")]
    public Button startButton;
    public Button nextButton;
    public Transform dividerLine1;
    public Transform titleText;
    public Transform titleInfo;
    public Transform scoreUIDisplay;
    public Transform scoreDisplay;
    public Transform timeTakenDisplay;

    [Range(0,0.5f)]
    public float animationDuration = 0.125f;

    [Header("OBJECTS")]
    public Tag currentAnswer;
    
    public bool testEnded = false;

    public bool canInteract = true;
    public float interactTimer;
    
    public List<GameObject> shapeCollection;

    public Transform shapePositionZero;
    public Transform[] shapePositions = new Transform[4];

    public int shapeTotal;
    public int shapeIndex = 0;
    public int selectedShape;

    public Transform[] shapes = new Transform[0];
    public Transform[] activeShapes = new Transform[4];

    [Header("SCORE VARIABLES")]
    private bool timerOn;
    public float timeTaken = 0; //use this to time how long each answer takes (do not count q1 & q2 - easy)
    public bool[] answers = new bool[0];
    public int score;

    void Awake(){
        shapeTotal = shapeCollection.Count;
        answers = new bool[shapeTotal];

        dividerLine1.transform.DOScale(0, 0);
        titleText.transform.DOScale(0, 0);
        titleInfo.transform.DOScale(0, 0);
        
        scoreUIDisplay.transform.DOScale(0, 0);
        scoreDisplay.transform.DOScale(0, 0);
        timeTakenDisplay.transform.DOScale(0, 0);
        
        startButton.transform.DOScale(0, 0);
        startButton.interactable = true;
        
        
        nextButton.transform.DOScale(0, 0);
        nextButton.interactable = false;

        StartCoroutine(InitialiseInferface());
    }

    private IEnumerator InitialiseInferface(){
        yield return new WaitForSeconds(1f);
        
        titleText.transform.DOScale(1, animationDuration*4);
        
        yield return new WaitForSeconds(animationDuration*4);

        titleInfo.transform.DOScale(1, animationDuration*4);
        
        yield return new WaitForSeconds(animationDuration*4);
        
        startButton.transform.DOScale(1, animationDuration*4);
    }

    private void RemoveStartInterface(){
        titleText.transform.DOScale(0, animationDuration*4);
        titleInfo.transform.DOScale(0, animationDuration*3);
        startButton.transform.DOScale(0, animationDuration*2);
    }
    void Start(){
        
        //deactivate all
        for (int i = 0; i < shapeCollection.Count; i++){
            shapeCollection[i].SetActive(false);
        }
        
    }
    public void SetShapes(){
        //enable the next button
        nextButton.transform.DOScale(1, 0.5f);
        //deactivate the next button
        nextButton.interactable = false;
        
        //deactivate all
        for (int i = 0; i < shapeCollection.Count; i++){
            shapeCollection[i].SetActive(false);
        }
        
        //activate shape at index
        shapeCollection[shapeIndex].SetActive(true);
        
        //get child shapes from index
        shapes = new Transform[shapeCollection[shapeIndex].transform.childCount];


        //get shapes 
        for (int i = 0; i < shapes.Length; i++){
            shapes[i] = shapeCollection[shapeIndex].transform.GetChild(i);
        }
        
        //get the 4 active shapes - exclude 0 in array 
        for (int i = 1; i < shapes.Length; i++){
            activeShapes[i-1] = shapes[i];
        }
        // add colliders to active shapes
        for (int i = 0; i < activeShapes.Length; i++){
            activeShapes[i].gameObject.AddComponent<BoxCollider>();
            BoxCollider c = activeShapes[i].GetComponent<BoxCollider>();
            c.size = new Vector3(5, 5, 1);
        }
        
        //shuffle shapes
        ShuffleShapes();

        
        //position and scale down shapes
        shapes[0].transform.position = shapePositionZero.position;
        shapes[0].transform.DOScale(0, 0);
        for (int i = 0; i < shapePositions.Length; i++){
            activeShapes[i].transform.position = shapePositions[i].position;
            activeShapes[i].transform.DOScale(0, 0);
            Debug.Log(shapes[i]);
            
            
        }
       
        //animate shapes on
        StartCoroutine(ShapeSetAnimation(animationDuration));
    }

    private IEnumerator ShapeSetAnimation(float d){

        yield return new WaitForSeconds(d*4);
        
        shapes[0].transform.DOScale(1, d*4);
        
        yield return new WaitForSeconds(d*4);
        
        dividerLine1.transform.DOScale(1, d*4);
        
        yield return new WaitForSeconds(d*2);
        
        for (int i = 0; i < activeShapes.Length; i++){
            activeShapes[i].transform.DOScale(1, d);
            Debug.Log(shapes[i]);
            yield return new WaitForSeconds(d*2);

        }
        timerOn = true;
    }
    private IEnumerator ShapeRemoveAnimation(float d){
        yield return new WaitForSeconds(0);
        for (int i = 0; i < shapes.Length; i++){
            activeShapes[i].transform.DOScale(1, d);
            Debug.Log(shapes[i]);
            yield return new WaitForSeconds(d*2);

        }
    }

    private IEnumerator CheckAnswerSequence(float d){
        Debug.Log("NEXT");
        
        if (currentAnswer == Tag.Correct){
            answers[shapeIndex] = true;
            shapes[0].GetComponent<ShapeSelector>().Correct();
            activeShapes[selectedShape].GetComponent<ShapeSelector>().Correct();
        }
        else if (currentAnswer == Tag.Incorrect){
            answers[shapeIndex] = false;
            shapes[0].GetComponent<ShapeSelector>().Incorrect();
            activeShapes[selectedShape].GetComponent<ShapeSelector>().Incorrect();
        }
        
        yield return new WaitForSeconds(d*20);
        
        dividerLine1.transform.DOScale(0, d);
        
        for (int i = 0; i < shapes.Length; i++){
            shapes[i].transform.DOScale(0, d);
            shapes[i].GetComponent<ShapeSelector>().ResetColour();
        }
        yield return new WaitForSeconds(d*10);

        
        Debug.Log("NEXT");
        
        //yield return new WaitForSeconds(1f);
        
        shapeIndex++;
        
        //check if ended
        testEnded = CheckEnded();
        
        //check if 
        if (!testEnded){
            //change to next shape
            SetShapes();
        }
        else{
            //go to end of test
            CheckFinalScore();
            StartCoroutine(EndTestSequence());
        }

    }
    private IEnumerator EndTestSequence(){
        scoreDisplay.transform.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString() + " / " + answers.Length.ToString();
        timeTakenDisplay.transform.GetComponent<TextMeshProUGUI>().text = "Time Taken: " + timeTaken.ToString() + "s";
        
        yield return new WaitForSeconds(1f);
        Debug.Log("ENDED");
        scoreUIDisplay.DOScale(1, animationDuration * 5);
        
        yield return new WaitForSeconds(animationDuration * 5);
        scoreDisplay.DOScale(1, animationDuration * 2);
        timeTakenDisplay.DOScale(1, animationDuration * 2);
    }

    public void CheckFinalScore(){
        for (int i = 0; i < answers.Length; i++){
            if (answers[i] == true){
                score++;
            }
        }
    }
    public void ShuffleShapes(){
        Transform temp;
        for (int i = 0; i < activeShapes.Length; i++) {
            int rnd = Random.Range(0, activeShapes.Length);
            temp = activeShapes[rnd];
            activeShapes[rnd] = activeShapes[i];
            activeShapes[i] = temp;
        }
        
    }
    private bool CheckEnded(){
        bool b = shapeIndex >= shapeCollection.Count;
        return b;
    }
    private void ResetSelection(){
        for (int i = 0; i < activeShapes.Length; i++){
            activeShapes[i].GetComponent<ShapeSelector>().Reset();
        }
    }

    void Update(){
        if (canInteract && activeShapes[0]!=null){
            if (Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)){
                    interactTimer = 0;
                    ResetSelection();

                    if (hit.transform == activeShapes[0]){
                        selectedShape = 0;
                        activeShapes[0].GetComponent<ShapeSelector>().Select();
                        nextButton.interactable = true;
                    }
                    if (hit.transform == activeShapes[1]){
                        selectedShape = 1;
                        activeShapes[1].GetComponent<ShapeSelector>().Select();
                        nextButton.interactable = true;
                    }
                    if (hit.transform == activeShapes[2]){
                        selectedShape = 2;
                        activeShapes[2].GetComponent<ShapeSelector>().Select();
                        nextButton.interactable = true;
                    }
                    if (hit.transform == activeShapes[3]){
                        selectedShape = 3;
                        activeShapes[3].GetComponent<ShapeSelector>().Select();
                        nextButton.interactable = true;
                    }
                    
                    //get shape tag
                    currentAnswer = activeShapes[selectedShape].GetComponent<ShapeTag>().tag;

                }
                else{
                    ResetSelection();
                    nextButton.interactable = false;
                }
            }
        }
        
        interactTimer += Time.deltaTime;
        if (interactTimer < DAO.instance.animationDuration+0.05f){
            canInteract = false;
        }
        else{
            canInteract = true;
        }

        if (interactTimer > 10){ interactTimer = 1; }
        
    }

    void LateUpdate(){
        if (timerOn){
            timeTaken += Time.deltaTime;
        }
    }


    //button interaction - start
    public void StartTest(){
        //remove start UI
        RemoveStartInterface();
        //set the first shape
        SetShapes();
    }
    //button interaction - next
    public void NextShape(){
        Debug.Log("NEXT");
        timerOn = false;
        nextButton.interactable = false;
        StartCoroutine(CheckAnswerSequence(animationDuration));
    }


}
