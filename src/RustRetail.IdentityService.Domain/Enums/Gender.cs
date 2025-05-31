using RustRetail.SharedKernel.Domain.Enums;

namespace RustRetail.IdentityService.Domain.Enums
{
    public class Gender : Enumeration
    {
        public static readonly Gender Male = new(nameof(Male), 0);
        public static readonly Gender Female = new(nameof(Female), 1);
        public static readonly Gender Other = new(nameof(Other), 2);

        private Gender(string name, int value) : base(name, value) { }
    }
}
