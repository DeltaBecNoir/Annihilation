using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Annihilation.Systems
{
    public class ExampleNPC : BaseNPC
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            NPC.width = 32;
            NPC.height = 48;
            NPC.aiStyle = -1;
            NPC.friendly = false;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 250;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.noGravity = true;
        }
        public override void InitializeState()
        {
            ChangeState(new ExampleFirstState(this), new Animation("FirstAnimation", 4, 5));
        }
    }

    public class ExampleFirstState : State
    {
        public ExampleFirstState(BaseNPC baseNPC) : base(baseNPC)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (timeInState >= 2)
            {
                baseNPC.ChangeState(new ExampleSecondState(baseNPC), new Animation("SecondAnimation", 4, 5));
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }

    public class ExampleSecondState : State
    {
        float speed = 2f;
        public ExampleSecondState(BaseNPC baseNPC) : base(baseNPC)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (timeInState >= 2)
            {
                if (Main.rand.NextBool(2))
                {
                    baseNPC.ChangeState(new ExampleFirstState(baseNPC), new Animation("FirstAnimation", 4, 5));
                }
                else
                {
                    baseNPC.ChangeState(new ExampleThirdState(baseNPC), new Animation("ThirdAnimation", 4, 5));
                }
            } 
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }

    public class ExampleThirdState : State
    {
        float speed = 2f;
        public ExampleThirdState(BaseNPC baseNPC) : base(baseNPC)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateUpdate()
        {
            base.OnStateUpdate();

            if (timeInState > 2)
            {
                baseNPC.ChangeState(new ExampleFirstState(baseNPC), new Animation("FirstAnimation", 4, 5));
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}