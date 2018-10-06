using System;
using System.Collections.Generic;
using Anagramalist.Implementations;
using NUnit.Framework;

[TestFixture]
class TestResultsTests
{
    [Test]
    public void Test()
    {
        var input = new List<Tester.SingleTestResult>
        {
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(42),}
        };
        var result = Tester.TestResult.Create(input);
        Assert.AreEqual(42, result.MedianTimeSeconds);
    }

    [Test]
    public void Test2()
    {
        var input = new List<Tester.SingleTestResult>
        {
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(10),},
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(20),}
        };
        var result = Tester.TestResult.Create(input);
        Assert.AreEqual(15, result.MedianTimeSeconds);
    }

    [Test]
    public void Test3()
    {
        var input = new List<Tester.SingleTestResult>
        {
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(0),},
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(1),},
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(2),}
        };
        var result = Tester.TestResult.Create(input);
        Assert.AreEqual(1, result.MedianTimeSeconds);
    }

    [Test]
    public void Test4()
    {
        var input = new List<Tester.SingleTestResult>
        {
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(0),},
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(1),},
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(2),},
            new Tester.SingleTestResult() {Time = TimeSpan.FromSeconds(3),}
        };
        var result = Tester.TestResult.Create(input);
        Assert.AreEqual(1.5, result.MedianTimeSeconds);
    }
}
