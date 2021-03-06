﻿using System;
using System.Collections.Generic;
using GridPatternLibrary.Helpers.Concrete;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class PatternParserTests
    {
        private static readonly object[] removeSpacesData =
            {
                new object[] {"Hello to Billy", "HellotoBilly"},
                new object[] {"", ""},
                new object[] {"123", "123"}
            };

        [Test, TestCaseSource("removeSpacesData")]
        public void RemoveSpacesTest(string input, string output)
        {
            var patternParser = new PatternParser();

            var result = patternParser.RemoveSpaces(input);

            Assert.That(result, Is.EqualTo(output));
        }

        private static readonly object[] getRowsData =
            {
                new object[] {"Hello\r\nto\r\nBilly", new List<string> {"Hello", "to", "Billy"}},
                new object[] {"", new List<string>()},
                new object[] {"Hello\r\n\r\nworld", new List<string> {"Hello", "world"}},
                new object[] {"123", new List<string> {"123"}},
            };

        [Test, TestCaseSource("getRowsData")]
        public void GetRowsTest(string input, List<string> output)
        {
            var patternParser = new PatternParser();

            var result = patternParser.GetRows(input);

            Assert.That(result, Is.EqualTo(output));
        }

        private static readonly object[] removeTitleRowData =
            {
                new object[] {new List<string> {"Hello", "to", "Billy"}, new List<string> {"to", "Billy"}},
                new object[] {new List<string> {""}, new List<string>()},
                new object[] {new List<string> {"123"}, new List<string>()}
            };

        [Test, TestCaseSource("removeTitleRowData")]
        public void RemoveTitleRowTest(List<string> input, List<string> output)
        {
            var patternParser = new PatternParser();

            var result = patternParser.RemoveTitleRow(input);

            Assert.That(result, Is.EqualTo(output));
        }

        private static readonly object[] removeTitleRowWithExceptionData =
            {
                new object[] {new List<string> (), new List<string>()}
            };

        [Test, TestCaseSource("removeTitleRowWithExceptionData"), ExpectedException(typeof(ArgumentException))]
        public void RemoveTitleRowWithExceptionTest(List<string> input, List<string> output)
        {
            var patternParser = new PatternParser();

            var result = patternParser.RemoveTitleRow(input);

            Assert.That(result, Is.EqualTo(output));
        }

        private static readonly object[] getColumnsData =
            {
                new object[]
                    {
                        new List<string> {"Hello,Billy", "to,you", "John,Doe"},
                        new List<string[]>
                            {
                                new[] {"Hello", "Billy"},
                                new[] {"to", "you"},
                                new[] {"John", "Doe"}
                            }
                    },
                new object[]
                    {
                        new List<string> {""},
                        new List<string[]> {new[] {""}}
                    },
                new object[]
                    {
                        new List<string> {"123"},
                        new List<string[]> {new[] {"123"}}
                    }
            };

        [Test, TestCaseSource("getColumnsData")]
        public void GetColumnsTest(List<string> input, List<string[]> output)
        {
            var patternParser = new PatternParser();

            var result = patternParser.GetColumns(input);

            Assert.That(result, Is.EqualTo(output));
        }

        private static readonly object[] removeTitleColumnData =
            {
                new object[]
                    {
                        new List<List<string>>
                            {
                                new List<string> {"Hello", "Billy"},
                                new List<string> {"to", "you"},
                                new List<string> {"John", "Doe"}
                            },
                        new List<List<string>>
                            {
                                new List<string> {"Billy"},
                                new List<string> {"you"},
                                new List<string> {"Doe"}
                            }
                    },
                new object[]
                    {
                        new List<List<string>> {new List<string> {""}},
                        new List<List<string>> {new List<string>()}

                    },
                new object[]
                    {
                        new List<List<string>> {new List<string> {"123"}},
                        new List<List<string>> {new List<string>()}
                    }
            };

        [Test, TestCaseSource("removeTitleColumnData")]
        public void RemoveTitleColumnTest(List<List<string>> input, List<List<string>> output)
        {
            var patternParser = new PatternParser();

            var result = patternParser.RemoveTitleColumn(input);

            Assert.That(result, Is.EqualTo(output));
        }

        private const string Pattern = @",Gate0,Leg1,Gate1,Leg2,Gate2,Leg3,Gate3,Leg4,Gate4
A,B1; S1,10,BX1; SX1,20,N,10,N,20,N
B,,,,,,,,-20,N
C,,,,,,-10,N,30,N
D,,,,,,,,-30,N
E,,,,-20,S2,20,B3; SX2,20,BX3
F,,,,,,,,-20,BX3
G,,,,,,-20,SX2,10,N
H,,,,,,,,-10,N
I,,-10,B2; BX1,30,SX1,20,N,10,BX2
J,,,,,,,,-10,BX2
K,,,,,,-20,N,20,BX2
L,,,,,,,,-20,BX2
M,,,,-30,N,10,SX1,30,BX2
N,,,,,,,,-30,BX2
O,,,,,,-10,N,20,BX2; SX1
P,,,,,,,,-20,BX2; SX1";

        [Test]
        public void ParseOriginalDataTest()
        {
            var patternParser = new PatternParser();

            var columns = patternParser.Parse(Pattern);

            Assert.That(columns.First().First(), Is.EqualTo("B1;S1"));
            Assert.That(columns.First().Last(), Is.EqualTo("N"));
            Assert.That(columns.Last().First(), Is.EqualTo(""));
            Assert.That(columns.Last().Last(), Is.EqualTo("BX2;SX1"));
        }        
    }
}