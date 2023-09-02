using AutoMapper;

namespace AccountService.Application.Mappings
{
    public class MapperConfigurationProfile {
        public static Profile AccountMappingProfile() {
            return new AccountMappingProfile();
        }
        public static Profile FriendRequestMappingProfile() {
            return new FriendRequestMappingProfile();
        }
    }
}
