using Fuchu;

namespace Sharper.C.Tests
{

using static ChainTestsModule;

public sealed class Program
{
    public int Main(string[] args)
    =>
        ChainTests.Run();
}

}