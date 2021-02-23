using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public Vector3 target;
    public float speed = 7f;
    public bool moveTowardTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
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
            Instantiate(GameSceneManager.Instance.flashPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
