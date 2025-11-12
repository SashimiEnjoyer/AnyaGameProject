using UnityEngine;

public class PlayerAttack : CharacterState
{
    public PlayerAttack(PlayerController player) : base(player)
    {
    }


    float timer = 0f;
    GameObject slashEffectObject;
    ParticleSystem slashEffect;
    ParticleSystemRenderer slashEffectRenderer;

    public override void EnterState()
    {
        if (character.PlayerTouchGround(Vector2.down))
        {
            if (Mathf.Abs(character.horizontalInput) > 0)
            {
                character.SetAnimatorState(character.anim, "Running_Attack_Ground");
            }
            else
            {
                character.SetAnimatorState(character.anim, "Attack_Ground");
            }
        }
        else
        {
            {
                character.SetAnimatorState(character.anim, "Attack_Ground");
            }
        }

        character.attackAudio.PlayOneShot(character.attackClip[Random.Range(0, character.attackClip.Length)]);

        if (slashEffectObject == null)
        {
            slashEffectObject = GameObject.Instantiate(character.attackSlashEffectPrefab, character.transform.position + (Vector3.right * (character.isFacingRight ? 1 : -1)), Quaternion.identity);

            slashEffectObject.transform.Rotate(Vector3.up * (character.isFacingRight ? 0 : 180));
            slashEffectObject.transform.parent = character.transform;
        }

        slashEffectObject.SetActive(true);

        if (slashEffect == null)
            slashEffect = slashEffectObject.GetComponent<ParticleSystem>();
            //slashEffect.Play();
        

        if(slashEffectRenderer == null)
            slashEffectRenderer = slashEffect.GetComponent<ParticleSystemRenderer>();

        slashEffectRenderer.flip = Vector3.right * (character.isFacingRight ? 0 : 1);
        slashEffect.Emit(1);

        timer = Time.time + slashEffect.main.duration;
        
        
    }

    public override void Tick()
    {
        if (Time.time < timer)
            return;
        else 
            slashEffectObject.SetActive(false);
        
        if(Time.time >= timer + 0.45f)
            character.SetState(character.playerLocomotionState);
    }

    public override void ExitState()
    {
        slashEffectObject.SetActive(false);
        character.EmptyEnemyList();
        timer = 0;
    }
}
