using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr
{
    [Serializable]
    public class NameValue<N, V>
    {
        public NameValue(N name, V value)
        {
            Name = name;
            Value = value;
        }
        public N Name { get; set; }
        public V Value { get; set; }
        public string? Description { get; set; }
    }

    [Serializable]
    public class NameValue : NameValue<string, string>
    {
        public NameValue(string name, string value)
            : base(name, value)
        {
        }
    }
}
