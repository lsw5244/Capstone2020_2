using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour {

    [SerializeField] float      m_speed = 1.4f;
    [SerializeField]            bool m_attackAnticipation = true;
    [SerializeField]            bool m_noBlood = false;
    [SerializeField] GameObject m_projectile;
    [SerializeField] Vector3    m_projectionSpawnOffset;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;

	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e")) {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if(Input.GetMouseButtonDown(0)) {
            m_animator.SetBool("hasAttackAnticipation", m_attackAnticipation);
            m_animator.SetTrigger("Attack");
        }

        //Use Item
        else if (Input.GetMouseButtonDown(1)) {
            m_animator.SetTrigger("UseItem");
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    // Function SpawnArrow called in Archer attack animation
    public void SpawnArrow()
    {
        if (m_projectile != null)
        {
            // Set correct arrow spawn position
            Vector3 projectionSpawnPosition = transform.localPosition + Vector3.Scale(m_projectionSpawnOffset, transform.localScale);
            GameObject arrow = Instantiate(m_projectile, projectionSpawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            arrow.transform.localScale = transform.localScale;
        }
    }
}
