using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 target;
    public float speed = 5f;
    public bool moveTowardTarget = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        speed = Random.Range(2f, 5f);
        //moveTowardTarget = true;
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //moveTowardTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTowardTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (transform.position == target)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        if (collision.tag == "House")
        {
            GameSceneManager.Instance.destroyHouse(collision.GetComponent<HouseData>());
            Destroy(this.gameObject);
        }
        if (collision.tag == "Flash")
        {
            Instantiate(GameSceneManager.Instance.ScorePrefab, transform.position, Quaternion.identity);
            GameSceneManager.Instance.addPoints(50);
            Destroy(this.gameObject);
        }
    }
}
