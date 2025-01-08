namespace Share.Core.Test;

public class ResultTests
{
    [Fact]
    public void Success()
    {
        var value = new ResultValue { Value = 1 };
        var result = new Result<ResultValue>(value);
        Assert.True(result.Successful);
    }

    [Fact]
    public void Failure()
    {
        var error = new Error { Message = "Error", Context = "Error" };
        var result = new Result<ResultValue>(error);
        Assert.False(result.Successful);
    }

    [Fact]
    public void SuccessAndFailure()
    {
        var value = new ResultValue { Value = 1 };
        var error = new Error { Message = "Error", Context = "Error" };
        var result = new Result<ResultValue>(value, error);
        Assert.False(result.Successful);
    }

    [Fact]
    public void Nullable()
    {
        var result = new Result<ResultValue>();
        Assert.True(result.Successful);
    }
}