namespace CommonFramework.Threading;

public interface IAsyncLockerProvider
{
    IAsyncLocker CreateLocker(object key);
}