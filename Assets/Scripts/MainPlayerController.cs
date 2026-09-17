using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]

public class MainPlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 move;
    [SerializeField]
    private float speed = 5.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 movDir = (move.y * transform.forward) + (move.x * transform.right);
       movDir.y = 0;
       characterController.Move(movDir * Time.deltaTime * speed); 
    }

    public void OnMove(InputValue value){
        move = value.Get<Vector2>();
    }

}
