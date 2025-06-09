namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.True(DateTime.Now is { Year: 2025, Month: 6 });
    }
}