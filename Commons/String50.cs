using System;
using static System.String;

namespace Commons
{
    [Serializable]
    public struct String50
    {
        public bool IsValid => Value.Length > 0 && Value.Length <= 50 && (!IsNullOrEmpty(Value) || !IsNullOrWhiteSpace(Value));
        public string Value { get; private set; }

        public String50(string value)
        {
            Value = value == null ? Empty : value.Trim();
        }
    }
}