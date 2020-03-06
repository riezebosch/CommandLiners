namespace CommandLiners.Options
{
    public class Option
    {
        public string Name { get; }
        public string Value { get; protected set; } = true.ToString();

        public Option(string name)
        {
            Name = name;
        }

        public override string ToString() => $"{Name}: {Value}";
    }
}