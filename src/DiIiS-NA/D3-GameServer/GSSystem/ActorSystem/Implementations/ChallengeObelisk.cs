﻿using DiIiS_NA.D3_GameServer.Core.Types.SNO;
using DiIiS_NA.GameServer.Core.Types.TagMap;
using DiIiS_NA.GameServer.GSSystem.MapSystem;
using DiIiS_NA.GameServer.GSSystem.PlayerSystem;
using DiIiS_NA.GameServer.GSSystem.TickerSystem;
using DiIiS_NA.GameServer.MessageSystem;
using DiIiS_NA.GameServer.MessageSystem.Message.Definitions.ACD;
using DiIiS_NA.GameServer.MessageSystem.Message.Definitions.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiIiS_NA.GameServer.GSSystem.ActorSystem.Implementations
{
    [HandledSNO(ActorSno._p2_weeklychallenge_obelisk /* x1_OpenWorld_LootRunObelisk_B.acr */)]
    public sealed class ChallengeObelisk : Gizmo
    {
        public ChallengeObelisk(World world, ActorSno sno, TagMap tags)
            : base(world, sno, tags)
        {
            Attributes[GameAttribute.TeamID] = 2;
            Attributes[GameAttribute.MinimapActive] = true;
            Attributes.BroadcastChangedIfRevealed();
        }

        public override void OnTargeted(Player player, TargetMessage message)
        {
            bool Activated = false;

            this.PlayAnimation(5, (AnimationSno)AnimationSet.TagMapAnimDefault[AnimationSetKeys.Opening]);
            Attributes[GameAttribute.Team_Override] = (Activated ? -1 : 2);
            Attributes[GameAttribute.Untargetable] = !Activated;
            Attributes[GameAttribute.NPC_Is_Operatable] = Activated;
            Attributes[GameAttribute.Operatable] = Activated;
            Attributes[GameAttribute.Operatable_Story_Gizmo] = Activated;
            Attributes[GameAttribute.Disabled] = !Activated;
            Attributes[GameAttribute.Immunity] = !Activated;
            Attributes.BroadcastChangedIfRevealed();
            CollFlags = 0;

            TickTimer Timeout = new SecondsTickTimer(World.Game, 3.5f);
            var Boom = Task<bool>.Factory.StartNew(() => WaitToSpawn(Timeout));
            Boom.ContinueWith(delegate
            {
                var actor = World.GetActorBySNO(ActorSno._x1_openworld_challenge_rifts_portal);
                actor.SetVisible(true);
                actor.Reveal(player);

                World.BroadcastIfRevealed(plr => new ACDCollFlagsMessage()
                {
                    ActorID = DynamicID(plr),
                    CollFlags = 0
                }, this);
            });

        }

        public override bool Reveal(Player player)
        {
             if (!base.Reveal(player))
                return false;
            if (!Attributes[GameAttribute.Operatable])
            {
                var actor = World.GetActorBySNO(ActorSno._x1_openworld_challenge_rifts_portal);
                actor.SetVisible(false);
                actor.Unreveal(player);
            }
            else
            {
                this.PlayAnimation(5, (AnimationSno)AnimationSet.TagMapAnimDefault[AnimationSetKeys.Opening]);
            }
            return true;
        }

        private bool WaitToSpawn(TickTimer timer)
        {
            while (timer.TimedOut != true)
            {

            }
            return true;
        }
    }
}
