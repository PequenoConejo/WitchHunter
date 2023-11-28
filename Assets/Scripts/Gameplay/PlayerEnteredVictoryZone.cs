using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Gameplay
{

    /// <summary>
    /// This event is triggered when the player character enters a trigger with a VictoryZone component.
    /// </summary>
    /// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
    public class PlayerEnteredVictoryZone : Simulation.Event<PlayerEnteredVictoryZone>
    {
        public VictoryZone victoryZone;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            // model.player.animator.SetTrigger("victory");
            // model.player.controlEnabled = false;

            player.controlEnabled = false;

            // MANAGER SET IN SPAWN ENEMIES SCRIPT
            GameObject vicMan = GameObject.FindWithTag("VictoryManager");
            VictoryManagerScript vicScript = (VictoryManagerScript) vicMan.GetComponent(typeof(VictoryManagerScript));
            vicScript.PlayerEntered();


        }
    }
}