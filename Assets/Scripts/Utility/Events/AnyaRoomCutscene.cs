using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnyaRoomCutscene : MonoBehaviour
{
    [Header("Animators")]
    public Animator liveanim;
    public Animator twtanim;
    public Animator dcanim;
    public Animator playeranim;

    [Header("Game Objects")]
    public GameObject livestream;
    public GameObject call;
    public GameObject twitter;
    public GameObject discord;
    public GameObject player;
    public GameObject location1;
    public GameObject location2;
    public SpriteRenderer monitorOn;
    public SpriteRenderer monitorOff;

    [Header("Dialogue")]
    public DialogueController dialogue1;
    public DialogueController dialogue2;
    public DialogueController dialogue3;
    public DialogueController dialogue4;
    public DialogueController dialogue5;
    public DialogueController dialogue6;
    public DialogueController dialogue7;
    public DialogueController dialogue8;
    public DialogueController dialogue9;
    public DialogueController dialogue10;
    public DialogueController dialogue11;

    [Header("Timing")]
    public float tdialogue1;
    public float tdialogue2;
    public float tanimation1;
    public float tdialogue3;
    public float tdialogue4;
    public float tdialogue5;
    public float tdialogue6;
    public float tfadetonextday;
    public float tdialogue7;
    public float tanimation2;
    public float tanimation3;
    public float tdialogue8;
    public float tanimation4;
    public float tanimation5;
    public float tdialogue9;
    public float tanimation6;
    public float tdialogue10;
    public float tdialogue11;
    public float time;
    private bool d1 = false;
    private bool d2 = false;
    private bool d3 = false;
    private bool d4 = false;
    private bool d5 = false;
    private bool d6 = false;
    private bool d7 = false;
    private bool d8 = false;
    private bool d9 = false;
    private bool d10 = false;
    private bool d11 = false;

    [Header("Transition")]
    public Image transistionImage;
    public Image transistionImage1;

    void Awake()
    {
        player.GetComponent<PlayerAnimations>().PlayAnimationIdle();
        liveanim.Play("Base Layer.Live1", 0, 0f);
    }


    void Update()
    {
        if (time >= 1f) //start, livestream opens
        {
            if (time <= 1.1f)
            livestream.transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.5f);
        }

        if (time >= tdialogue1) // dialogue 1
        dialogue1.ExecuteInteractable();

        if (time >= tdialogue2) // dialogue 2
        {
            dialogue2.ExecuteInteractable();
            liveanim.Play("Base Layer.LiveReine1", 0, 0f);
        }

        if (time >= tanimation1) // player animation 1
        {
            if (time <= tanimation1 + 0.1f)
            {
                player.GetComponent<PlayerAnimations>().PlayAnimationAttack();
                liveanim.Play("Base Layer.LiveSmack", 0, 0f);
            }

            if (time >= tanimation1 + 0.6f) // player back to idle
            {
                if (time <= tanimation1 + 0.7f)
                {
                    player.GetComponent<PlayerAnimations>().PlayAnimationIdle();
                }
            }
        }

        if (time >= tdialogue3) //dialogue 3
        {
            dialogue3.ExecuteInteractable();
            liveanim.Play("Base Layer.LiveReine2", 0, 0f);
        }

        if (time >= tdialogue4) //player dilogue 4
        {
            dialogue4.ExecuteInteractable();
            
            if ( dialogue4.dialogueIndex == 0)
            liveanim.Play("Base Layer.LiveOllie1", 0, 0f);

            if ( dialogue4.dialogueIndex >= 1)
            liveanim.Play("Base Layer.LiveOllie2", 0, 0f);

            if ( dialogue4.dialogueIndex >= 4)
            liveanim.Play("Base Layer.LiveOllie3", 0, 0f);
        }

        if (time >= tdialogue4 + 0.1f) // livestream stiwtch to normal/ending
        {
            liveanim.Play("Base Layer.Live1", 0, 0f);
        }

        if (time >= tdialogue5) //dialogue 5
        {
            dialogue5.ExecuteInteractable();

        }

        if (time >= tdialogue5 + 1f) //livestream ends
        {
            if (time <= tdialogue5 + 1.1f)
            livestream.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        }

        if (time >= tdialogue5 + 2f) //call with reine and ollie opens
        {
            if (time <= tdialogue5 + 2.1f)
            call.transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.5f);
        }

        if (time >= tdialogue6) //dialogue 6
        {
            dialogue6.ExecuteInteractable();
        }

        if (time >= tdialogue6 + 0.5f) //call ends
        {
            if (time <= tdialogue6 + 1f)
            call.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        }

        if (time >= tfadetonextday) //day ends
        {
            if (time <= tfadetonextday + 0.2f)
            transistionImage.DOFade(2f, 4f);
        }

        if (time >= tfadetonextday + 2) //move to bed, location 1
        {
            if (time <= tfadetonextday + 2.5f)
            {
                player.transform.position = location1.transform.position;
                player.transform.localScale = new Vector3(1,1,1);
                player.GetComponent<PlayerController>().isFacingRight = true;
                monitorOff.enabled = true;
                monitorOn.enabled = false;
            }
        }

        if (time >= tfadetonextday + 4) //day start
        {
            if (time <= tfadetonextday + 4.5f)
            transistionImage.DOFade(-2, 4f);
        }

        if (time >= tdialogue7) //dialogue 7
        {
            dialogue7.ExecuteInteractable();
        }

        if (time >= tanimation2) //move to pc
        {
            if (time <= tanimation2 + 0.2f)
            {
                player.GetComponent<PlayerController>().horizontalInput = 1f;
                playeranim.SetFloat("Speed",1);
            }
        }

        if (player.transform.position.x >= location2.transform.position.x) //stop moving, idle
        {
            player.GetComponent<PlayerController>().horizontalInput = 0;
            playeranim.SetFloat("Speed",0);

        }

        if (time >= tanimation3) //turn on pc, open twitter
        {
            if (time <= tanimation3 + 0.3f)
            {
                monitorOff.enabled = false;
                monitorOn.enabled = true;
            }

            if (time >= tanimation3 + 0.3f)
            {
                if (time <= tanimation3 + 0.6f)
                {
                    twtanim.Play("Base Layer.Twitter1", 0, 0f);
                    twitter.transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.5f);
                }
            }
        }

        if (time >= tdialogue8) //dialogue 8
        {
            dialogue8.ExecuteInteractable();
        }
        
        if (time >= tanimation4) //anya tweeted
        {
            if (time <= tanimation4 + 0.2f)
            {
                twtanim.Play("Base Layer.Twitter2", 0, 0f);
            }
        }

        if (time >= tanimation5) //tweet responds
        {
            if (time <= tanimation5 + 0.2f)
            {
                twtanim.Play("Base Layer.Twitter3", 0, 0f);
            }
        }

        if (time >= tdialogue9) //dialogue 9
        {
            dialogue9.ExecuteInteractable();
        }

        if (time >= tdialogue9 + 0.5f) //closes twitter
        {
            if (time <= tdialogue9 + 1f)
            {
                twitter.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
            }
        }

        if (time >= tanimation6) //open discord, tanigox
        {
            if (time <= tanimation6 + 0.3f)
            {
                dcanim.Play("Base Layer.Discord", 0, 0f);
                discord.transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.5f);
            }
        }

        if (time >= tdialogue10) //typing discord
        {
            if (time <= tdialogue10 + 0.5f)
            {
                dcanim.Play("Base Layer.Discord1", 0, 0f);
                dialogue10.ExecuteInteractable();
            }
        }

        if (time >= tdialogue11) //yagoo send link
        {
            if (time <= tdialogue11 + 0.5f)
            {
                dcanim.Play("Base Layer.Discord2", 0, 0f);
                dialogue11.ExecuteInteractable();
            }
        }

        if (time >= tdialogue11 + 0.3f) //bright shine
        {
            if (time <= tdialogue11 + 0.6f)
            {
                transistionImage1.DOFade(2f, 2f);
            }
        }

        if (time >= tdialogue11 + 2.6f) //bright shine
        {
            if (time <= tdialogue11 + 2.9f)
            {
                transistionImage.DOFade(2f, 0.1f);
            }
        }

        if (time >= tdialogue11 + 3.3f) //bright shine
        {
            if (time <= tdialogue11 + 3.6f)
            {
                transistionImage1.DOFade(-2f, 2f);
            }
        }
     
    }

    void FixedUpdate()
    {
        if (d1 == false && d2 == false && d3 == false && d4 == false && d5 == false && d6 == false && d7 == false && d8 == false && d9 == false && d10 == false && d11 == false)
        time += Time.deltaTime;

        d1 = dialogue1.isActive;
        d2 = dialogue2.isActive;
        d3 = dialogue3.isActive;
        d4 = dialogue4.isActive;
        d5 = dialogue5.isActive;
        d6 = dialogue6.isActive;
        d7 = dialogue7.isActive;
        d8 = dialogue8.isActive;
        d9 = dialogue9.isActive;
        d10 = dialogue10.isActive;
        d11 = dialogue11.isActive;
    }
}
