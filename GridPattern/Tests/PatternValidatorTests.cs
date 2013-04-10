﻿using System.Collections.Generic;
using GridPatternLibrary.Helpers.Concrete;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class PatternValidatorTests
    {        
        private PatternValidator patternValidator;
        private List<List<string>> data;

        [SetUp]
        public void SetUp()
        {
            patternValidator = new PatternValidator();
        }

        [Test]
        public void IsSizeValidTest()
        {
            const string pattern = @",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,B1X; S1X,20,N,10,N,20,N
B,,,,,,,,-20,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; S2X,20,B3X
F,,,,,,,,-20,B3X
G,,,,,,-20,S2X,10,N
H,,,,,,,,-10,N
I,,-10,B2; B1X,30,S1X,20,N,10,B2X
J,,,,,,,,-10,B2X
K,,,,,,-20,N,20,B2X
L,,,,,,,,-20,B2X
M,,,,-30,N,10,S1X,30,B2X
N,,,,,,,,-30,B2X
O,,,,,,-10,N,20,B2X; S1X
P,,,,,,,,-20,B2X; S1X";

            var patternParser = new PatternParser();
            data = patternParser.Parse(pattern);

            var result = patternValidator.IsSizeValid(data);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void IsSizeValidRowCountFailTest()
        {
            const string pattern = @",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,B1X; S1X,20,N,10,N,20,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; S2X,20,B3X
F,,,,,,,,-20,B3X
G,,,,,,-20,S2X,10,N
H,,,,,,,,-10,N
I,,-10,B2; B1X,30,S1X,20,N,10,B2X
J,,,,,,,,-10,B2X
K,,,,,,-20,N,20,B2X
L,,,,,,,,-20,B2X
M,,,,-30,N,10,S1X,30,B2X
N,,,,,,,,-30,B2X
O,,,,,,-10,N,20,B2X; S1X
P,,,,,,,,-20,B2X; S1X";

            var patternParser = new PatternParser();
            data = patternParser.Parse(pattern);

            var result = patternValidator.IsSizeValid(data);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo("Pattern row count is invalid"));
        }

        private static readonly object[] isSizeValidRowLengthFailData =
            {
                new object[] {@",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,B1X; S1X,20,N,10,N,20
B,,,,,,,,-20,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; S2X,20,B3X
F,,,,,,,,-20,B3X
G,,,,,,-20,S2X,10,N
H,,,,,,,,-10,N
I,,-10,B2; B1X,30,S1X,20,N,10,B2X
J,,,,,,,,-10,B2X
K,,,,,,-20,N,20,B2X
L,,,,,,,,-20,B2X
M,,,,-30,N,10,S1X,30,B2X
N,,,,,,,,-30,B2X
O,,,,,,-10,N,20,B2X; S1X
P,,,,,,,,-20,B2X; S1X", "A"},
                new object[] {@",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,B1X; S1X,20,N,10,N,20,N
B,,,,,,,,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; S2X,20,B3X
F,,,,,,,,-20,B3X
G,,,,,,-20,S2X,10,N
H,,,,,,,,-10,N
I,,-10,B2; B1X,30,S1X,20,N,10,B2X
J,,,,,,,,-10,B2X
K,,,,,,-20,N,20,B2X
L,,,,,,,,-20,B2X
M,,,,-30,N,10,S1X,30,B2X
N,,,,,,,,-30,B2X
O,,,,,,-10,N,20,B2X; S1X
P,,,,,,,,-20,B2X; S1X", "B"},
                new object[] {@",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,B1X; S1X,20,N,10,N,20,N
B,,,,,,,,-20,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; S2X,20,B3X
F,,,,,,,,-20,B3X
G,,,,,-20,S2X,10,N
H,,,,,,,,-10,N
I,,-10,B2; B1X,30,S1X,20,N,10,B2X
J,,,,,,,,-10,B2X
K,,,,,,-20,N,20,B2X
L,,,,,,,,-20,B2X
M,,,,-30,N,10,S1X,30,B2X
N,,,,,,,,-30,B2X
O,,,,,,-10,N,20,B2X; S1X
P,,,,,,,,-20,B2X; S1X", "G"},
                new object[] {@",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,B1X; S1X,20,N,10,N,20,N
B,,,,,,,,-20,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; S2X,20,B3X
F,,,,,,,,-20,B3X
G,,,,,,-20,S2X,10,N
H,,,,,,,,-10,N
I,,-10,B2; B1X,30,S1X,20,N,10,B2X
J,,,,,,,,-10,B2X
K,,,,,,-20,N,20,B2X
L,,,,,,,,-20,B2X
M,,,,-30,N,10,S1X,30,B2X
N,,,,,,,,-30,B2X
O,,,,,,-10,N,20,B2X; S1X
P,,,,,,,-20,B2X; S1X", "P"},
            };

        [Test, TestCaseSource("isSizeValidRowLengthFailData")]
        public void IsSizeValidRowLengthFailTest(string pattern, string rowName)
        {
            var patternParser = new PatternParser();
            data = patternParser.Parse(pattern);

            var result = patternValidator.IsSizeValid(data);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo(string.Format("Pattern row: {0} is invalid", rowName)));
        }


    }
}