using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RotationTestManager : MonoBehaviour{

    public Button nextButton;
    
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

    void Awake(){
        
    }
    void Start(){
        shapeTotal = shapeCollection.Count;

        for (int i = 0; i < shapeCollection.Count; i++){
            shapeCollection[i].SetActive(false);
        }

        nextButton.interactable = false;
        
        SetShapes();
    }
    void SetShapes(){
        
        //deactivate all
        for (int i = 0; i < shapeCollection.Count; i++){
            shapeCollection[i].SetActive(false);
        }
        
        //activate shape at index
        shapeCollection[shapeIndex].SetActive(true);
        
        //get child shapes from index
        shapes = new Transform[shapeCollection[shapeIndex].transform.childCount];


        //get and position shapes 
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
        
        shapes[0].transform.position = shapePositionZero.position;
        for (int i = 0; i < shapePositions.Length; i++){
            activeShapes[i].transform.position = shapePositions[i].position;
            Debug.Log(shapes[i]);
            //animate
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

    void Update(){
        if (canInteract){
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
                    string tag = activeShapes[selectedShape].GetComponent<ShapeTag>().tag.ToString();
                    Debug.Log(tag);
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

    private void ResetSelection(){
        for (int i = 0; i < activeShapes.Length; i++){
            activeShapes[i].GetComponent<ShapeSelector>().Reset();
        }
        
    }
    // private IEnumerator ResetInteraction
}
