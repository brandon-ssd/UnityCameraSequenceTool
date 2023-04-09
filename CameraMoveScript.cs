using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public Transform player; // Transform of player object, camera's target to follow
    public float moveSpeed = 5f; // Speed at which the Lerp moves to target
    public float returnSpeed = 5f;// Speed at which the Lerp moves back to player
    public player playerController; // Needed for disable movement for cutscenes
    public bool movingToTarget = false; // Decides if we move to Player or Target objects. Very Important!
    private Vector3 targetPosition; // Position of your target object
    public GameObject TargetObject; // Used to update every frame of the target's coordinates


    // Start is called before the first frame update
    void Start()
    {
        if (player == null){ // Auto-Find player with Player tag
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {   // Constantly updates the target's position.
        if (movingToTarget){ 
            targetPosition = TargetObject.transform.position;
        }else{
            targetPosition = player.position;
        }
    }

    private void LateUpdate(){
        // If movingToTarget is true, we move to the target. If it's false, we move back to player.
        if(movingToTarget){
            transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), Time.deltaTime * moveSpeed);
            // Stop all player actions and animations here.
            playerController.enabled = false;
            playerController.body.velocity = Vector2.zero;
            playerController.animator.speed = 0;        
        }else{
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), Time.deltaTime * returnSpeed);
            // Regain control of the player here.
            playerController.enabled = true;
            playerController.animator.speed = 1;
        }
    }

    public void MoveToTarget(Transform target){ // Setting bool to true causes camera to move to target.
        movingToTarget = true;
        this.targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        TargetObject = target.gameObject;
    }

    public IEnumerator WaitAtTarget(float duration){ // waits for x amount of seconds
        yield return new WaitForSeconds(duration);
    }

    public IEnumerator InputToWait(KeyCode inputKey){ // Make sure you add a Wait action after this.
        while(!Input.GetKeyDown(inputKey)){
            yield return null;
        }
    }

    public void MoveToPlayer(){ // Moves camera back to player.
        movingToTarget = false;
    }

}
