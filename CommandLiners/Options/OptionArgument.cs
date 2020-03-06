namespace CommandLiners.Options
{
    public class OptionArgument : Option
    {
        public OptionArgument(string name, string value) : base(name) => Value = value;
    }
}