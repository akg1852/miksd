using System;
using System.Collections.Generic;

namespace Mix.Models
{
    public class Garnish
    {
        public Garnishes Id;
        public string Name;

        public Garnish() { }
        public Garnish(Garnishes id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    [Flags]
    public enum Garnishes : long
    {
        None = 0,
        OrangeSlice = 1,
        LemonSlice = 2,
        Olive = 4,
        Cherry = 8,
        OrangeTwist = 16,
        LemonTwist = 32,
        LimeWedge = 64,
        LimeSlice = 128,
    }
}