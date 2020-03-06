namespace CommandLiners.Options
{
    public class OptionArgument : Option
    {
        private readonly string _value;
        
        public OptionArgument(string name, string value) : base(name) => _value = value;

        public override string ToString() => _value;
    }
}