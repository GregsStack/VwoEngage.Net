namespace GregsStack.PushCrew.Net.Api
{
    using System;

    public class Segment
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Segment(string id, string name)
        {
            this.Id = id;
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return $"Segment({this.Id}, {this.Name})";
        }
    }
}
