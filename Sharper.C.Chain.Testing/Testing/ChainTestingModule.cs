using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using Fuchu;

namespace Sharper.C.Testing
{

using static Data.ChainModule;
using static SystemArbitraryModule;

public static class ChainTestingModule
{
    public static Test WithoutOverflow<A>(this string label, Func<Chain<A>> f)
    =>
        label.Group
          ( Test.Case("Build", () => f())
          , Test.Case("Evaluate", () => f().Eval().Skip(1000000))
          );

    public static Arbitrary<Chain<A>> AnyChain<A>(Arbitrary<A> arbA)
    =>
        AnySeq(arbA).Convert
          ( xs => xs.Aggregate(EndChain<A>(), (s, a) => YieldLink(a, s))
          , s => s.Eval()
          );
}

}