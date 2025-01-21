using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyBuffState : StateMachineBehaviour
{
    public string triggerName;
    public Joanna joanna;
    public Rei rei;
    public bool isJoanna;//해당 버프가 조안나에게도 적용 되는지
    public bool isRei;//해당 버프가 레이에게도 적용 되는지

    private void Awake()
    {
        joanna = GameObject.FindWithTag("Joanna").GetComponent<Joanna>();
        rei = GameObject.FindWithTag("Rei").GetComponent<Rei>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isJoanna)
        {
            joanna.extraSkills[1].GetComponent<Animator>().SetTrigger(triggerName);
        }
        if(isRei)
        {
            rei.extraSkills[1].GetComponent<Animator>().SetTrigger(triggerName);
        }
    }
}
