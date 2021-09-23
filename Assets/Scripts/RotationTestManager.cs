using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotationTestManager : MonoBehaviour{
    
    public List<GameObject> shapes;

    public Transform shapePositionZero;
    public Transform[] shapePositions = new Transform[4];
    public Transform[] shapeColliders = new Transform[4];
    
    public int shapeTotal;
    public int shapeIndex = 0;
    public int selectedShape;

    public Transform[] s = new Transform[0];

    void Awake()
    {
        //shapeColliders = shapePositions;
    }
    void Start(){
        shapeTotal = shapes.Count;

        for (int i = 0; i < shapes.Count; i++){
            shapes[i].SetActive(false);
        }

        SetShapes();
    }
    void SetShapes(){
        
        //deactivate all
        for (int i = 0; i < shapes.Count; i++){
            shapes[i].SetActive(false);
        }
        
        //activate shape at index
        shapes[shapeIndex].SetActive(true);
        
        //get child shapes from index
        s = new Transform[shapes[shapeIndex].transform.childCount];
        
        //shuffle positions
        ShufflePositions();
        
        //get and position shapes 
        for (int i = 0; i < s.Length; i++){
            s[i] = shapes[shapeIndex].transform.GetChild(i);
        }
        s[0].transform.position = shapePositionZero.position;
        for (int i = 0; i < shapePositions.Length; i++){
            s[i+1].transform.position = shapePositions[i].position;
            Debug.Log(s[i]);
            //animate
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
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)){
                if (hit.transform == shapeColliders[0]){
                    Debug.Log(shapePositions[0].transform.name);
                    selectedShape = int.Parse(shapePositions[0].transform.name);
                    //Debug.Log(shapes[selectedShape].GetComponent<ShapeTag>);
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
            }
        }
    }
}
