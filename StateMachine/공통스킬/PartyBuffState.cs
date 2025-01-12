using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyBuffState : StateMachineBehaviour
{
    public string triggerName;
    public Joanna joanna;
    public Rei rei;
    private void Awake()
    {
        joanna = GameObject.FindWithTag("Joanna").GetComponent<Joanna>();
        rei = GameObject.FindWithTag("Rei").GetComponent<Rei>();
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        joanna.extraSkills[1].GetComponent<Animator>().SetTrigger(triggerName);
        rei.extraSkills[1].GetComponent<Animator>().SetTrigger(triggerName);
    }
}
