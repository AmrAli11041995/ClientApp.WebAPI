using Customer.DTOs.AppDTOs.Client;
using Customer.DTOs.Common;

namespace Customer.Core.Interfaces.IAppServices
{
    public interface IClientService
    {
        Task<Result<PaginatedResult>> FilterByAsync(PaginatedFiltration filtrationDTO);
        Task<Result<ClientListingDTO>> GetByIdAsync(Guid id);
        Task<Result<Customer.Core.DomainModels.Client>> CreateAsync(ClientCreateDTO customerDTO);
        Task<Result<Customer.Core.DomainModels.Client>> UpdateAsync(ClientUpdateDTO customerDTO);
        Task<Result<bool>> DeleteAsync(Guid id);
     

    }
}
