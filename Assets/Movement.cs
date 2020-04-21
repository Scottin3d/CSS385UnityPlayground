using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

  public float PlayerHitSpeed = 30f;
  public float PuckStartSpeed = 50f;
  public float MinPuckSpeed = 5f;
  public float MaxPuckSpeed = 15f;

  public float RBX;
  public float RBY;

  public Vector2 Velocity;
  public float Mag;
  Rigidbody2D RB;
  private Vector2 StartPos;
  // Start is called before the first frame update
  void Start() {
    RB = GetComponent<Rigidbody2D>();
    RB.AddForce(new Vector2(PuckStartSpeed, 0));
    StartPos = transform.position;
  }

  // Update is called once per frame
  
  void Update() {
    Mag = RB.velocity.magnitude;
    if (RB.velocity.magnitude >= MaxPuckSpeed) {
      RB.velocity = Vector2.ClampMagnitude(RB.velocity, MaxPuckSpeed);
    }

    if (RB.velocity.magnitude <= MinPuckSpeed) {
      RB.AddForce(Velocity * 3);
    }


    if (Mag == 0 && RB.velocity.x == 0 && RB.velocity.y == 0) {
      RB.AddForce(new Vector2(PuckStartSpeed, 0));
    }

    if (Input.GetKeyDown(KeyCode.Space)) {
      ResetPosition();
    }
  }
  

  void OnCollisionEnter2D(Collision2D collision) {
    float Magnitude = 20f;
    
    if (RB.velocity.x <= .7f || RB.velocity.x >= -0.7f) {
      RBX = RB.velocity.x / .2f;
      RBY = RB.velocity.y;
      RB.AddForce(new Vector2(RBX, RBY) * RB.velocity.magnitude);
    }
    
    
    if (collision.gameObject.tag == "Player") {
      Magnitude = PlayerHitSpeed;
    }

    RB.AddForce(-RB.velocity * Magnitude);


    Velocity = RB.velocity;
    Velocity.Normalize();
  }

  void ResetPosition() {
    transform.position = StartPos;
    RB.velocity = Vector2.zero;
    RB.AddForce(new Vector2(PuckStartSpeed, 0));
  }
}


