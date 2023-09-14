using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public string type;
    Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start() {
        Jump();
    }

    void Jump() {
        float randomJumpForce = Random.Range(3f, 6f);
        Vector2 jumpVelocity = Vector2.up * randomJumpForce;
        jumpVelocity.x = Random.Range(-2f, 2f);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
