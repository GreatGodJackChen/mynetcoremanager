using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CJ.Domain.Uow
{
    internal class InnerUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage =
            "Did not call Complete method of a unit of work.";

        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        public void Complete()
        {
            _isCompleteCalled = true;
        }

        public Task CompleteAsync()
        {
            _isCompleteCalled = true;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (!_isCompleteCalled)
            {
                if (HasException())
                {
                    return;
                }

                throw new Exception(DidNotCallCompleteMethodExceptionMessage);
            }
        }

        private static bool HasException()
        {
            try
            {
#pragma warning disable CS0618 // 类型或成员已过时
                return Marshal.GetExceptionCode() != 0;
#pragma warning restore CS0618 // 类型或成员已过时
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}