using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Photon.Pun;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public PlayerController player;
        public PhotonView view;

        public override void Execute()
        {
            if (player.view.IsMine)
            {
                // var player = model.player;
                player.collider2d.enabled = true;
                player.controlEnabled = false;
                if (player.audioSource && player.respawnAudio)
                    player.audioSource.PlayOneShot(player.respawnAudio);
                player.health.Increment();
                player.gameController.SetParameters(player);
                player.Teleport(model.spawnPoint.transform.position);
                player.jumpState = PlayerController.JumpState.Grounded;
                player.animator.SetBool("dead", false);
                // model.virtualCamera.m_Follow = player.transform;
                // model.virtualCamera.m_LookAt = player.transform;
                Simulation.Schedule<EnablePlayerInput>(2f);
            }
        }
    }
}