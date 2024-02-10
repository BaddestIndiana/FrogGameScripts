using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 MoveInput;
    public Vector2 LookInput;
    public Animator anim;
    public bool grounded, paused, toungeOut, punching, trig;
    public Transform PlayerCamera;
    public GameObject[] Spawns;
    public float xRot, speed, Jumpforce, Sensitivity, prevSensitivity;
    public int health, rand, kills;
    public Camera cam;

    public GameObject pausePan, tounge, punch, sp, st, gameMode, ffa, H1Pan, H2Pan, PunchText;
    public Rigidbody rb;
    RaycastHit hit;

    public PhotonView view, collview;



    public string hat, shoes, texture;

    void Start()
    {
        
        PunchText = GameObject.FindGameObjectWithTag("punchtxt");
        ffa = GameObject.FindGameObjectWithTag("ffa");
        pausePan = GameObject.FindGameObjectWithTag("pause");
        H1Pan = GameObject.FindGameObjectWithTag("h1");
        H2Pan = GameObject.FindGameObjectWithTag("h2");
        
        Spawns = GameObject.FindGameObjectsWithTag("Respawn");
        health = 3;
        
        view = this.GetComponent<PhotonView>();
        if (this.view.IsMine)
        {
            H1Pan.SetActive(false);
            H2Pan.SetActive(false);
            pausePan.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cam = GetComponentInChildren(typeof(Camera)) as Camera;
            cam.enabled = true;
        }
    }
    void Update()
    {
        if (this.view.IsMine)
        {
            MoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            LookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            PlayerMoves();
            CameraMoves();
            GroundCheck();
        }
    }
    void PlayerMoves()
    {
        Vector3 MoveVector = transform.TransformDirection(MoveInput) * speed;
        if (MoveVector.x == 0 && MoveVector.y == 0)
        {
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Move", true);
        }
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Debug.Log("Jumped");
            rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            grounded = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pausePan.SetActive(true);
                paused = true;
                prevSensitivity = Sensitivity;
                Sensitivity = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pausePan.SetActive(false);
                paused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Sensitivity = prevSensitivity;
            }
        }
        if (Input.GetMouseButtonDown(0) && punching == false)
        {
            PunchText.SetActive(false);
            punching = true;
            anim.SetBool("Punch", true);
            view.RPC("InstantiatePunch", RpcTarget.All);
            StartCoroutine(Punch());
        }
    }
    void CameraMoves()
    {
        xRot -= LookInput.y * Sensitivity;

        transform.Rotate(0f, LookInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
    void GroundCheck()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "ground" && hit.distance < 0.5f)
            {
                Debug.Log("on floor");
                grounded = true;
                anim.SetBool("Jump", false);
            }
            else
            {
                grounded = false;
                anim.SetBool("Jump", true);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Punch" && trig == false)
        {
            trig = true;
            Debug.Log("hitsomethin");
            collview = other.gameObject.GetComponent<PhotonView>();
            if (collview.Controller != view.Controller)
            {
                health--;
                heathCheck();
                Debug.Log("hit");                
                Debug.Log(health);         
            }
            StartCoroutine(trigger()); 
        }
    }
    public void heathCheck()
    {
        switch (health)
        {
            case 0:
                view.RPC("Death", RpcTarget.All);
                H1Pan.SetActive(false);
                H2Pan.SetActive(false);
                break;
            case 1:
                H1Pan.SetActive(true);
                H2Pan.SetActive(false);
                Debug.Log("H1");
                break;
            case 2:
                H2Pan.SetActive(true);
                Debug.Log("H2");
                break;
        }

    }
    [PunRPC]
    public void Death()
    {
        Debug.Log("you died");
        ffa.GetComponent<FFAMGR>().runWinOnALL(collview.Owner);
        view.RPC("Respawn", RpcTarget.All);
        health = 3;
    }
    IEnumerator Punch()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Punch", false);
        punching = false;
        PhotonNetwork.Destroy(sp);
    }
    IEnumerator trigger()
    {
        yield return new WaitForSeconds(1);
        trig = false;
    }

    [PunRPC] void InstantiateGrab()
    {
        if (view.IsMine)
        {
            st = PhotonNetwork.Instantiate(tounge.name, transform.position, Quaternion.identity);
        }
    }
    [PunRPC] void InstantiatePunch()
    {
        if (view.IsMine)
        {
            sp = PhotonNetwork.Instantiate(punch.name, transform.position, Quaternion.identity);
            sp.transform.parent = this.gameObject.transform;
        }
    }
    [PunRPC] void Respawn()
    {
        if (view.IsMine)
        {
            rand = Random.Range(1, 5);
            this.gameObject.transform.position = Spawns[rand].transform.position;
        }
    }
}
