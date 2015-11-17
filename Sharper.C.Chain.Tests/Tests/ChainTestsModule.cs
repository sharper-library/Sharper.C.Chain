using System;
using System.Linq;
using FsCheck;
using Fuchu;

namespace Sharper.C.Tests
{

using Testing.Laws;
using static Data.ChainModule;
using static Testing.ChainTestingModule;
using static Testing.PropertyModule;
using static Testing.SystemArbitraryModule;

public static class ChainTestsModule
{
    [Tests]
    public static Test ChainTests
    =>
        nameof(Chain<int>)
        .Group
          ( IsMonad(AnyInt)
          , "Iterates".WithoutOverflow(() => IterateChain(0, n => n + 1))
          );

    public static Test IsMonad<A>(Arbitrary<A> arbA)
      where A : IEquatable<A>
    =>
        MonadLaws.For
          ( LastLink
          , f => fa => fa.Map(f)
          , f => fa => fa.FlatMap(f)
          , (s1, s2) => s1.Eval().SequenceEqual(s1.Eval())
          , AnyChain(arbA)
          , AnyFunc1<A, Chain<A>>(AnyChain(arbA))
          , AnyFunc1<A, A>(arbA)
          , arbA
          );
}

}