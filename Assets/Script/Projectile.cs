using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifeTime;

    private BoxCollider2D hitBox;

    private void Awake()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (hit)
            return;
        float flySpeed = speed * Time.deltaTime * direction;
        transform.Translate(flySpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        hitBox.enabled = false;
        gameObject.SetActive(false);

        if (collision.tag == "Mob")
            collision.GetComponent<Health>().TakeDamge(1);
    }

    public void SetDirection (float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        hitBox.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
