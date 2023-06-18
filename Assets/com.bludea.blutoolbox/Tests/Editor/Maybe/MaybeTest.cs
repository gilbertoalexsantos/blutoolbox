using System;
using NUnit.Framework;

namespace BluToolbox.Tests
{
  public class MaybeTest
  {
    [Test]
    public void TestUnpack()
    {
      Maybe<int> fooM = Maybe.Some(5);
      Assert.AreEqual(5, fooM.Value);

      fooM = 5.Some();
      Assert.AreEqual(5, fooM.Value);

      fooM = fooM.Some();
      Assert.AreEqual(5, fooM.Value);
    }

    [Test]
    public void TestEquality()
    {
      Maybe<int> fooM = Maybe.Some(5);
      Maybe<int> barM = Maybe.Some(5);
      Assert.IsTrue(fooM == barM);

      barM = Maybe.Some(15);
      Assert.IsTrue(fooM != barM);
    }

    [Test]
    public void TestComparison()
    {
      Maybe<int> m4 = Maybe.Some(4);
      Maybe<int> otherM4 = Maybe.Some(4);
      Maybe<int> m5 = Maybe.Some(5);
      Maybe<int> m6 = Maybe.Some(6);

      Assert.IsTrue(m5 > m4 && m6 > m5);
      Assert.IsTrue(m5 >= m4 && m6 >= m5);
      Assert.IsTrue(m4 >= otherM4);

      Assert.IsTrue(m4 < m5 && m5 < m6);
      Assert.IsTrue(m4 <= m5 && m5 <= m6);
      Assert.IsTrue(m4 <= otherM4);

      Maybe<int> none = Maybe.None<int>();
      Assert.IsFalse(m4 == none);
      Assert.IsTrue(m4 != none);

      none = 5.None();
      Assert.IsFalse(m4 == none);
      Assert.IsTrue(m4 != none);

      none = m4.None();
      Assert.IsFalse(m4 == none);
      Assert.IsTrue(m4 != none);

      Maybe<int> none2 = 6.None();
      Assert.IsTrue(none == none2);
      Assert.IsFalse(none != none2);
    }

    [Test]
    public void ThrowExceptionOnAccessWithNoValue()
    {
      Maybe<int> m = Maybe.None<int>();

      Assert.Throws<Exception>(() =>
      {
        Assert.Equals(0, m.Value);
      });
    }
  }
}