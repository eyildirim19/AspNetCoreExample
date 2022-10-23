using AspNetCoreExample.Areas.Manage.Models;
using AspNetCoreExample.Models.Entities;
using AutoMapper;

namespace AspNetCoreExample.Models.Mapper
{
    // Profile => AutoMapper namespacesi altındadır..
    // 1 ) profile sınıfı oluşturulup createmap metodu ile mapping yapılır. createmap metodu Profile sınıfından gelir..
    // 2 ) İlgili Controolerda IMapper interfacesi tanımlanır. BU interface üzerinden map metodu ile mapping yapılır
    // 3 ) Program.cs'de AddAutoMapper() metodu ile Controller'a instance gönderilir..
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categories, CategoryVM>(); // Categories'den CategoryVM'e
            CreateMap<CategoryVM, Categories>(); // CategoryVM'den Categories'e
            // Diğer sınıflar için devam edersiniz...
        }
    }
}