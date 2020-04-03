namespace EnigmaLibrary.Models.Classic.Components
{
    using EnigmaLibrary.Models.Enums;
    using EnigmaLibrary.Models.Interfaces.Components;

    public class ComponentFactory : IComponentFactory
    {
        public IPlugboard CreatePlugboard()
        {
            return new Plugboard();
        }

        public IReflector CreateReflector()
        {
            return new Reflector();
        }

        public IRotor CreateRotor(RotorSlot slot)
        {
            return new Rotor(slot);
        }
    }
}
