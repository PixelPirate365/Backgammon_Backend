using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Mappings {
    public class MapperConfigurationProfile {
        public static Profile UserMappingProfile() {
            return new UserMappingProfile();
        }
        
    }
}
