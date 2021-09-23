using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotationTestManager : MonoBehaviour{

    public bool canInteract = true;
    public float interactTimer;
    
    public List<GameObject> shapeCollection;

    public Transform shapePositionZero;
    public Transform[] shapePositions = new Transform[4];
    public Transform[] shapeColliders = new Transform[4];
    
    public int shapeTotal;
    public int shapeIndex = 0;
    public int selectedShape;

    public Transform[] shapes = new Transform[0];
    public Transform[] activeShapes = new Transform[4];

    void Awake()
    {
        //shapeColliders = shapePositions;
    }
    void Start(){
        shapeTotal = shapeCollection.Count;

        for (int i = 0; i < shapeCollection.Count; i++){
            shapeCollection[i].SetActive(false);
        }

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
        
        //shuffle positions
        ShufflePositions();
        
        //get and position shapes 
        for (int i = 0; i < shapes.Length; i++){
            shapes[i] = shapeCollection[shapeIndex].transform.GetChild(i);
        }
        shapes[0].transform.position = shapePositionZero.position;
        for (int i = 0; i < shapePositions.Length; i++){
            shapes[i+1].transform.position = shapePositions[i].position;
            Debug.Log(shapes[i]);
            //animate
        }
    }
    
    public void ShuffleShapes(){
        Transform temp;
        for (int i = 0; i < shapePositions.Length; i++) {
            int rnd = Random.Range(0, shapePositions.Length);
            temp = shapePositions[rnd];
            shapePositions[rnd] = shapePositions[i];
            shapePositions[i] = temp;
        }
        
    }
    public void ShufflePositions(){
        Transform temp;
        for (int i = 0; i < shapePositions.Length; i++) {
            int rnd = Random.Range(0, shapePositions.Length);
            temp = shapePositions[rnd];
            shapePositions[rnd] = shapePositions[i];
            shapePositions[i] = temp;
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
                    if (hit.transform == shapeColliders[0]){
                        Debug.Log(shapePositions[0].transform.name);
                        selectedShape = int.Parse(shapePositions[0].transform.name);
                    }
                    if (hit.transform == shapeColliders[1]){
                        Debug.Log(shapePositions[1].transform.name);
                        selectedShape = int.Parse(shapePositions[1].transform.name);
                    }
                    if (hit.transform == shapeColliders[2]){
                        Debug.Log(shapePositions[2].transform.name);
                        selectedShape = int.Parse(shapePositions[2].transform.name);
                    }
                    if (hit.transform == shapeColliders[3]){
                        Debug.Log(shapePositions[3].transform.name);
                        selectedShape = int.Parse(shapePositions[3].transform.name);
                    }
                    //get shape tag
                    string tag = shapes[selectedShape].GetComponent<ShapeTag>().tag.ToString();
                    Debug.Log(tag);
                    //change selected shape materials & animate
                    shapes[selectedShape].GetComponent<ShapeSelector>().Select();
                    Debug.Log(shapes[selectedShape]);
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
        for (int i = 1; i < shapes.Length; i++){
            shapes[i].GetComponent<ShapeSelector>().Reset();
        }
        
    }
    // private IEnumerator ResetInteraction
}
