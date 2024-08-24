using AutoMapper;
using Customer.Application.Helper;
using Customer.Core.DomainModels;
using Customer.Core.Enums;
using Customer.Core.Interfaces.Common;
using Customer.Core.Interfaces.IAppServices;
using Customer.DTOs.AppDTOs.Client;
using Customer.DTOs.Common;
using Customer.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Customer.Application.AppServices
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Customer.Core.DomainModels.Client, Guid> _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(
            IRepository<Customer.Core.DomainModels.Client, Guid> clientRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<Result<PaginatedResult>> FilterByAsync(PaginatedFiltration filtrationDTO)
        {
            var clients = _clientRepository.GetWhere();
            var totalCount = clients.Count();

            List<ClientListingDTO> clientsListing = _mapper.Map<List<ClientListingDTO>>(await clients.Paginate(filtrationDTO).ToListAsync());

            return new Result<PaginatedResult>()
            {
                Data = new PaginatedResult { Data = clientsListing, TotalCount = totalCount },
                Message = "",
                Status = true,
                StatusCode = 200,
            };

        }

        public async Task<Result<Core.DomainModels.Client>> CreateAsync(ClientCreateDTO clientDTO)
        {
            try
            {
                // check email is unique
                var clientWithSameEmail = _clientRepository.GetWhere(x => x.Email == clientDTO.Email).ToList();
                if(clientWithSameEmail == null || clientWithSameEmail.Count > 0)
                {
                    return new Result<Core.DomainModels.Client>
                    {
                        StatusCode = (int)ResponseStatus.InternalError,
                        Message = "This email is exist before",
                        Status = false,
                    };
                }
                Core.DomainModels.Client client = _mapper.Map<Core.DomainModels.Client>(clientDTO);
                client.Id = new Guid();
                await _clientRepository.CreateAsync(client);
                await _clientRepository.SaveChangesAsync();
                return new Result<Core.DomainModels.Client>
                {
                    StatusCode = (int)ResponseStatus.Success,
                    Data = client,
                    Message = "Added Successfully",
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                return new Result<Core.DomainModels.Client>
                {
                    StatusCode = (int)ResponseStatus.InternalError,
                    Message = "Something Went Wrong",
                    Status = false,
                    ExceptionMessage = ex.Message
                };
            }
        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            try
            {
                await _clientRepository.DeleteAsync(id);
                await _clientRepository.SaveChangesAsync();

                return new Result<bool>
                {
                    StatusCode = (int)ResponseStatus.Success,
                    Message = "Deleted Successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    StatusCode = (int)ResponseStatus.InternalError,
                    Message = "Something Went Wrong",
                    Status = false,
                    ExceptionMessage = ex.Message
                };
            }
        }

        public async Task<Result<ClientListingDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                ClientListingDTO clientDto = _mapper.Map<ClientListingDTO>(await _clientRepository.GetByIdAsync(id));
                return new Result<ClientListingDTO>
                {
                    StatusCode = (int)ResponseStatus.Success,
                    Data = clientDto,
                    Message = "",
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                return new Result<ClientListingDTO>
                {
                    StatusCode = (int)ResponseStatus.InternalError,
                    Message = "Something Went Wrong",
                    Status = false,
                    ExceptionMessage = ex.Message
                };
            }
        }
       
        public async Task<Result<Core.DomainModels.Client>> UpdateAsync(ClientUpdateDTO clientDTO)
        {
            try
            {
                // check email is unique
                var clientWithSameEmail = _clientRepository.GetWhere(x => x.Email == clientDTO.Email && x.Id != clientDTO.Id).ToList();
                if (clientWithSameEmail == null || clientWithSameEmail.Count > 0)
                {
                    return new Result<Core.DomainModels.Client>
                    {
                        StatusCode = (int)ResponseStatus.InternalError,
                        Message = "This email is exist before",
                        Status = false,
                    };
                }

                Core.DomainModels.Client client = _mapper.Map<Core.DomainModels.Client>(clientDTO);
                _clientRepository.Update(client);
                await _clientRepository.SaveChangesAsync();
                return new Result<Core.DomainModels.Client>
                {
                    StatusCode = (int)ResponseStatus.Success,
                    Message = "Updated Successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new Result<Core.DomainModels.Client>
                {
                    StatusCode = (int)ResponseStatus.InternalError,
                    Message = "Something Went Wrong",
                    Status = false,
                    ExceptionMessage = ex.Message
                };
            }
        }
    }
}