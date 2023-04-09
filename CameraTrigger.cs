using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType{
    Wait,
    InputToWait,
    MoveToTarget,
    MoveToPlayer
}

[System.Serializable]
public class CameraAction{
    public ActionType actionType;

    [SerializeField]
    private float waitDuration;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private KeyCode inputKey = KeyCode.Space;

    public float WaitDuration{
        get { return waitDuration;}
        set { waitDuration = value;}
    }

    public Transform Target{
        get{ return target;}
        set {target = value;}
    }

    public KeyCode InputKey{
       get{ return inputKey;}
    set {inputKey = value;}
    }

}

public class CameraTrigger : MonoBehaviour
{
    public List<CameraAction> actions = new List<CameraAction>();
    public bool triggerOnce = true;
    private CameraMoveScript cameraMoveScript;

    private int currentActionIndex = 0;
    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraMoveScript = Camera.main.GetComponent<CameraMoveScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            if(!triggerOnce || (triggerOnce && !hasTriggered)){
                StartCoroutine(ExecuteCutscene());
                hasTriggered = true;
            }
        }
    }

    private IEnumerator ExecuteCutscene(){
        while(currentActionIndex < actions.Count){
            CameraAction currentAction = actions[currentActionIndex];
            switch(currentAction.actionType){
                case ActionType.Wait:
                    yield return cameraMoveScript.WaitAtTarget(currentAction.WaitDuration);
                    break;
                case ActionType.InputToWait:
                    yield return cameraMoveScript.InputToWait(currentAction.InputKey);
                    break;
                case ActionType.MoveToTarget:
                    cameraMoveScript.MoveToTarget(currentAction.Target);
                    break;
                case ActionType.MoveToPlayer:
                    cameraMoveScript.MoveToPlayer();
                    break;
                default:
                    Debug.LogError("Invalid, what did you do bro?");
                    break;
            }
            currentActionIndex++;
        }
        if(!triggerOnce){
            currentActionIndex = 0;
            hasTriggered = false;
        }
    }
}
