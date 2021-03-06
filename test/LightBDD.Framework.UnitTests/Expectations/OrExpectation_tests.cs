﻿using LightBDD.Framework.Expectations;
using LightBDD.Framework.Formatting.Values;
using NUnit.Framework;

namespace LightBDD.Framework.UnitTests.Expectations
{
    [TestFixture]
    public class OrExpectation_tests
    {
        [Test]
        public void It_should_pass_validation()
        {
            var expectation = Expect.To.BeLessThan(3).Or(x => x.BeGreaterThan(5));
            Assert.True(expectation.Verify(2, ValueFormattingServices.Current));
            Assert.True(expectation.Verify(6, ValueFormattingServices.Current));
        }

        [Test]
        public void It_should_pass_complex_validation()
        {
            var expectation = Expect.To.BeEmpty().Or(x => x.BeNull()).Or(x => x.Contain('c'));
            Assert.True(expectation.Verify("abc", ValueFormattingServices.Current));
            Assert.True(expectation.Verify("", ValueFormattingServices.Current));
            Assert.True(expectation.Verify(null, ValueFormattingServices.Current));
        }

        [Test]
        public void It_should_pass_negated_validation()
        {
            var expectation = Expect.To.Not.BeGreaterThan(3).Or(x => x.Not.BeLessThan(5));
            Assert.True(expectation.Verify(2, ValueFormattingServices.Current));
            Assert.True(expectation.Verify(6, ValueFormattingServices.Current));
        }

        [Test]
        [TestCase(4, "expected: (less than '3' or greater than '5'), but got: '4'\n\texpected: less than '3', but got: '4'\n\texpected: greater than '5', but got: '4'")]
        public void It_should_fail_validation(int value, string expectedMessage)
        {
            var expectation = Expect.To.BeLessThan(3).Or(x => x.BeGreaterThan(5));
            var result = expectation.Verify(value, ValueFormattingServices.Current);
            Assert.False(result);
            Assert.That(result.Message.Replace("\r", ""), Is.EqualTo(expectedMessage.Replace("\r", "")));
        }

        [Test]
        [TestCase(4, "expected: (not greater than '3' or not less than '5'), but got: '4'\n\texpected: not greater than '3', but it was\n\texpected: not less than '5', but it was")]
        public void It_should_fail_negated_validation(int value, string expectedMessage)
        {
            var expectation = Expect.To.Not.BeGreaterThan(3).Or(x => x.Not.BeLessThan(5));
            var result = expectation.Verify(value, ValueFormattingServices.Current);
            Assert.False(result);
            Assert.That(result.Message.Replace("\r", ""), Is.EqualTo(expectedMessage.Replace("\r", "")));
        }

        [Test]
        public void It_should_format()
        {
            Assert.That(Expect.To.BeGreaterThan(3).Or(x => x.Not.BeLessThan(5)).ToString(), Is.EqualTo("(greater than '3' or not less than '5')"));
        }
    }
}