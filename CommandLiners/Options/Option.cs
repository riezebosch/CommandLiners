namespace CommandLiners.Options
{
    public class Option
    {
        public string Name { get; set; }
        public string Value { get; protected set; } = true.ToString();

        public Option(string name)
        {
            Name = name;
        }
    }
}