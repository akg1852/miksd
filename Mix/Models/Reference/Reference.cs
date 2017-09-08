using System.Collections.Generic;

using V = Mix.Models.Vessel;
using P = Mix.Models.PrepMethod;
using G = Mix.Models.Garnish;
using S = Mix.Models.SpecialPrep;
using v = Mix.Models.Vessels;
using p = Mix.Models.PrepMethods;
using g = Mix.Models.Garnishes;
using s = Mix.Models.SpecialPreps;

namespace Mix.Models
{
    public static partial class Reference
    {
        public static List<Vessel> AllVessels = new List<Vessel>
        {
            new V(v.Cocktail, "a Cocktail glass"),
            new V(v.Rocks, "an Old Fashioned glass"),
            new V(v.Highball, "a Highball/Collins glass"),
            new V(v.Shot, "a Shot glass"),
            new V(v.Flute, "a Champagne Flute"),
        };

        public static List<PrepMethod> AllPrepMethods = new List<PrepMethod>
        {
            new P(p.Shake, "Shake"),
            new P(p.Stir, "Stir"),
            new P(p.Build, "Build"),
            new P(p.Layer, "Layer"),
        };

        public static List<Garnish> AllGarnishes = new List<Garnish>
        {
            new G(g.Olive, "an olive"),
            new G(g.Cherry, "a maraschino cherry"),
            new G(g.OrangeSlice, "an orange slice"),
            new G(g.LemonSlice, "a lemon slice"),
            new G(g.LimeSlice, "a lime slice"),
            new G(g.LimeWedge, "a lime wedge"),
            new G(g.OrangeTwist, "an orange twist"),
            new G(g.LemonTwist, "a lemon twist"),
        };

        public static List<SpecialPrep> AllSpecialPreps = new List<SpecialPrep>
        {
            new S(s.None, ""),
            new S(s.Rinse, "rinse"),
            new S(s.Muddle, "muddle"),
            new S(s.Float, "float"),
        };
    }
}