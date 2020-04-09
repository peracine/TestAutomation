using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TAContract.Tests
{
    public class AlphabeticalOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var result = testCases.ToList();
            result.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.TestMethod.Method.Name, b.TestMethod.Method.Name));
            return result;
        }
    }
}
