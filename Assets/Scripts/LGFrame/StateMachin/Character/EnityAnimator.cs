using System;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame
{
    public static class EnityAnimator
    {
        public  class Motion
        {
            public readonly int Attack;
            public readonly int Idle;
            public readonly int Run;
            public readonly int Spell;
            public readonly int Death;

            public Motion()
            {
                Attack = Animator.StringToHash("Attack");
                Idle   = Animator.StringToHash("Attack");
                Run    = Animator.StringToHash("Attack");
                Spell  = Animator.StringToHash("Spell01");
                Death = Animator.StringToHash("Attack");
            }
        }


    }
}
