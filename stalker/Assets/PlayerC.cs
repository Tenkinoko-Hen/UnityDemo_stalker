using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerC : MonoBehaviour
{
    public static PlayerC instance;
    public int playerHp=100;
    public bool WinKey = false;
    public bool PlayDie = false;

    public emMoveType mMoveType;
    public emMathf_SubType mMathf_SubType;
    public emCharacterController_SubType mCharacterController_SubType;
    public emRigidbody_SubType mRigidbodySubType;

    public float speed = 2f;
    public float mRotateSpeed = 2f;

    public CharacterController mcharacterController;
    public Rigidbody rig;

    public Animator animator;

    public Text HPUI; 
    public enum emMoveType
    {
          Mathf,
          CharacterController,
          Ridgidbody
    }

    public enum emMathf_SubType
    {
        Mathf,
        Translate,
        MoveTowards
    }

    public enum emCharacterController_SubType 
    {
        Move,
        SimpleMove,
    }

    public enum emRigidbody_SubType
    {
        Vcelocity,
        AddForce,
        MovePosition,
    }

    private void Awake()
    {
        instance = this;
       this.transform.Find("girl role").GetComponent<Animator>();
        //mMoveType = emMoveType.Mathf;

        if (mMoveType == emMoveType.CharacterController)
        {
            mcharacterController = this.transform.GetComponent<CharacterController>();
            if (mcharacterController == null)
            {
             mcharacterController=   this.transform.gameObject.AddComponent<CharacterController>();
                mcharacterController.center = new Vector3(0,0.6f,0);
                mcharacterController.radius = 0.5f;
                mcharacterController.height = 1.46f;
            }

        }else if (mMoveType == emMoveType.Ridgidbody)
        {
            rig = this.transform.GetComponent<Rigidbody>();
            if (rig == null)
            {
                rig = this.transform.gameObject.AddComponent<Rigidbody>();
                CapsuleCollider capsule= transform.gameObject.AddComponent<CapsuleCollider>();
                capsule.center = new Vector3(0, 0.5f, 0);
                capsule.radius = 0.5f;
                capsule.height = 1;
                //用并集的方式 屏蔽刚体的constraints参数
                rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
                rig.drag = 5f;
            }
        }
    }

    private void Update()
    {
        #region
        HPUI.text = "生命值:" + playerHp;
        if (playerHp <= 0)
        {
            animator.SetBool("bDie", true);
            PlayDie = true;
        }
        #endregion

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
           
            animator.SetBool("bWalk", true);
            //角色发生移动需求的变化
            //1.旋转
            //2.移动
            Vector3 dir = new Vector3(h, 0, v);

            Quaternion targetQ=  Quaternion.LookRotation(dir, Vector3.up);
            // transform.rotation = targetQ;
          
                                                   //当前的四元数，  目标的四元数，旋转的速度
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQ, Time.deltaTime * mRotateSpeed);

            switch (mMoveType) {

                case emMoveType.Mathf:
                    {
                        if (mMathf_SubType == emMathf_SubType.Mathf)
                        {
                           //数学的位移           玩家的Z轴也就是朝向的轴
                            transform.position += transform.forward * Time.deltaTime * speed;
                        }
                        else if (mMathf_SubType == emMathf_SubType.Translate)
                        {
                            //往哪个方向走多远                                            以世界坐标系为基准
                            transform.Translate(transform.forward * Time.deltaTime * speed,Space.World);
                        }
                        else if (mMathf_SubType == emMathf_SubType.MoveTowards)
                        {
                            //通过向量移动，                           当前玩家的位置，需要移动到目标点的位置(当前的位置加上移动方向的位置) ,从当前的位置移动到最新位置间隔的时间   
                            transform.position = Vector3.MoveTowards(transform.position,transform.position + transform.forward * Time.deltaTime * speed,Time.deltaTime*speed);
                        }
                    }
                    break;
                case emMoveType.CharacterController:
                    {
                        if (mCharacterController_SubType == emCharacterController_SubType.Move)
                        {
                            mcharacterController.Move(-transform.up * Time.deltaTime * speed);//一直给一个向下的移动，模拟重力
                            //向哪个方向进行移动
                            mcharacterController.Move(transform.forward * Time.deltaTime * speed);//按帧移动
                        }
                        else if (mCharacterController_SubType == emCharacterController_SubType.SimpleMove)
                        {
                            //向哪个方向进行移动
                            mcharacterController.SimpleMove(transform.forward * Time.deltaTime * speed); //按秒移动 有模拟重力的效果
                        }
                     
                    } 
                    break;
                case emMoveType.Ridgidbody:
                    {
                        if (mRigidbodySubType == emRigidbody_SubType.AddForce)
                        {
                            //给他一个方向的力    按秒刷新
                            rig.AddForce(transform.forward * speed);

                        }
                        else if(mRigidbodySubType == emRigidbody_SubType.Vcelocity)
                        {
                            

                            //给他一个方向的位移量， 按秒进行刷新
                            rig.velocity = transform.forward  * speed;
                        }
                        else if(mRigidbodySubType == emRigidbody_SubType.MovePosition)
                        {
                            rig.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
                        }
                    }
                    break;
            }
            if (!this.transform.GetComponent<AudioSource>().isPlaying)
                this.transform.GetComponent<AudioSource>().Play();
        }
        else
        {
            animator.SetBool("bWalk", false);
           

        }
    }
}
