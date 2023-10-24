//namespace ToDoApi.interceptor
//{
//    public class jwt : DelegatingHandler
//    {
//        private readonly string token;

//        public JwtTokenInterceptor(string token)
//        {
//            this.token = token;
//        }

//        protected override async Task<HttpResponseMessage> SendAsync(
//            HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            return await base.SendAsync(request, cancellationToken);
//        }
//    }
//}
