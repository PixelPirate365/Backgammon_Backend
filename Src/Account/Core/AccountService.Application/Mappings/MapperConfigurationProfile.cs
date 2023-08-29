using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Mappings {
    public class MapperConfigurationProfile {
        public static Profile AccountMappingProfile() {
            return new AccountMappingProfile();
        }
    }
}
