namespace ContentApiExample
{
    class AuthenticationContent
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
    }
    class AuthenticationResponse
    {
        public Claim[] Claims { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
    }
    class Claim
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public AccessDescriptor[] AccessDescriptors { get; set; }
    }
    class AccessDescriptor
    {
        public string Area { get; set; }
        public string ClaimAccess { get; set; }
        public int ClaimAccessRaw { get; set; }
    }
}
