using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody playerRigid;
    Vector3 movement;
    public float speed;//ความเร็วการเดิน
    public float turnSpeed = 10f;//ควมเร็วการหมุน
    Animator anim;

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();//ต้องการ Rigidbody
        anim = GetComponent<Animator>();//ต้องการ Animator
        Cursor.visible = false;//ปิดเมาส์
        Cursor.lockState = CursorLockMode.Locked;//ล็อกเมาส์
        
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");//รับค่าปุ่มเดินกน้าถอยหลัง +1 -1
        float v = Input.GetAxisRaw("Vertical");//รับค่าปุ่มขวา ซ้าย +1 -1
        Turning(h,v);
        Move(h, v);
        Animating(h, v);
        Jump();
        Fire();
    
    }
    void Fire()
    {
        
        if (Input.GetButton("Fire1"))//เช็คคลิกเมาส์ซ้าย
        {
            
            movement = Camera.main.transform.forward;//ยึดเดินหน้าตามกล้อง
            movement.y = 0;//ไม่รับค่าขึ้นลงตามแกน y
            movement.Normalize();
            Quaternion newDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);

        }
        
    }
    void Jump()
    {
        
        if (Input.GetButton("Jump"))
        {
            
            movement.y= 1;
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigid.MovePosition(transform.position + movement);
        }
    }
       

    void Move(float h,float v)//เคลื่อนไหว
    {
        if (Input.GetButton("Vertical") && v > 0)//รับปุ่มเดินหน้าและเช็คว่าเป็นปุ่มที่มีค่า +1(w)
        {
            movement = Camera.main.transform.forward;
            movement.y = 0;
            movement.Normalize();
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigid.MovePosition(transform.position + movement);
        }
        else if (Input.GetButton("Vertical") && v < 0)//รับปุ่มถอยหลังและเช็คว่าเป็นปุ่มที่มีค่า -1(s)
        {
            movement = -Camera.main.transform.forward;
            movement.y = 0;
            movement.Normalize();
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigid.MovePosition(transform.position + movement);
        }
        else if (Input.GetButton("Horizontal") && h > 0)//รับปุ่มขวาและเช็คว่าเป็นปุ่มที่มีค่า +1(d)
        {
            movement = Camera.main.transform.right;
            movement.y = 0;
            movement.Normalize();
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigid.MovePosition(transform.position + movement);
        }
        else if (Input.GetButton("Horizontal") && h < 0)//รับปุ่มซ้ายและเช็คว่าเป็นปุ่มที่มีค่า -1(a)
        {
            movement = -Camera.main.transform.right;
            movement.y = 0;
            movement.Normalize();
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigid.MovePosition(transform.position + movement);
        }

    }
  
    void Animating(float h, float v)//จัดการอนิเมชั่น
    {
        bool isWalking = false;
        if(h!=0 || v != 0)//เมื่อกดปุ่มเดิน
        {
            isWalking = true;
        }

        anim.SetBool("IsFire", false);
        if (Input.GetButton("Fire1"))//เมื่อคลิกเมาส์ซ้าย
        {
            anim.SetBool("IsFire", true);
        }

            anim.SetBool("IsWalking", isWalking);
    }
    void Turning(float h, float v)//หมุนตัวไปตามกล้อง
    {
        if (h != 0 || v != 0)//เมื่อกดเดินเท่านั้น
        {
            movement = Camera.main.transform.forward;
            movement.y = 0;
            movement.Normalize();
            Quaternion newDirection = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
          
        }
    
       
    }

   
}
