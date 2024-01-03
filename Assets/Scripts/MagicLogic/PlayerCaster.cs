using System.Collections;
using System.Collections.Generic;
using TestShooter.InputSystem;
using TestShooter.Spells;
using UnityEngine;

namespace TestShooter.Player
{
    public class PlayerCaster: ISpellCasterable 
    {
        private ISpellable _spell; //TODO list of spells if needed
        private IEnergyOperatorable _energyUseLogic;
        private IInputable _inputSystem;

        public PlayerCaster(IEnergyOperatorable energyUseLogic, IInputable inputSystem)
        {
            _energyUseLogic = energyUseLogic;
            _inputSystem = inputSystem;
            _inputSystem.OnEnergyUseDone += CastSpell;
            _spell = new KillAllEnemiesSpell();
        }

        public void CastSpell()
        {
            if (_energyUseLogic.IsMaxEnergy)
            {
                _energyUseLogic.SubstractEnergy(float.MaxValue);
                _spell.Use();
            }
        }
    }
}
