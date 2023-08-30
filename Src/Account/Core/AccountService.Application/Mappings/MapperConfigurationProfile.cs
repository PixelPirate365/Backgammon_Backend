using AutoMapper;

namespace AccountService.Application.Mappings
{
    public class MapperConfigurationProfile {
        public static Profile AccountMappingProfile() {
            return new AccountMappingProfile();
        }
    }
}
