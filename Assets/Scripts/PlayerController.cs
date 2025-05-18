using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerControler : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Animation anim;

    public bool gameOver = false;

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animation>(); // Access Animation component on child

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (gameOverText != null)
            gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
            return;

        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            input = Vector3.forward;
        else if (Input.GetKey(KeyCode.DownArrow))
            input = Vector3.back;
        else if (Input.GetKey(KeyCode.LeftArrow))
            input = Vector3.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            input = Vector3.right;

        Vector3 horizontalMovement = input * moveSpeed;

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y = -1f;
        }

        moveDirection.x = horizontalMovement.x;
        moveDirection.z = horizontalMovement.z;

        characterController.Move(moveDirection * Time.deltaTime);

        if (input != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(input);

            if (anim && !anim.IsPlaying("Run"))
                anim.Play("Run");
        }
        else
        {
            if (anim && !anim.IsPlaying("Idle"))
                anim.Play("Idle");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            Time.timeScale = 0f;

            if (anim)
                anim.Play("Idle");

            // Show UI
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            if (gameOverText != null)
            {
                gameOverText.text = "Game Over";
                gameOverText.gameObject.SetActive(true);
            }
        }
    }
}
