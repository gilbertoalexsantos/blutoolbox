using System.Linq;

namespace Bludk
{
    public class CompositeCancelToken : ICancelToken
    {
        private readonly ICancelToken[] _tokens;

        public bool IsCancelled => _tokens.Any(token => token.IsCancelled);

        public CompositeCancelToken(params ICancelToken[] tokens)
        {
            _tokens = tokens;
        }
    }
}