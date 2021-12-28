using UnityEngine;

public class ToggleLabel : MonoBehaviour
{
    [SerializeField] GameObject labelPrefab;
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);
    GameObject newLabel;
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.T)){
            if(newLabel == null)
                ShowLabel();
            else{
                TurnOffLabel();
            }
        }
        
        // if (Input.GetKeyDown(KeyCode.K)){
        //     TurnOffLabel();
        // }
    }

    void TurnOffLabel(){
        if (newLabel != null){
            Destroy(newLabel);
        }
    }

    void ShowLabel(){
        // if (newLabel != null){
        //     return;
        // }
        Vector3 position = transform.position + offset;
        newLabel = Instantiate(labelPrefab, position, Quaternion.identity);
        newLabel.SetActive(true);
        newLabel.GetComponent<Label>().SetLabel(name);
    }
}
