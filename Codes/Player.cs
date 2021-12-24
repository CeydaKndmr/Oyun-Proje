using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   public float speed;
   public Rigidbody2D rigidbody;
   public float jumpSpeed;
   public Animator anim;
   public int life;
   public float damagedTime;
   public Color normalColor;
   public Color damagedColor;
   public Text lifeText;
   public ItemSlot[] inventory;


    // Start is called before the first frame update
    void Start()
    {
        normalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
      if(Input.GetKeyDown(KeyCode.Space) && gameObject.transform.Find("Foot").GetComponent<GroundCheck>().isGrounded)
      {
     rigidbody.AddForce(new Vector2(0,jumpSpeed));
      }
      if(damagedTime > 0 )
      {
          damagedTime -= Time.deltaTime;
          if(gameObject.GetComponent<SpriteRenderer>().color == normalColor)
          {
              gameObject.GetComponent<SpriteRenderer>().color = damagedColor;

          }
      }
      else
      {
          if( gameObject.GetComponent<SpriteRenderer>().color == damagedColor)
          {
              gameObject.GetComponent<SpriteRenderer>().color = normalColor;
          }
      }


    }
    void FixedUpdate()
    {
       float hor = Input.GetAxisRaw("Horizontal");
       anim.SetFloat("Speed",Mathf.Abs(hor));


       rigidbody.velocity = new Vector2(hor * speed, rigidbody.velocity.y);

       if(hor > 0.1f)
       {
           gameObject.transform.localScale =new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);
       }
       else if (hor < -0.1f)
       {
           gameObject.transform.localScale =new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 0);

       }

    }

private void OnCollisionEnter2D(Collision2D collision)
{
 if (collision.gameObject.tag == "enemy"  && damagedTime <=0)
       {
         Hit();
       }
}


private void OnCollisionStay2D(Collision2D collision)
{
 if (collision.gameObject.tag == "enemy"  && damagedTime <=0)
       {
           Hit();
       }
}


public void Hit()
{

           life--;
           lifeText.text = life.ToString();
           damagedTime = 0.7f;
           gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +0.6f , gameObject.transform.position.z);
           if(life <= 0)
           {
               SceneManager.LoadScene("SampleScene");
           }
}

public void UpdateLifeText()
{
    lifeText.text = life.ToString();
}
public void AddItem(Item item)
{
    for(int i = 0; i < inventory.Length; i++)
{
    if(inventory[i].isEmpty)
    {
        inventory[i].SetItem(item);
        break;
    }
}

}

}
