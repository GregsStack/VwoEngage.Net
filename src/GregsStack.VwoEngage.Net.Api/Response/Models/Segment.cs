namespace GregsStack.VwoEngage.Net.Api.Response.Models
{
    using System;

    public class Segment
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Segment(long id, string name)
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
