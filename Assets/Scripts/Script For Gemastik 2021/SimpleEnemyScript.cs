using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyScript : MonoBehaviour
{
    private GravityAttractor planet;
    private Transform target;
    [Range(1f, 5f)] public float speed = 4f;
    private float rotSpeed = 75f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Hati - hati pakai ini, kalau bisa hanya player aja
        target = GameObject.FindObjectOfType<Gravity_Body>().transform;
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
    }

    private void FixedUpdate() {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position , speed * Time.deltaTime);
        rb.MovePosition(pos);
        planet.AttractOtherObject(this.transform);
        transform.Rotate(Vector3.up, Time.deltaTime * rotSpeed);
    }

    private void OnCollisionEnter(Collision col) {
        
        if(col.gameObject.CompareTag("Player"))
            this.gameObject.SetActive(false);
    }
}
