namespace PosixCommandline.Options
{
    public class Option
    {
        public string Name { get; }
        public Option(string name)
        {
            Name = name;
        }

        public override string ToString() => true.ToString();
    }
}