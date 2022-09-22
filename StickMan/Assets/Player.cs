using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float gravity;
    public GameObject _camera;
    public GameObject pistol;
    public GameObject dummy;

    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    private Vector3 move;
    private Vector3 cameraPosition;
    private RaycastHit hit;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        cameraPosition = _camera.transform.position;
        Debug.DrawRay(cameraPosition, _camera.transform.forward * 100, Color.red);
        CharacterControll();
        HoldGun();
    }

    private void CharacterControll()
    {
        float mouse = Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + mouse, transform.eulerAngles.z);

        float x = 0, z = 0;
        if (controller.isGrounded)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                z *= 2;
                Mathf.Min(z, -1);
            }

            animator.SetFloat("x", x);
            animator.SetFloat("z", z);

            move = new Vector3(x, 0, z);

            move = transform.TransformDirection(move);

            move *= speed * Time.deltaTime;
        }
        else
        {
            move.y -= gravity * Time.deltaTime;
        }

        controller.Move(move);
    }

    private void HoldGun()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (animator.GetBool("Gun"))
            {
                animator.SetBool("Gun", false);
                pistol.SetActive(false);

                _camera.transform.localPosition = new Vector3(0f, 3.4f, -10f);
                _camera.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
            }
            else
            {
                animator.SetBool("Gun", true);
                pistol.SetActive(true);

                _camera.transform.localPosition = new Vector3(2f, .4f, -4);
                _camera.transform.localEulerAngles = new Vector3(10f, -5f, 0f);
            }
        }

        if (animator.GetBool("Gun") && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cameraPosition, _camera.transform.forward * 5, out hit, 100f))
            {
                if (hit.collider)
                {
                    Debug.Log("a");
                    hit.transform.GetComponent<Dummy>().Hit();
                }
            }
        }
    }
}
