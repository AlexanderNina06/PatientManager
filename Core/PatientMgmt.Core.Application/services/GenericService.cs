
using AutoMapper;

namespace PatientMgmt.Core.Application;

public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
where SaveViewModel : class
where ViewModel : class
where Entity : class
{
  private readonly IGenericRepository<Entity> _genericRepository;
  private readonly IMapper _mapper;

  public GenericService(IGenericRepository<Entity> genericRepository, IMapper mapper){
    _genericRepository = genericRepository;
    _mapper = mapper;
  }
    public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
    {
        Entity entity = _mapper.Map<Entity>(vm);

        entity = await _genericRepository.AddAsync(entity);

        SaveViewModel SaveVm = _mapper.Map<SaveViewModel>(entity);

        return SaveVm;
    }

    public async Task Delete(int id)
    {
        Entity entity = await _genericRepository.GetByIdAsync(id);
        await _genericRepository.DeleteAsync(entity);
    }

    public async Task<List<ViewModel>> GetAll()
    {
        var entity = await _genericRepository.GetAllAsync();

        return _mapper.Map<List<ViewModel>>(entity);
    }

    public async Task<SaveViewModel> GetByIdSaveViewModel(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);

        SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);

        return vm;
    }

    public async Task Update(SaveViewModel vm, int id)
    {
       Entity entity = _mapper.Map<Entity>(vm);

      await _genericRepository.UpdateAsync(entity, id);

    }
}
