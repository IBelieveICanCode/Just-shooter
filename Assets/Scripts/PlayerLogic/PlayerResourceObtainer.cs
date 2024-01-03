using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerResourceObtainer : IResourceObtainable
    {
        private IHealthOperatorable _heathUser;
        private IEnergyOperatorable _energyUser;

        public PlayerResourceObtainer(IHealthOperatorable healthUser, IEnergyOperatorable energyUser)
        {
            _heathUser = healthUser;
            _energyUser = energyUser;

            EventManager.GetEvent<PassResourceToPlayerEvent>().StartListening(ObtainResource);
        }

        public void ObtainResource(ResourceType type, float amount)
        {
            switch (type)
            {
                case ResourceType.Energy:
                {
                    _energyUser.AddEnergy(amount);
                    break;
                }
                case ResourceType.Health:
                {
                    _heathUser.AddHealth(amount);
                    break;
                }
            }
        }
    }
}
