namespace PatientMgmt.Core.Application;

public interface IGenericService<SaveViewModel, ViewModel, Entity> 
where SaveViewModel : class
where ViewModel : class
where Entity : class
{

Task<SaveViewModel> Add(SaveViewModel vm);
Task Update(SaveViewModel vm, int id);
Task Delete(int id);
Task<List<ViewModel>> GetAll();
Task<SaveViewModel> GetByIdSaveViewModel(int id);

}
