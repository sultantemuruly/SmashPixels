using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class BasePlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private FloatingJoystick joystick;
    private Rigidbody rb;

    [SerializeField] private Animator anim;

    [SerializeField] private Transform inventoryTransform;
    [SerializeField] private int maxInventory;
    [SerializeField] private TextMeshProUGUI inventoryText;

    [SerializeField] private GameObject maxAlert;
    private bool isFull;

    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        inventoryText.text = inventoryTransform.childCount.ToString();
    }

    private void Update()
    {
        rb.velocity = new Vector3(moveSpeed * joystick.Horizontal, 0f, moveSpeed * joystick.Vertical);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Pixel" && !isFull)
        {
            GameObject pixel = col.gameObject;
            pixel.GetComponent<BoxCollider>().isTrigger = true;
            pixel.GetComponent<Rigidbody>().isKinematic = true;
            pixel.transform.parent = inventoryTransform;
            pixel.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

            for(int i=0; i<inventoryTransform.childCount; i++)
            {
                inventoryTransform.GetChild(i).transform.localPosition = new Vector3(0f, i*0.25f, 0f);
            }

            inventoryText.text = inventoryTransform.childCount.ToString();

            if(inventoryTransform.childCount >= maxInventory)
            {
                isFull = true;
                maxAlert.SetActive(true);
            }
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "PixelOre")
        {
            timer += Time.deltaTime;

            if(timer >= 0.3f)
            {
                timer = 0f;

                EventsManager.onOreGetsHit.Invoke(col.gameObject.GetComponent<PixelOre>().pixelOreId);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("PixelArt") && inventoryTransform.childCount > 0)
        {
            timer += Time.deltaTime;

            if(timer >= 0.3f)
            {
                timer = 0f;

                Transform pixelTransform = inventoryTransform.GetChild(inventoryTransform.childCount - 1).transform;
                pixelTransform.parent = null;
            
                pixelTransform.DOJump(other.transform.position, 10f, 1, 0.25f);
                Destroy(pixelTransform.gameObject, 0.25f);

                EventsManager.onFillWithPixels.Invoke(other.GetComponent<PixelArt>().pixelArtId);

                if(inventoryTransform.childCount < maxInventory)
                {
                    isFull = false;
                    maxAlert.SetActive(false);
                }
            }
        }
    }
}
