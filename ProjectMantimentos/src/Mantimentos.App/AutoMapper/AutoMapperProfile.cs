using AutoMapper;
using Mantimentos.App.Business.Models;
using Mantimentos.App.ViewModels;

namespace Mantimentos.App.AutoMapper
{
    /// <summary>
    /// Criado a Class AutoMapperProfile que como definido no startup a classe que herda de Profile vai ser implementado o automapper.
    /// Utilizado o ReserveMap para fazer o mapeamento inverso. Caso exista algum impedimento necessario considerar o mapeamento manualmente
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        private readonly IMapper _mapper;
        public AutoMapperProfile(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AutoMapperProfile()
        {
            CreateMap<Marca, MarcaViewModel>().ReverseMap();
            CreateMap<Movimento, MovimentoViewModel>().ReverseMap();
            CreateMap<Mantimento, MantimentoViewModel>().ReverseMap();
            CreateMap<TpMantimento, TpMantimentoViewModel>().ReverseMap();
            CreateMap<TpMantimentoCategoria, TpMantimentoCategoriaViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<UnidadeMedida, UnidadeMedidaViewModel>().ReverseMap();
        }

    }
}
