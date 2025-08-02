namespace Read.WebApi;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Books
    {
        private const string Base = $"{ApiBase}/books";

        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrSlug}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";
        public const string Register = $"{Base}/register";
        public const string Login = $"{Base}/login";
        public const string Refresh = $"{Base}/refresh";
    }
}