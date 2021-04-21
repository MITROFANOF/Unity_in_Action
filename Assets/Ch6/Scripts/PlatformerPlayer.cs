using UnityEngine;

namespace Ch6.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
    public class PlatformerPlayer : MonoBehaviour
    {
        public float speed = 250f;
        public float jumpForce = 12f;

        private Rigidbody2D _body;
        private Animator _anim;
        private BoxCollider2D _box;
        private static readonly int ParamSpeed = Animator.StringToHash("speed");

        private void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _box = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            var deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            var movement = new Vector2(deltaX, _body.velocity.y);
            _body.velocity = movement;

            var bounds = _box.bounds;
            var max = bounds.max;
            var min = bounds.min;

            var corner1 = new Vector2(max.x, min.y - 0.1f);
            var corner2 = new Vector2(min.x, min.y - 0.2f);
            var hit = Physics2D.OverlapArea(corner1, corner2);

            bool grounded = hit;

            _body.gravityScale = grounded && deltaX == 0 ? 0 : 1;

            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            MovingPlatform platform = null;
            if (hit)
            {
                platform = hit.GetComponent<MovingPlatform>();
            }

            transform.parent = platform ? platform.transform : null;

            _anim.SetFloat(ParamSpeed, Mathf.Abs(deltaX));

            if (!Mathf.Approximately(deltaX, 0))
            {
                transform.localScale = new Vector3(Mathf.Sign(deltaX), 1f, 1f);
            }

            var pScale = Vector3.one;
            
            if (platform)
            {
                pScale = platform.transform.localScale;
            }

            if (deltaX != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
            }
        }
    }
}