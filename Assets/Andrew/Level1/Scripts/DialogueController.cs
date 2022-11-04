using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject dialogueWindow;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float newCameraSize;

    private float _initialCameraSize;
    private GameObject _player;

    private bool _canCancel = false;
    // Start is called before the first frame update
    void Start()
    {
        _initialCameraSize = virtualCamera.m_Lens.OrthographicSize;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && _canCancel)
        {
            EndCutScene();
            _canCancel = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCutScene();
        }
    }

    private void StartCutScene()
    {
        dialogueWindow.SetActive(true);
        //text.SetActive(true);
        StartCoroutine("ChangeCameraSize");
        Invoke("EnableCancel", 3);
    }

    private void EndCutScene()
    {
        dialogueWindow.SetActive(false);
        text.SetActive(false);
        StartCoroutine("ChangeCameraSize");
        Destroy(gameObject, 2);
    }

    IEnumerator ChangeCameraSize()
    {
        Movement movement = _player.GetComponent<Movement>();
        movement.canMove = !movement.canMove;

        float delta = (_initialCameraSize - newCameraSize) / 60.0f;

        if (virtualCamera.m_Lens.OrthographicSize == newCameraSize)
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize += delta;
                yield return new WaitForSeconds(1 / 60.0f);
            }
            virtualCamera.m_Lens.OrthographicSize = _initialCameraSize;
        }
        else
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize -= delta;
                yield return new WaitForSeconds(1 / 60.0f);
            }
            virtualCamera.m_Lens.OrthographicSize = newCameraSize;
        }
    }

    private void EnableCancel()
    {
        _canCancel = true;
    }
}
