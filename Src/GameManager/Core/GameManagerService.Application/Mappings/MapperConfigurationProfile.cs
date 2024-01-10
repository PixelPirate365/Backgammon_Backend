using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Mappings {
    public class MapperConfigurationProfile {

        public static Profile PlayerMappingProfile() {
            return new PlayerMappingProfile();
        }
        public static Profile RecieverBalanceMappingProfile() {
            return new RecieverBalanceMappingProfile();
        }
    }
}
